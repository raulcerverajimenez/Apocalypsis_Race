using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_piedra : MonoBehaviour
{
    private Gestion_V_P script_gvp;
    public GameObject go_explosio; //efecto de explosion 
    public int i_cont_projectil;
    
    Rigidbody rb_piedra;

    public void Start()
    {
        script_gvp = FindObjectOfType<Gestion_V_P>();
        rb_piedra = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision hit)
    {
        //preguntamos si el que colisiona es el projectil
        if (hit.gameObject.name == "Projectil(Clone)")
        {
            i_cont_projectil++;
        }

        if (i_cont_projectil >= 1)
        {
            //Destroy(Instantiate(go_explosio, transform.position, Quaternion.identity), 1.5f);
            Destroy(gameObject);
            script_gvp.setPuntos(200);
        }
    }
}
