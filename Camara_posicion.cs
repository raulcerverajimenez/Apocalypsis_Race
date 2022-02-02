using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class Camara_posicion : MonoBehaviour
{
    public CharacterController cc_coche;

    private Camera c_camara;

    private Vector3 v3_posGeneral = new Vector3(0, 5, -14);
    private Vector3 v3_posMetrayeta = new Vector3(0, 1.74f, -0.08f);

    private void Start()
    {
        c_camara = GetComponent<Camera>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.N))
        {
            c_camara.transform.position = cc_coche.transform.position + 
                c_camara.transform.TransformVector(v3_posGeneral);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            c_camara.transform.position = cc_coche.transform.position + 
                c_camara.transform.TransformVector(v3_posMetrayeta);
        }

    }
}
