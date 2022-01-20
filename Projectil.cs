/*
 * Controla el proyectil
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    public GameObject go_explosio; //efecto de explosion 
    public float f_velocitat = 200f;
    public float f_vida = 5f;

    void Start()
    {
        //Velociad proyectil
        GetComponent<Rigidbody>().velocity = transform.TransformVector(Vector3.forward * f_velocitat);

        //Destrucción proyectil
        Destroy(gameObject, f_vida);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(Instantiate(go_explosio, transform.position, Quaternion.identity), 1.5f);
        Destroy(gameObject);
    }


}
