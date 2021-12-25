using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_camera : MonoBehaviour
{

    public GameObject go_a_seguir;
    public Vector3 v3_posicio = Vector3.zero;

   

    // Start is called before the first frame update
    void Start()
    {

    }

    Vector3 v3_velocitat = Vector3.zero;
    Vector3 v3_velocitat_rotacio = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (go_a_seguir == null)
            return;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            go_a_seguir.transform.TransformPoint(v3_posicio),
            ref v3_velocitat, 0.3f);

        transform.LookAt(
            Vector3.SmoothDamp(
                transform.TransformPoint(new Vector3(0, 0, 1000f)),
                go_a_seguir.transform.TransformPoint(new Vector3(0, 0, 1000f)),
                ref v3_velocitat_rotacio, 0.3f));

    }
}
