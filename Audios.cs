/*
 * Gestiona todos los audios del juego 
 */

using UnityEngine;

public class Audios : MonoBehaviour
{

    AudioSource[] audioSources;
    AudioSource as_cancion, as_disparo, as_caidaArbol, as_destroyArbol,
                    as_caidaPiedra, as_premio, as_golpeCoche, as_motor, as_trueno;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        
        as_cancion = audioSources[0];
        as_disparo = audioSources[1];
        as_caidaArbol = audioSources[2];
        as_destroyArbol = audioSources[3];
        as_caidaPiedra = audioSources[4];
        as_premio = audioSources[5];
        as_golpeCoche = audioSources[6];
        as_motor = audioSources[7];
        as_trueno = audioSources[8];
       
        as_cancion.Play();
        as_motor.Play();
        
        as_motor.loop = true;
    }

    public void Reproductor(int num)
    {
        switch (num)
        {
            case 0:
                as_disparo.Play();
                break;
            case 1:
                as_caidaArbol.Play();
                break;
            case 2:
                as_caidaPiedra.Play();
                break;
            case 3:
                as_destroyArbol.Play();
                break;
            case 4:
                as_golpeCoche.Play();
                break;
            case 5:
                as_premio.Play();
                break;
            case 6:
                as_trueno.Play();
                break;
        }
    }

    //el motor suena en función de la aceleración
    internal void sonidoMotor(float f_km_hora, float f_velocidad_max_m_s)
    {
        as_motor.pitch = (f_km_hora / 3.6f) / f_velocidad_max_m_s + 1;
        as_motor.volume = (f_km_hora / 3.6f) / f_velocidad_max_m_s + 0.2f;
    }
}



