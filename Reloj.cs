/*
 * Crontola el cronómetro
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reloj : MonoBehaviour
{
    public Text txt_tiempo;

    private float f_segundos = 0.0f;
    private int i_minutos = 0;

    private float f_segTotales = 0;
    public float F_segTotales { get => f_segTotales; }

    void Update()
    {
        f_segundos += Time.deltaTime;
        f_segTotales += Time.deltaTime;

        if (f_segundos > 59)
        {
            f_segundos = 0;
            i_minutos++;
        }

        txt_tiempo.text = string.Format("{0:00}", i_minutos) + ":" + string.Format("{0:00}", f_segundos);
    }
}
