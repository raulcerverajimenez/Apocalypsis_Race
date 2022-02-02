/*
 * Gestiona el movimiento de coche
 */

using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Gestion_estados))]


public class Mov_coche : MonoBehaviour
{
    public ParticleSystem ps_polvoRueda1;
    public ParticleSystem Ps_polvoRueda1 {set => ps_polvoRueda1 = value; }
    public ParticleSystem ps_polvoRueda2;
    public ParticleSystem Ps_polvoRueda2 {set => ps_polvoRueda2 = value; }

    public Transform t_rueda_izq;
    public Transform t_rueda_der;

    public Text txt_velocimetro;

    public GameObject go_turbo;
    private bool b_turbo = false;
    private int i_turbo = 1;
    public int I_turbo { get => i_turbo; set => i_turbo += value; }
    

    private Audios a_audios;
    private Gestion_estados ge_estados;
    private Moviment_Camara mc_camera;

    private Vector3 v3_l_posicon_final;
    private Vector3 v3_g_posicion_final;
    private Vector3 v3_g_gravedad = new Vector3(0, -9.8f, 0);

    private float f_km_hora;

    private float f_velocidad_max_m_s = 40f;
    private float f_bot = 9f;
    private float f_sensibilidad_giro = 65f;

    private CharacterController cc_coche;


    private void Start()
    {
        a_audios = FindObjectOfType<Audios>();
        mc_camera = FindObjectOfType<Moviment_Camara>();
        ge_estados = GetComponent<Gestion_estados>();
        cc_coche = GetComponent<CharacterController>();
    }

    void Update()
    {
        avanceCoche();
        giroCoche();
        giroRuedas();
        sonidoMotor();
        polvoRuedas();
    }


    private void avanceCoche()
    {

        if (cc_coche.isGrounded)
        {
            v3_l_posicon_final = Vector3.down;

            //Avanzar o retroceder con aceleracion/deceleración (eje Z)
            v3_l_posicon_final += Vector3.forward * ge_estados.F_a_r * f_velocidad_max_m_s;

            //Saltar
            if (ge_estados.B_saltar)
            {
                v3_l_posicon_final += Vector3.up * f_bot;
            }

            //Turbo
            if (ge_estados.B_turbo && i_turbo > 0)
            {
                i_turbo--;
                turbo();
            }

            //lo transforma a cordenadas globales
            v3_g_posicion_final = transform.TransformVector(v3_l_posicon_final);

            //calcular velocidad en km/h (*3.6)
            f_km_hora = v3_l_posicon_final.z * 3.6f;

            if (f_km_hora < 0) f_km_hora *= -1;
            txt_velocimetro.text = string.Format("{0:0}", f_km_hora);

        }
        else
        {
            // si no esta tocando el suelo, baja hasta el suelo 
            v3_g_posicion_final += v3_g_gravedad * Time.deltaTime;
        }

        //Mueve el coche
        cc_coche.Move(v3_g_posicion_final * Time.deltaTime);

    }

    private void giroCoche()
    {
        if (cc_coche.isGrounded)
        {
            //giro coche izquierda o derecha (eje y)
            transform.Rotate(Vector3.up, ge_estados.F_giro * f_sensibilidad_giro * Time.deltaTime, Space.Self);
        }
        else
        {
            // si no esta tocando el suelo, baja hasta el suelo 
            v3_g_posicion_final += v3_g_gravedad * Time.deltaTime;
        }

    }

    private void giroRuedas()
    {
        //giro de las ruedas limitado a 30 y -30 grados
        if (t_rueda_der.transform.localRotation.y < 0.30f && t_rueda_der.transform.localRotation.y > -0.30f)
        {
            t_rueda_izq.transform.Rotate(Vector3.up, ge_estados.F_giro * f_sensibilidad_giro * Time.deltaTime, Space.Self);
            t_rueda_der.transform.Rotate(Vector3.up, ge_estados.F_giro * f_sensibilidad_giro * Time.deltaTime, Space.Self);
        }

        //vuelta de las ruedas a su posicion de inicio
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            t_rueda_izq.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            t_rueda_der.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }


    private void sonidoMotor()
    {
        a_audios.sonidoMotor(f_km_hora, f_velocidad_max_m_s, b_turbo);
    }

    public void turbo()
    {
        f_velocidad_max_m_s = 60f;
        Invoke("velocidadNormal", 5f);
        go_turbo.SetActive(true);
        b_turbo = true;
    }

    private void velocidadNormal()
    {
        f_velocidad_max_m_s = 40f;
        go_turbo.SetActive(false);
        b_turbo = false;
    }


    //Controla el polvo que emiten las ruedas
    private void polvoRuedas()
    {
        if (cc_coche.isGrounded && f_km_hora != 0)
        {
            ps_polvoRueda1.Play();
            ps_polvoRueda2.Play();
        }
        else
        {
            ps_polvoRueda1.Stop();
            ps_polvoRueda2.Stop();
        }
    }
}
