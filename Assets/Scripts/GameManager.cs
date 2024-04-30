using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private ControlVidaNave controlVidaNave;       // Referencia al script ControlVidaNave
    [SerializeField] private float tiempoGalaxiaMin;                // Referencia variable tiempo de juego entre galaxias
    [SerializeField] private float tiempoIncrementoGalaxia;         // Referencia variable tiempo de incremento de cada galaxia
    [SerializeField] private int numeroGalaxiaMax;                  // Referencia variable número máximo de galaxias a jugar

    private float escalaTiempo;                 // Referencia variable escala del tiempo
    private bool juegoCorriendo;                // Referencia variable de activación de juego
    private bool gameOver;                      // Referencia variable de activación fin del juego 
    private bool nivel;                         // Referencia variable de activación de nuevo nivel
    private bool ganaste;                       // Referencia variable ganaste = true (ganaste), flase (perdiste)
    private bool enNoche;
    private float tiempoJuego;                  // Referencia variable tiempo transcurrido en el juego
    private float tiempoGalaxiaRecorrido;       // Referencia variable tiempo de recorrido de la galaxia
    private int numeroGalaxiaActual;            // Referencia variable que cuenta el numero de galaxia o nivel del juego
    private float tiempoGalaxiaActual;          // Referencia variable tiempo que dura una galaxia actual

    // Start is called before the first frame update
    void Start()
    {
        IniciaJuego();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (nivel == false || gameOver == false)
        {
            // Actualiza el tiempo: clic en T (retrasa el tiempo), clic en pausa o Escape (pausa el juego)
            ActualizaTiempoJuego();
            VerificaTiempoGalaxia();
            VerificaNumeroGalaxia();
            VerificaVidaNave();
        }
        else
        {
            CambiaEstadoJuego();
        }
        
    }

    // Gestiona las variables en Start
    private void IniciaJuego()
    {
        escalaTiempo = 1f;
        Time.timeScale = escalaTiempo;
        tiempoJuego = 0f;
        tiempoGalaxiaActual = tiempoGalaxiaMin;
        tiempoGalaxiaRecorrido = tiempoGalaxiaActual;
        juegoCorriendo = true;
        gameOver = false;
        nivel = false;
        enNoche = false;
        numeroGalaxiaActual = 1;
    }
    
    // Gestiona el tiempo de juego
    private void ActualizaTiempoJuego()
    {
        // Si el juego está corriendo => incrementa el tiempo del juego
        if (juegoCorriendo)
        {
            // Actualiza el tiempo de jeugo
            tiempoJuego += Time.deltaTime * Time.timeScale;
            
            //Debug.Log(tiempoJuego);
            
            //Si clic en T => ralentizar el tiempo
            //Sino clic en T => reestablece al tiempo normal
            if (Input.GetKeyDown(KeyCode.T))
            {
                escalaTiempo = 0.3f;
                Time.timeScale = escalaTiempo; // Ajustar Time.timeScale según clicT
                Debug.Log(escalaTiempo);
            }
            // Restablecer Time.timeScale y clicT cuando se libera la tecla T
            else if (Input.GetKeyUp(KeyCode.T))
            {
                escalaTiempo = 1f;
                Time.timeScale = escalaTiempo;

            }


        }

        // Si no hay cambio de nivel o el jeugo terminó => se puede usar escape
        if (nivel == false && gameOver == false)
        {
            // Si clic en tecla escape => pausa el juego
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CambiaEstadoJuego();
            }
        }

    }

    // Gestiona el cambio de estado del juego (corriendo o pausa)
    public void CambiaEstadoJuego()
    {
        juegoCorriendo = !juegoCorriendo;
        
        // Si juegoCorriendo = true => el juego está corriendo
        // Sino => el juego está pausado
        if (juegoCorriendo)
        {
            escalaTiempo = 1f;
            Time.timeScale = escalaTiempo;
        }
        else
        {
            // Activa el panel de pausa
            escalaTiempo = 0f;
            Time.timeScale = escalaTiempo;
        }
    }

    
    //
    private void VerificaTiempoGalaxia()
    {
        
        // Si el tiempo de juego ha superado el tiempo mínimo de Galaxia * el número de galaxia actual => incrementa el número de galaxia en 1 y el tiempo de la galaxia en +10 segundos
        if (tiempoJuego >= tiempoGalaxiaRecorrido)
        {
            // incrementa el número de galaxia en 1
            numeroGalaxiaActual ++;

            // incrementa el tiempo de la galaxia actual en 10 segundos el tiempo de la 
            tiempoGalaxiaActual += tiempoIncrementoGalaxia;
            
            // tiempo en recorrer la galaxia = tiempo recorrido + tiempo de la galaxia actual
            tiempoGalaxiaRecorrido = tiempoGalaxiaActual + tiempoGalaxiaRecorrido;

            //Debug.Log("numeroGalaxiaActual = " + numeroGalaxiaActual + " tiempoGalaxiaActual = " + tiempoGalaxiaActual + " tiempoGalaxiaRecorrido = "+ tiempoGalaxiaRecorrido);
            
            // Cambia flags de gestión
            juegoCorriendo = false;
            nivel = true;
            gameOver = false;
            enNoche = false;
        }

        if (tiempoJuego >= (tiempoGalaxiaRecorrido - tiempoGalaxiaActual + tiempoGalaxiaActual * 0.5f))
        {
            enNoche = true;
        }



    }

    // Verifica la galaxia
    private void VerificaNumeroGalaxia()
    {
        // Si el numero de galaxia actual > al número máximo de galaxias => se termina el juego
        if (numeroGalaxiaActual >= numeroGalaxiaMax)
        {
            // termina el juego
            juegoCorriendo = false;
            nivel = false;
            gameOver = true;
            ganaste = true;
        }
    }

    // Gestiona la actualización de la vida de la nave
    private void VerificaVidaNave()
    {
        float vidaNave = controlVidaNave.EstadoVidaNave();

        // Si vida de la nave = 0 => se detiene el juego (perdiste)
        if (vidaNave <= 0)
        {
            juegoCorriendo = false;
            nivel = false;
            gameOver = true;
            ganaste = false;
        }
    }

    // Gestiona el incremento del tiempo de juego cuando la Nave colisiona con un portal tiempo
    public void IncrementarTiempoJuego()
    {
        // Incrementa el tiempo de juego
        tiempoJuego *= 1.1f;
        
        // Incrementa el tiempo de de la galaxia actual
        tiempoGalaxiaActual *= 1.1f;

        // Incrementa el tiempo de recorrido de la galaxia
        tiempoGalaxiaRecorrido *= 1.1f;
        
    }

    // Devuelve el valor del tiempo jugado
    public float EstadoTiempoJuego()
    {
        return tiempoJuego;
    }

    // Devuelve el valor del estado del juego
    public bool EstadoJuego()
    {
        return juegoCorriendo;
    }

    // Devuelve el valor de la escala de tiempo
    public float EscalaTiempo()
    {
        return escalaTiempo;
    }

    // Devuelve el número de galaxia actual
    public int EstadoGalaxiaActual()
    {
        return numeroGalaxiaActual;
    }

    // Devuelve el número máximo de galaxias
    public int EstadoGalaxiaMax()
    {
        return numeroGalaxiaMax;
    }

    // Devuelve el estado de Game Over
    public bool EstadoGameOver()    
    {
        return gameOver;
    }

    // Actualiza el estado de Game Over
    public void SetGameOver(bool _gameOver)
    {
        gameOver = _gameOver;

        /*
       * Inicia las variables del juego
       * Obtiene el índice de la escena actual
       * Calcula el índice de la siguiente escena en orden
       * Carga la siguiente escena en orden
       */

        if (gameOver == false)
        {
            IniciaJuego();

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            int nextSceneIndex = (currentSceneIndex - 1) % SceneManager.sceneCountInBuildSettings;

            SceneManager.LoadScene(nextSceneIndex);

        }
    }

    // Devuelve el estado del nivel de juego
    public bool EstadoNivel()
    {
        return nivel;
    }

    // Actualiza el valor de nivel
    public void SetNivel(bool _nivel)
    {
        nivel = _nivel;
        juegoCorriendo = true;
        gameOver = false;
    }

    // Devuelve el estado de vida de la nave
    public float EstadoVidaNave()
    {
        return controlVidaNave.EstadoVidaNave();
    }

    // Devuelve el estado de tiempo de la galaxia actual
    public float EstadoTiempoGalaxiaActual()
    {
        return tiempoGalaxiaActual;
    }

    public bool EstadoGanaste()
    {
        return ganaste;
    }

    // Devuelve tiempo de galaxia recorrido
    public float EstadoTiempoGalaxiarecorrido()
    {
        return tiempoGalaxiaRecorrido;
    }

    public bool EstadoEnNoche()
    {
        return enNoche;
    }    
}
