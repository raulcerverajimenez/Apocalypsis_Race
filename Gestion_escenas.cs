using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Gestion_escenas : MonoBehaviour
{
    public Button btn_boton;
    public int indiceNivel;

    private GameObject go_coche;
    private bool b_cambiar = true;

    public GameObject go_instrucciones;
    public GameObject go_coches;

    public GameObject cc_amarillo;
    //public CharacterController cc_rojo;
    //public CharacterController cc_negro;
    //public CharacterController cc_azul;

    private List<CharacterController> listaCoches = new List<CharacterController>();

    public Text if_alias;

    private Memoria_score script_MemoriaScore;

    private String s_nomCoche;

    public string S_nomCoche { get => s_nomCoche; }
    public GameObject Go_coche { get => go_coche; }
  

    public void Start()
    {
        script_MemoriaScore = FindObjectOfType<Memoria_score>();
        //cc_amarillo= GameObject.Find("Free_Racing_Car_Yellow");

    }

    public void Update()
    {
        //if (SceneManager.GetActiveScene().buildIndex == 1 && b_cambiar)
        //{
        //    Instantiate(go_coche, (new Vector3(103, 1, 294)), Quaternion.Euler(new Vector3(0, 0, 0)));
           
        //    b_cambiar = false;

        //}
        
    }

    //cambia de escena

    String s_nombrePrefName;
    public void CambiarNivel1()
    {
        MemoriaPuntos();
        SceneManager.LoadScene(1);    
    }

    public void CambiarNivel0()
    {
        SceneManager.LoadScene(0);
    }

    private void MemoriaPuntos()
    {
        PlayerPrefs.SetString("key", if_alias.text);
    }

    //cierra la aplicacion
    public void Salir()
    {
        Application.Quit();
    }

    public void EnterMouseBoton()
    {
        btn_boton.image.color = new Color(0, 0, 0, 0.4f);
    }

    public void ExitMouseBoton()
    {
        btn_boton.image.color = new Color(56, 56, 56, 0);
    }

    public void ActivarIntrucciones()
    {
        go_instrucciones.SetActive(true);
    }

    public void DesactivarIntrucciones()
    {
        go_instrucciones.SetActive(false);
    }

    public void ActivarCoches()
    {
        go_coches.SetActive(true);
    }

    public void DesactivarCoches()
    {
        go_coches.SetActive(false);
    }

    public void selectCoche(int num)
    {
        //listaCoches.Add(cc_amarillo);
        //listaCoches.Add(cc_rojo);
        //listaCoches.Add(cc_negro);
        //listaCoches.Add(cc_azul);

        switch (num)
        {
            case 1:
                Debug.Log("coche1 = " + cc_amarillo.name);
                go_coche = cc_amarillo;
                
                
                Debug.Log("coche2 = " + cc_amarillo.name);
                break;
            case 2:
                s_nomCoche = "car_red"; 
                break;
            case 3:
                s_nomCoche = "car_negro";
                break;
            case 4:
                s_nomCoche = "car_azul";
                break;
        }
        //go_coches.SetActive(false);
      


    }

}
