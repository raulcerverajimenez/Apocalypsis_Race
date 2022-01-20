/*
 * Gestiona la vida del coche, los premios y los puntos
 */


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gestion_V_P : MonoBehaviour
{
    private Audios a_audios;

    private bool b_gameRunning = true;

    private float f_puntos = 0;
    public Text t_score;
    private int i_score = 0;

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
            //Activa la pantalla del Game Over
            go_gcontadores.SetActive(false);
            go_gameOver.SetActive(true);

            //Desactiva el coche
            FindObjectOfType<Mov_coche>().enabled = false;
        }
        //Texto de puntos final
        //int puntosFinales = (int)(f_puntos / r_script_reloj.F_segTotales * 100);
        int puntosFinales = (int)f_puntos;
        t_score.text = PlayerPrefs.GetString("key", "Alias") + " ...... " + puntosFinales;
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
                    i_vida -= 10;
                    a_audios.Reproductor(4);
                    Scrollbar(-0.1f);
                }

                break;

            case "Tree 4":
                if (!b_paraGolpes)
                {
                    i_vida -= 20;
                    a_audios.Reproductor(4);
                    Scrollbar(-0.2f);
                }
                break;

            case "Cap_tornado":
                if (!b_paraGolpes)
                {
                    a_audios.Reproductor(4);
                    i_vida -= 20;
                    //Destroy(c.gameObject);
                    Scrollbar(-0.2f);
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
                Invoke("Desactiva", 7f);
                break;
        }

        if (i_vida < 0) i_vida = 0;
        if (i_vida > 100) i_vida = 100;
        t_cont_vida.text = i_vida + "%";

        ////Texto de puntos final
        //int puntosFinales = (int)(f_puntos / r_script_reloj.F_segTotales * 100);
        //t_score.text = PlayerPrefs.GetString("key", "Alias") + " ...... " + puntosFinales;
    }

    //Desactiva el paragolpes
    private void Desactiva()
    {
        go_paraGolpes.SetActive(false);
        b_paraGolpes = false;
    }

    private Color newColor;


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


