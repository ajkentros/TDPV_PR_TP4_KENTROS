using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlVidaNave : MonoBehaviour
{
    //[SerializeField] private Slider vidaNave;                   // Referencia variable del tipo Slider para controlar la vida de la Nave
    [SerializeField] private GameManager gameManager;           // Referencia a la variable GameManager
    //[SerializeField] private TextMeshProUGUI vidaNaveValor;     // Referencia a la variable vida de l nave en valor %

    private float vidaNaveActual;                       // Referencia variable vida actual de la nave
    private readonly float vidaNaveMaxima = 100;        // Referencia variable vida máxima de la nave
    private float tiempoJuego;                          // Referencia variable tiempo de juego
    private float tiempoGalaxiaActual;                        // Referencia variable tiempo de juego entre galaxias
    private float tiempoInicioGalaxia;                  // Referencia tiempo de inicio en la galaxia actual

    private void Start()
    {
        // Llama al método IniciaStart para iniciar variables
        IniciaStart();

    }

    // Gestiona el inicio de variable en Start()
    private void IniciaStart()
    {
        // Toma el tiempo de juego de Game Manager
        tiempoJuego = gameManager.EstadoTiempoJuego();
        tiempoGalaxiaActual = gameManager.EstadoTiempoGalaxiaActual();

        // Define el tiempo de inicia de la galaxia = tiempoJuego
        tiempoInicioGalaxia = tiempoJuego;

        // Inicializa la vida actual
        vidaNaveActual = vidaNaveMaxima;


    }
    private void Update()
    {
        // Toma el tiempo de juego de Game Manager
        tiempoJuego = gameManager.EstadoTiempoJuego();
        tiempoGalaxiaActual = gameManager.EstadoTiempoGalaxiaActual();

        // Llama al método ActulizaVidaNave
        ActualizaVidaNave();

        // Llama al método que actualiza el indicador de vida de la nave
        //ActualizaSlideVidaNave();

    }

    // Gestiona la actualización de la vida de la nave en función del tiempo de la galaxia 
    private void ActualizaVidaNave()
    {
        //Debug.Log("vidaNaveActual = " + vidaNaveActual);
        // Verifica si ha pasado más de un 85% del tiempoGalaxiaActual desde el inicio de la galaxia actual => disminuye la vida de la nave
        if (tiempoJuego - tiempoInicioGalaxia > tiempoGalaxiaActual * 0.45f)
        {
            // Reduce la vida de la nave en un porcentaje específico por galaxia
            float decremento = (tiempoGalaxiaActual * 0.1f);

            // Llama al método que decrementa la vida de la nave
            DecrementarVida(decremento);

            // Actualiza el tiempo de inicio de la galaxia actual
            tiempoInicioGalaxia = tiempoJuego;
        }

        if (vidaNaveActual >= vidaNaveMaxima)
        {
            vidaNaveActual = vidaNaveMaxima;
        }
    }

    // Gestiona el decremento de la vida de la nave
    public void DecrementarVida(float decremento)
    {
        vidaNaveActual -= decremento; //Debug.Log(vidaNaveActual);
        
        if(vidaNaveActual <= 0f)
        {
            vidaNaveActual = 0f;
        }
    }

    // Devuelve el valor actual de vida de la nave 
    public float EstadoVidaNave()
    {
        return vidaNaveActual;
    }

    // Devuelve el valor actual de vida de la nave 
    public float EstadoVidaNavemaxima()
    {
        return vidaNaveMaxima;
    }
    // Gestiona el incremento de vida de la nave cuando colisiona con el portal vida
    public void SetVidaActualNave()
    {
        // Incrementa un 10%
        vidaNaveActual *= 1.1f;
        
    }
}
