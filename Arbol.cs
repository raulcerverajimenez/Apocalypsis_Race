using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbol : MonoBehaviour
{

    private Audios a_audios;
    private float f_fuerza = 100000;

    private void Start()
    {
        a_audios = FindObjectOfType<Audios>();
    }

    public void caure()
    {
        gameObject.AddComponent<Rigidbody>();

        Rigidbody rb_arbol = GetComponent<Rigidbody>();
        rb_arbol.mass = 1000;

        rb_arbol.AddForceAtPosition(transform.forward * f_fuerza,
                        transform.position + new Vector3(0, 20f, 0));
        //transform.forwad --> vector local z del arbol transformado a global
        //transform.position + new Vector3(0, 20f, 0) --> aplica la fuerza en el punto 20f

        a_audios.Reproductor(1);
    }

}
