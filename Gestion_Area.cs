/*
 * Gestiona las areas
 * El area detecta la entrada del coche, en ese instante derriba los árboles,
 * crea premios, tornados y piedras volcánicas, todo ello de forma aleatoria.
*/


using System.Collections.Generic;
using UnityEngine;

public class Gestion_Area : MonoBehaviour
{

    public GameObject go_premio;
    public GameObject go_tornado;
    public Rigidbody rb_piedra;

    public Arbol a_arbol_0;
    public Arbol a_arbol_1;
    public Arbol a_arbol_2;
    public Arbol a_arbol_3;

    private List<Arbol> l_lista = new List<Arbol>();
    private Arbol rb_arbol;

    public GameObject area;

    private void Start()
    {
        l_lista.Add(a_arbol_0);
        l_lista.Add(a_arbol_1);
        l_lista.Add(a_arbol_2);
        l_lista.Add(a_arbol_3);
    }


    private void OnTriggerEnter(Collider c)
    {
        //preguntamos si el que entra es el coche
        if (c.gameObject.name == "Free_Racing_Car_Yellow")
        {

            //Elige arboles de forma aleatoria
            int i_cont = Random.Range(1, 4);
            
            for (int i = 0; i < i_cont; i++)
            {
                int i_tamaño = l_lista.Count;
                int i_indice = Random.Range(0, i_tamaño);
                rb_arbol = l_lista[i_indice];
                rb_arbol.GetComponent<Arbol>().caure();
                l_lista.RemoveAt(i_indice);
            }


            /*Posicion para instanciar piedra respecto al área: 
             * Coge la posicion local  del area y la transforma a global
             *Le suma el vector que creamos y lo trtansforma a global
            */
            Vector3 v3_pos_instanciar_piedra = area.transform.position +
                                area.transform.TransformVector(new Vector3(0, 5, 20));

            for (int i = 0; i < Random.Range(10, 20); i++)
            {
                Instantiate(rb_piedra, v3_pos_instanciar_piedra, Quaternion.Euler(new Vector3(0, 0, 0)));

                v3_pos_instanciar_piedra.x += Random.Range(-10, 10);
                v3_pos_instanciar_piedra.y += Random.Range(-10, 20);
                v3_pos_instanciar_piedra.z += Random.Range(-20, 20);
            }


            /*Posicion para instanciar premio respecto al área: 
            - Coge la posicion local  del area y la transforma a global
            - Le suma el vector que creamos y lo trtansforma a global
            */
            Vector3 v3_pos_instanciar_premio = area.transform.position +
                              area.transform.TransformVector(new Vector3(0, 0.01f, 10));

            for (int i = 0; i < Random.Range(1, 3); i++)
            {
                Instantiate(go_premio, v3_pos_instanciar_premio, Quaternion.Euler(new Vector3(0, 0, 0)));
                v3_pos_instanciar_premio.x += Random.Range(1, 2);
                v3_pos_instanciar_premio.z += Random.Range(-5, 15);
            }


            /*Posicion para instanciar tornado respecto al área: 
            - Coge la posicion local  del area y la transforma a global
            - Le suma el vector que creamos y lo trtansforma a global
            */
            Vector3 v3_pos_instanciar_tornado = area.transform.position +
                                area.transform.TransformVector(new Vector3(0, 0.01f, 10));

            for (int i = 0; i < Random.Range(1, 3); i++)
            {
                Instantiate(go_tornado, v3_pos_instanciar_tornado, Quaternion.Euler(new Vector3(0, 0, 0)));
                //v3_pos_instanciar_premio.x += Random.Range(1, 2);
                v3_pos_instanciar_tornado.z += Random.Range(5, 10);
            }
        }
    }
}


