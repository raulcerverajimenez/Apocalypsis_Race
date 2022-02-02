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
    public GameObject go_instrucciones;
    public Text if_alias;

    //cambia de escena
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

    public void ClickMouseBoton()
    {
        btn_boton.image.color = new Color(56, 90, 90, 0);
    }

    public void ActivarIntrucciones()
    {
        go_instrucciones.SetActive(true);
    }

    public void DesactivarIntrucciones()
    {
        go_instrucciones.SetActive(false);
    }
}
