/*Gestiona la vida del coche, los premios y los puntos*/


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gestion_V_P : MonoBehaviour
{
    private Audios a_audios;

    private bool b_gameRunning = true;

    private float f_puntos = 0;
    public Text t_puntos;
    private int i_score = 0;

    private int i_vida = 100;
    public InputField if_cont_vida;

    private Dispar d_script_dispar;
    private Mov_coche m_script_movCoche;
    private Reloj r_script_reloj;

    public GameObject go_gameOver;

    private List<AudioSource> l_audios_sonando;


    void Start()
    {
        a_audios = FindObjectOfType<Audios>();

        r_script_reloj = FindObjectOfType<Reloj>();
        d_script_dispar = FindObjectOfType<Dispar>();
        m_script_movCoche = FindObjectOfType<Mov_coche>();


    }

    private void Update()
    {
        if (i_vida <= 0)
        {

            i_score = PlayerPrefs.GetInt("alias");

            //Activa la pantalla del Game Over
            go_gameOver.SetActive(true);

            //Desactiva el coche
            FindObjectOfType<Mov_coche>().enabled = false;
        }

        //Pausa el juego
        if (Input.GetKeyDown(KeyCode.Space)) ChangeGameRunning();
    }


    private void OnTriggerEnter(Collider c)
    {
        //if (c.gameObject.name.Substring(0, 4).Equals("Tree")) i_vida -= 10;

        switch (c.gameObject.name)
        {
            case "Large Rock 1(Clone)":
                i_vida -= 10;
                a_audios.Reproductor(4);
                break;

            case "Tree 4":
                i_vida -= 20;
                a_audios.Reproductor(4);
                break;
            case "Cap_tornado":
                a_audios.Reproductor(4);
                i_vida -= 20;
                Destroy(c.gameObject);
                break;

            case "Premio_vida":
                i_vida += 10;
                break;

            case "Premio_proyectil":
                d_script_dispar.setContProyectil(20);
                break;

            case "Premio_turbo":
                m_script_movCoche.turbo();
                break;
            case "Premio_tamaño":
                m_script_movCoche.cambiaEscala();
                break;
        }

        if (i_vida < 0) i_vida = 0;

        if_cont_vida.text = i_vida + " %";
        i_score = (int)(f_puntos / r_script_reloj.F_segTotales) * 150;

        t_puntos.text = "Score: " + i_score;

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


