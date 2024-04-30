using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class CicloDianNoche : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;       // Referencia a la variable del tiepo GameManager          
    [SerializeField] private SpriteRenderer fondoNoche;             // Referencia a la variable Sprite del fondo noche
    [SerializeField] private SpriteRenderer fondoDia;            // Referencia a la variable tilemap del fondo día
    
    [SerializeField] private Light luz;                     // Referencia a la variable del tipo Light

    [SerializeField] private AnimationCurve transicionCurve; // Referencia a la variable del tipo Curva de interpolación para la transición

    private float transicionDuracion;                   // Referencia a la variable duración de la transición entre fondos
    private float transicionTimer = 0.0f;               // Referencia a la variable temporizador de la transición
    private float tiempoGalaxiaActual;                  // Referencia a variable tiempo de galaxia actual
    private readonly float intensidadMax = 1.0f;        // Referencia a la variable intensidad máxima de la luz
    private readonly float intensidadMin = 0.1f;        // Referencia a la variable intensidad mínima de la luz
    
    private bool enTransicion;          // Referencia a la variable bandera para indicar si está en transición
    private bool enNoche;

    void Start()
    {

        enTransicion = true;
        
        fondoNoche.color = new Color(1f, 1f, 1f, 1f);
        fondoDia.color = new Color(1f, 1f, 1f, 0f);
        
    }

    void Update()
    {
        // Obtiene el tiempo de la galaxia actual, desde GameManager
        tiempoGalaxiaActual = gameManager.EstadoTiempoGalaxiaActual();
        enNoche = gameManager.EstadoEnNoche();

        // Calcula la duración de la transición como el 20% del tiempo de la galaxia actual
        transicionDuracion = tiempoGalaxiaActual * 0.2f;


        
        // Si está en transición y se cumple la condición para comenzar la transición de noche a día
        if (enNoche == true && enTransicion == true)
        {

           // Debug.Log("tiempoJuego = " + tiempoJuego + " tiempoGalaxiaRecorrido = " + tiempoGalaxiaRecorrido);
            //Debug.Log("noche a día");
            TransicionNocheADia();

        }
        else if(enNoche == false && enTransicion == false)
        {
           // Debug.Log("tiempoJuego = " + tiempoJuego + " tiempoGalaxiaRecorrido = " + tiempoGalaxiaRecorrido);
            //Debug.Log("día a noche");
            TransicionDiaANoche();
           
        }
        
    }

    void TransicionNocheADia()
    {
        transicionTimer += Time.deltaTime;

        // Usa la curva de interpolación para suavizar la transición
        float t = transicionTimer / transicionDuracion;
        float lerpValue = transicionCurve.Evaluate(t);

        // Ajusta la transparencia de los fondos según la transición
        fondoNoche.color = new Color(1f, 1f, 1f, 1 - lerpValue);
        fondoDia.color = new Color(1f, 1f, 1f, lerpValue);

        // Ajusta la intensidad de la luz según el estado de noche
        luz.intensity = intensidadMax - (intensidadMax - intensidadMin) * lerpValue;

        // Si la transición ha terminado, finaliza la transición
        if (transicionTimer >= transicionDuracion)
        {
            enTransicion = false;

            transicionTimer = 0.0f;
        }
    }

    void TransicionDiaANoche()
    {
        transicionTimer += Time.deltaTime;

        // Usa la curva de interpolación para suavizar la transición
        float t = transicionTimer / transicionDuracion;
        float lerpValue = transicionCurve.Evaluate(t);

        // Ajusta la transparencia de los fondos según la transición
        fondoNoche.color = new Color(1f, 1f, 1f, lerpValue);
        fondoDia.color = new Color(1f, 1f, 1f, 1 - lerpValue);

        // Ajusta la intensidad de la luz según el estado de noche
        luz.intensity = intensidadMin + (intensidadMax - intensidadMin) * lerpValue;

        // Si la transición ha terminado, finaliza la transición
        if (transicionTimer >= transicionDuracion)
        {
            enTransicion = true;

            transicionTimer = 0.0f;
        }
    }
}
