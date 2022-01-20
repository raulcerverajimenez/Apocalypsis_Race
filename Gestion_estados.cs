/* 
 * Gestiona las entredas de teclado, ratón, pantalla del móvil.....
 * Detecta si se esta jugando en un PC o un Android
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gestion_estados : MonoBehaviour
{
    public bool b_android;
    public GameObject go_cnv_android;
    public FixedJoystick joystick;


    //encapsulado de variables a tipo get
    bool b_saltar;
    public bool B_saltar { get => b_saltar; }

    bool b_turbo;
    public bool B_turbo { get => b_turbo;}

    bool b_disparar;
    public bool B_disparar { get => b_disparar; }

    float f_a_r;
    public float F_a_r { get => f_a_r; }

    float f_giro;
    public float F_giro { get => f_giro; }
   


    // Update is called once per frame
    void Update()
    {
        if (b_android)
        {
            go_cnv_android.SetActive(true);

            f_a_r = joystick.Vertical;
            f_giro = joystick.Horizontal;
        }
        else
        {
            f_a_r = Input.GetAxis("Vertical");
            f_giro = Input.GetAxis("Horizontal");

            b_turbo = Input.GetKeyDown(KeyCode.X) ? true : false;
            b_disparar = Input.GetKeyDown(KeyCode.C) ? true : false;
        }
    }

    public void Turbo()
    {
        b_turbo = true;
    }

    public void No_Turbo()
    {
        b_turbo = false;
    }

    public void Disparar()
    {
        b_disparar = true;
    }

    public void No_disparar()
    {
        b_disparar = false;
    }
}
