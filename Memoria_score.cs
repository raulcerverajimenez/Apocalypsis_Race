using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Memoria_score : MonoBehaviour
{
    private string s_nombrePrefName;


    public void escribir(string s_nombre)
    {
        PlayerPrefs.SetString(s_nombrePrefName, s_nombre);  
    }

    public string leer()
    {
        return PlayerPrefs.GetString(s_nombrePrefName, "Alias");
    }
}
