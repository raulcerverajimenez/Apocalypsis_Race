using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    
    public GameObject go_capsula;
    public GameObject go_esfera;
    public GameObject go_tornado;

    private float f_veloc_rotacion;
    private float f_veloc_traslacion;
    private int i_direccion;
    

    private void Start()
    {
        //float f_escala = Random.Range(0.5f, 1f);
        //go_tornado.transform.localScale = (new Vector3(f_escala, f_escala * 2, f_escala));

        switch (Random.Range(0, 2))
        {
            case 0:
                i_direccion = -1;
                break;
            case 1:
                i_direccion = 1;
                break;
        }

        f_veloc_rotacion = Random.Range(1000, 1400);
        f_veloc_traslacion = Random.Range(40f, 70f);
        
        Destroy(go_esfera, Random.Range(10, 15));
    }

    void Update()
    {
        go_capsula.transform.Rotate(Vector3.up, f_veloc_rotacion * i_direccion * Time.deltaTime, Space.World);
        go_esfera.transform.Rotate(Vector3.up, f_veloc_traslacion * i_direccion * Time.deltaTime, Space.World);
       
    }
}
