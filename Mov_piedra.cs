using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Mov_piedra : MonoBehaviour
{
    Rigidbody rb_piedra;
    GameObject go_coche;

    public float f_velocidad_avance;
    private float f_fuerza = 70000;

    AudioSource as_audioData;

    bool b_sonido_piedra = true;

    void Start()
    {
        rb_piedra = GetComponent<Rigidbody>();
        rb_piedra.mass = 50; //Masa de la piedra

        go_coche = GameObject.Find("Free_Racing_Car_Yellow");

        // La piedra coge la rotacion del coche para
        //que ambos tengan el eje en la misma rotación
        rb_piedra.transform.rotation = go_coche.transform.rotation;

        //Fuerza que empuja la piedra
        rb_piedra.AddForceAtPosition(transform.forward * -f_fuerza,
                        transform.position + new Vector3(0, 0, 0));

        rb_piedra.transform.Rotate(190, 0, 0); //rotacion piedra

        //Cambia el tamaño de la piedra
        float f_escala = Random.Range(0.3f, 1);
        rb_piedra.transform.localScale = (new Vector3(f_escala, f_escala, f_escala));

        Destroy(gameObject, Random.Range(10, 20));   //Destruye la piedra
    }



    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.name == "Terrain" && b_sonido_piedra)
        {
            //se reproduce el sonido de la piedra cuando choca con el suelo
            as_audioData = GetComponent<AudioSource>();
            as_audioData.Play(0);

            // Esta variable booleana es para controlar que sólo reproduce elsonido
            //la primera vez que toca el terreno.
            b_sonido_piedra = false; 
        }
    }

}



