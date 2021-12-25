using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayo : MonoBehaviour
{

    GameObject go_coche;
    GameObject go_rayo;

    private bool b_rayo = true;
    private float f_segundos = 5;
    private Audios a_audios;

    void Start()
    {
        go_coche = GameObject.Find("Free_Racing_Car_Yellow");
        go_rayo = GameObject.Find("SimpleLightningBoltPrefab");

        a_audios = FindObjectOfType<Audios>();
    }

    // Update is called once per frame
    void Update()
    {
        f_segundos -= Time.deltaTime;

        if (f_segundos <= 0)
        {
            GenerarRayos();
            f_segundos = Random.Range(5, 13);
        }

    }


    void GenerarRayos()
    {

        Vector3 v3_pos_instanciar_rayo = go_coche.transform.position +
                                       go_coche.transform.TransformVector(new Vector3(Random.Range(-5, 2), 0, Random.Range(30, 40)));

        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            Destroy(Instantiate(go_rayo, v3_pos_instanciar_rayo, Quaternion.Euler(new Vector3(0, 0, 0))), 0.3f);
            v3_pos_instanciar_rayo.x += Random.Range(-5, 2);
            v3_pos_instanciar_rayo.z += Random.Range(3, 10);
            a_audios.Reproductor(6);
        }
    }

}
