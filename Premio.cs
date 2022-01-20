/*
 * Gestiona los premios, les asigna nombre, material y efecto
*/
 

using UnityEngine;

public class Premio : MonoBehaviour
{
    private Audios a_audios;

    public GameObject go_premio;
    private GameObject go_efecto = null;

    public Material m_material_vida;
    public Material m_material_proyectil;
    public Material m_material_turbo;
    public Material m_material_paraGolpes;

    public GameObject go_efectoVida;
    public GameObject go_efectoProyectil;
    public GameObject go_efectoTurbo;

    private Vector3 v3_posicionInicial;

    // Start is called before the first frame update
    void Start()
    {
        v3_posicionInicial = transform.localPosition;

        a_audios = FindObjectOfType<Audios>();

        setPremios();
        go_premio.transform.localRotation = Quaternion.Euler(new Vector3(45, 0, 45));
      
        Destroy(gameObject, Random.Range(10, 15));
    }

    private void setPremios()
    {
        //Accede al renderer del objeto
        Renderer rend = go_premio.GetComponent<Renderer>();

        switch (Random.Range(0, 4))
        {
            case 0:
                go_premio.name = "Premio_vida";
                rend.material = m_material_vida;
                go_efecto = go_efectoVida;
                Instantiate(go_efecto, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                break;
            case 1:
                go_premio.name = "Premio_proyectil";
                rend.material = m_material_proyectil;
                go_efecto = go_efectoProyectil;
                Instantiate(go_efecto, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                break;
            case 2:
                go_premio.name = "Premio_turbo";
                rend.material = m_material_turbo;
                go_efecto = go_efectoTurbo;
                Instantiate(go_efecto, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                break;
            case 3:
                go_premio.name = "Premio_paraGolpes";
                rend.material = m_material_paraGolpes;
                go_efecto = go_efectoTurbo;
                Instantiate(go_efecto, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                break;
        }

    }

 
    void Update()
    {
        transform.Rotate(Vector3.up, 2f, Space.World);
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.name == "Free_Racing_Car_Yellow")
        {
            a_audios.Reproductor(5);
            Destroy(gameObject);
            //DestroyImmediate(go_efecto, true);
            
        }
    }
}
