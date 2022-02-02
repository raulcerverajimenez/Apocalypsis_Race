/*
 * Gestiona la vida del coche, los premios y los puntos.
 */


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gestion_V_P : MonoBehaviour
{
    private Audios a_audios;

    private bool b_gameRunning = true;

    private float f_puntos = 0;
    private int i_score = 0;

    public Text t_score1;
    public Text t_puntos;
    public Text t_tiempo;
    public Text t_total;
    public Text t_nombre;

    private int i_vida = 100;
    public Text t_cont_vida;

    public Text t_cont_turbo;

    private Dispar d_script_dispar;
    private Mov_coche m_script_movCoche;
    private Reloj r_script_reloj;

    public GameObject go_gameOver;
    public GameObject go_gcontadores;
    public GameObject go_paraGolpes;

    private List<AudioSource> l_audios_sonando;

    public Scrollbar sb_vida;

    private bool b_paraGolpes = false;

    void Start()
    {
        t_cont_vida.text = 100 + "";

        m_script_movCoche = FindObjectOfType<Mov_coche>();
        a_audios = FindObjectOfType<Audios>();
        r_script_reloj = FindObjectOfType<Reloj>();
        d_script_dispar = FindObjectOfType<Dispar>();
    }

    private void Update()
    {
        t_cont_turbo.text = m_script_movCoche.I_turbo + "";

        if (i_vida <= 0)
        {
            //apagar efecto polvo
            m_script_movCoche.ps_polvoRueda1.Stop();
            m_script_movCoche.ps_polvoRueda2.Stop();

            //apaga el sonido del motor
            a_audios.Reproductor(7);

            //Activa la pantalla del Game Over
            go_gcontadores.SetActive(false);
            go_gameOver.SetActive(true);

            //Desactiva el script coche
            FindObjectOfType<Mov_coche>().enabled = false;

            //Texto de puntos final
            int puntosFinales = (int)(f_puntos / (int)r_script_reloj.F_segTotales * 100);
            t_nombre.text = PlayerPrefs.GetString("key", "Alias");
            t_puntos.text = f_puntos.ToString();
            int minutos = ((int)r_script_reloj.F_segTotales / 60);
            int segundos = ((int)r_script_reloj.F_segTotales % 60);
            t_tiempo.text = "'" +  minutos + " ''" + segundos;
            t_total.text =  puntosFinales.ToString();
        }


        //Texto puntos partida
        t_score1.text = f_puntos.ToString();

        //Pausa el juego
        if (Input.GetKeyDown(KeyCode.Space)) ChangeGameRunning();
    }


    private void OnTriggerEnter(Collider c)
    {
        switch (c.gameObject.name)
        {
            case "Large Rock 1(Clone)":
                if (!b_paraGolpes)
                {
                    i_vida -= 5;
                    a_audios.Reproductor(4);
                    Scrollbar(-0.05f);
                }

                break;

            case "Tree 4":
                if (!b_paraGolpes)
                {
                    i_vida -= 10;
                    a_audios.Reproductor(4);
                    Scrollbar(-0.1f);
                }
                break;

            case "Cap_tornado":
                if (!b_paraGolpes)
                {
                    a_audios.Reproductor(4);
                    i_vida -= 15;
                    //Destroy(c.gameObject);
                    Scrollbar(-0.15f);
                }
                break;

            case "Premio_vida":
                i_vida += 10;
                Scrollbar(0.1f);
                break;

            case "Premio_proyectil":
                d_script_dispar.setContProyectil(20);
                break;

            case "Premio_turbo":
                //aumenta el turbo en 1
                m_script_movCoche.I_turbo = 1;
                break;

            case "Premio_paraGolpes":
                go_paraGolpes.SetActive(true);
                b_paraGolpes = true;
                
                Invoke("Desactiva1", 5f);
                Invoke("Desactiva2", 5.25f);
                Invoke("Desactiva3", 5.5f);
                Invoke("Desactiva4", 5.75f);
                Invoke("Desactiva5", 6f);
                Invoke("Desactiva6", 6.25f);
                Invoke("Desactiva7", 6.5f);
                Invoke("Desactiva8", 6.75f);
                Invoke("Desactiva", 7f);
                break;
        }

        if (i_vida < 0) i_vida = 0;
        if (i_vida > 100) i_vida = 100;
        t_cont_vida.text = i_vida + "%";

    }

    //Desactiva el paragolpes

    private void Desactiva1() { go_paraGolpes.SetActive(false); }
    private void Desactiva2() { go_paraGolpes.SetActive(true); }
    private void Desactiva3() { go_paraGolpes.SetActive(false); }
    private void Desactiva4() { go_paraGolpes.SetActive(true); }
    private void Desactiva5() { go_paraGolpes.SetActive(false); }
    private void Desactiva6() { go_paraGolpes.SetActive(true); }
    private void Desactiva7() { go_paraGolpes.SetActive(false); }
    private void Desactiva8() { go_paraGolpes.SetActive(true); }
    private void Desactiva()
    {
        go_paraGolpes.SetActive(false);
        b_paraGolpes = false;
    }


    private void Scrollbar(float num)
    {
        sb_vida.size += num;
        ColorBlock cb = sb_vida.colors;

        if (sb_vida.size >= 0.6f)
        {
            cb.normalColor = Color.green;
        }
        else if (sb_vida.size <= 0.3f)
        {
            cb.normalColor = Color.red;
        }
        else if (sb_vida.size < 0.6f)
        {
            cb.normalColor = Color.yellow;
        }
        sb_vida.colors = cb;
    }

    public void setPuntos(float puntos) { f_puntos += puntos; }


    // metodo para pausar o reanudar el juego (imagen y audio)
    public void ChangeGameRunning()
    {
        b_gameRunning = !b_gameRunning;

        if (b_gameRunning)
        {
            //juego activo
            Time.timeScale = 1f;
            foreach (AudioSource a in l_audios_sonando)
            {
                a.Play();
            }
        }
        else
        {
            //juego en pausa
            Time.timeScale = 0f;
            foreach (AudioSource a in Audios_sonando())
            {
                a.Pause();
            }
        }
    }

    //Busca todos los audioSource, y los que estan sonando en ese momento
    //los añade a una lista
    private List<AudioSource> Audios_sonando()
    {
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        l_audios_sonando = new List<AudioSource>();

        foreach (AudioSource a in audios)
        {
            if (a.isPlaying)
            {
                l_audios_sonando.Add(a);
            }
        }

        return l_audios_sonando;
    }
}


