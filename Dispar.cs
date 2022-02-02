using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Gestion_estados))]


public class Dispar : MonoBehaviour
{
    public Vector3 v3_posEfecto = new Vector3(0, 0, -4);

    private Gestion_estados ge_estados;
    private Audios a_audios;

    public GameObject go_proyectil;
    public GameObject go_efectoDispar;
    public Transform t_punt_dispar;

    public InputField if_cont_proyectil;
    private int i_cont_proyectil = 0;

    public void Start()
    {
        i_cont_proyectil = 40;
        ge_estados = GetComponent<Gestion_estados>();
        a_audios = FindObjectOfType<Audios>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ge_estados.B_disparar)
        {
            if (i_cont_proyectil > 0)
            {
                a_audios.Reproductor(0);

                Instantiate(go_proyectil, t_punt_dispar.position, t_punt_dispar.rotation);

                Vector3 v3_pos_instanciar_efecto = t_punt_dispar.transform.position + t_punt_dispar.transform.TransformVector(v3_posEfecto);
                Destroy(Instantiate(go_efectoDispar, v3_pos_instanciar_efecto, Quaternion.identity), 0.5f);

                i_cont_proyectil--;
            }
        }

        if_cont_proyectil.text = i_cont_proyectil + "";

    }

    public void setContProyectil(int num)
    {
        i_cont_proyectil += num;
    }

}
