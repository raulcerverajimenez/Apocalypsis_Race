using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_arbol : MonoBehaviour
{
    private Gestion_V_P g_gvp;
    private Audios a_audios;

    public GameObject go_arboles_dinamicos;

    private Vector3 v3_posicion_inicial;
    private Quaternion q_rotacion_inicial;

    public GameObject go_explosio;//efecto de explosion cuando choca con algo
    public int i_cont_projectil;
    GameObject go_arbol;

    public void Start()
    {
        a_audios = FindObjectOfType<Audios>();
        g_gvp = FindObjectOfType<Gestion_V_P>();

        go_arbol = GetComponent<GameObject>();

        v3_posicion_inicial = transform.position;
        q_rotacion_inicial = transform.rotation;
    }


    private void OnCollisionEnter(Collision hit)
    {
        //preguntamos si el que colisiona es el projectil
        if (hit.gameObject.name == "Projectil(Clone)")
        {
            i_cont_projectil++;
        }

        if (i_cont_projectil >= 2)
        {
            a_audios.Reproductor(3);
            Destroy(Instantiate(go_explosio, transform.position, Quaternion.identity), 1.5f);
            //Destroy(gameObject);

            i_cont_projectil = 0;
            g_gvp.setPuntos(500);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.transform.position = v3_posicion_inicial;
            gameObject.transform.rotation = q_rotacion_inicial;
            Invoke("resetArbol", 10f);
        }
    }

    private void resetArbol()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }


}
