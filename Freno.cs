using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freno : MonoBehaviour
{
    private Mov_coche script_MovCoche;

    private void Start()
    {
        script_MovCoche = FindObjectOfType<Mov_coche>();   
    }
    private void OnCollisionEnter (Collision c)
    {
        Debug.Log("WWWW " + c.gameObject.name);
        if(c.gameObject.name == "Terrain")
        {
            script_MovCoche.I_freno = 0;
        }
        else
        {
            script_MovCoche.I_freno = 1;
        }
    }
}
