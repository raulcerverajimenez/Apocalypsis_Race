
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Gestion_estados))]


public class Mov_coche : MonoBehaviour
{
    private Audios a_audios;
    private Gestion_estados ge_estados;
    private Moviment_Camara mc_camera;

    Vector3 v3_l_posicon_final;
    Vector3 v3_g_posicion_final;

    public Vector3 v3_g_gravedad = new Vector3(0, -9.8f, 0);
    public float f_km_hora;
    public float f_velocidad_max_m_s = 40f;
    public float f_bot = 9f;
    public float f_sensibilidad_giro = 65f;

    CharacterController cc_coche;

    public Transform t_rueda_izq;
    public Transform t_rueda_der;

    public Text txt_velocimetro;


    private float f_freno = 1;

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
    }


    private void avanceCoche()
    {

        if (cc_coche.isGrounded)
        {
            v3_l_posicon_final = Vector3.down;

            //Avanzar o retroceder con aceleracion/deceleración (eje Z)
            v3_l_posicon_final += Vector3.forward * ge_estados.F_a_r * f_velocidad_max_m_s * f_freno;

            //Saltar
            if (ge_estados.B_saltar)
            {
                v3_l_posicon_final += Vector3.up * f_bot;
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
        a_audios.sonidoMotor(f_km_hora, f_velocidad_max_m_s);
    }

    public void turbo()
    {
        f_velocidad_max_m_s = 60f;
        Invoke("velocidadNormal", 5f);
    }

    private void velocidadNormal()
    {
        f_velocidad_max_m_s = 40f;
    }

    public void cambiaEscala()
    {
        mc_camera.B_camera = false;
        Invoke("escala1", 0.25f);
        Invoke("escala2", 0.50f);
        Invoke("escala3", 0.75f);
        Invoke("escala4", 1f);
        Invoke("escala5", 1.25f);
        Invoke("escala6", 1.5f);
        //Invoke("escala7", 1.5f);
        //Invoke("escala8", 1.75f);
        //Invoke("escala9", 2f);
        //Invoke("escala10", 2.5f);
        //Invoke("escala11", 3f);

        Invoke("escalaNormal", 8f);
    }

    private void escala1() { cc_coche.transform.localScale = (new Vector3(1.2f, 1.2f, 1.2f)); }
    private void escala2() { cc_coche.transform.localScale = (new Vector3(1.4f, 1.4f, 1.4f)); }
    private void escala3() { cc_coche.transform.localScale = (new Vector3(1.6f, 1.6f, 1.6f)); }
    private void escala4() { cc_coche.transform.localScale = (new Vector3(1.8f, 1.8f, 1.8f)); }
    private void escala5() { cc_coche.transform.localScale = (new Vector3(2f, 2f, 2f)); }
    private void escala6() { cc_coche.transform.localScale = (new Vector3(2.2f, 2.2f, 2.2f)); }
    private void escala7() { cc_coche.transform.localScale = (new Vector3(1.35f, 1.35f, 1.35f)); }
    private void escala8() { cc_coche.transform.localScale = (new Vector3(1.4f, 1.4f, 1.4f)); }
    private void escala9() { cc_coche.transform.localScale = (new Vector3(1.45f, 1.45f, 1.45f)); }
    private void escala10() { cc_coche.transform.localScale = (new Vector3(1.5f, 1.5f, 1.5f)); }
    private void escala11() { cc_coche.transform.localScale = (new Vector3(1.55f, 1.55f, 1.55f)); }
    private void escalaNormal() 
    { 
        cc_coche.transform.localScale = (new Vector3(1, 1, 1));
        mc_camera.B_camera = true;
    }



}
