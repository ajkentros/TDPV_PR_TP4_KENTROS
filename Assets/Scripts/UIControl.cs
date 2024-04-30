using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class UIControl : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;               // Referencia variable del tipo Game Manager
    [SerializeField] private ControlVidaNave controlVidaNave;       // Referencia variable del tipo ControlVidanave

    [Header("Paneles")]
    [SerializeField] private GameObject panelPausa;                 // Referencia variable panel de pausa
    [SerializeField] private GameObject panelTiempo;                // Referencia variable panel de pausa
    [SerializeField] private GameObject panelNivel;                 // Referencia variable panel de pausa
    [SerializeField] private GameObject panelFinal;                 // Referencia variable panel de pausa

    [Header("campos Panel Juego")]
    [SerializeField] private TextMeshProUGUI tiempoJuego;           // Referencia variable al texto del tiempo
    [SerializeField] private Slider vidaNaveSlider;                 // Referencia variable del tipo Slider para controlar la vida de la Nave
    [SerializeField] private TextMeshProUGUI vidaNaveValor;         // Referencia a la variable vida de l nave en valor %
    [SerializeField] private TextMeshProUGUI numeroGalaxiaJuego;    // Referencia variable al texto de numero de galaxia en el panel juego
    [SerializeField] private TextMeshProUGUI tiempoGalaxia;         // Referencia a la variable vida de la nave en valor %

    [Header("campos Panel Pausa")]
    [SerializeField] private TextMeshProUGUI tiempoPausa;           // Referencia variable al texto del tiempo
    [SerializeField] private TextMeshProUGUI vidaNavePausa;         // Referencia variable al texto de vida de la nave
    [SerializeField] private TextMeshProUGUI numeroGalaxiaPausa;    // Referencia variable al texto de numero de galaxia en el panel pausa
    
    [Header("campos Panel Nivel")]
    [SerializeField] private TextMeshProUGUI tiempoNivel;           // Referencia variable al texto del tiempo
    [SerializeField] private TextMeshProUGUI vidaNaveNivel;         // Referencia variable al texto de vida de la nave
    [SerializeField] private TextMeshProUGUI numeroGalaxiaNivel;    // Referencia variable al texto de numero de galaxia en el panel pausa
    [SerializeField] private TextMeshProUGUI numeroGalaxiaNivel2;   // Referencia variable al texto de numero de galaxia en el panel pausa

    [Header("campos Panel Final")]
    [SerializeField] private TextMeshProUGUI tituloFinal;           // Referencia variable al texto del tiempo
    [SerializeField] private TextMeshProUGUI tiempoFinal;           // Referencia variable al texto del tiempo
    [SerializeField] private TextMeshProUGUI vidaNaveFinal;         // Referencia variable al texto de vida de la nave
    [SerializeField] private TextMeshProUGUI numeroGalaxiaFinal;    // Referencia variable al texto de numero de galaxia en el panel pausa


    private float tiempo;          // Referencia variable tiempoJuego
 

    // Start is called before the first frame update
    void Start()
    {
        IniciaStart();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //ActualizarPanelJuego();
        ActualizaUI();
        

    }
    // Inicia variables en Start
    private void IniciaStart()
    {
        // Desactiva el canvas de pausa
        panelTiempo.SetActive(true);

        // Desactiva el canvas de pausa
        panelPausa.SetActive(false);

        // Desactiva el canvas de nivel
        panelNivel.SetActive(false);

        // Desactiva el canvas de nivel
        panelFinal.SetActive(false);

    }

    // Actuliza el canvas
    private void ActualizaUI()
    {
        // Toma el tiempo de juego del Game Manager
        tiempo = gameManager.EstadoTiempoJuego();

        // Define la variable que verifica si el juego está corriendo o en pausa
        bool juegoCorriendo = gameManager.EstadoJuego();

        // Define la variable que verifica si el juego pasa de nivel
        bool nivel = gameManager.EstadoNivel();

        // Define la variable que verifica si el juego terminó
        bool gameOver = gameManager.EstadoGameOver();

        // Si el juego está corriendo => visiviliza el pane de juego
        if (juegoCorriendo == true && nivel == false && gameOver == false)
        {
            // Activa el panel de pausa
            panelTiempo.SetActive(true);        // Activa panel de tiempo
            panelPausa.SetActive(false);     
            panelNivel.SetActive(false);
            panelFinal.SetActive(false);

            // Llama al método que actualiza el panel de juego
            ActualizaPanelJuego();

        }
        
        // Si el juego no está corriendo y está en pausa => visibiliza el panel de pausa
        if (juegoCorriendo == false && nivel == false && gameOver == false)
        {
            // Activa el panel de pausa
            panelTiempo.SetActive(true);
            panelPausa.SetActive(true);     // Activa panel de pausa
            panelNivel.SetActive(false);
            panelFinal.SetActive(false);

            // LLama al método para actualizar panel pausa
            ActualizaPanelPausa();
        }
        
        // Sino si el juego no está corriendo y no está en pausa y el nivel se superó => visibiliza el panel de nivel
        if (juegoCorriendo == false && nivel == true && gameOver == false)
        {
            // Activa el panel de nivel
            panelTiempo.SetActive(false);
            panelPausa.SetActive(false);
            panelNivel.SetActive(true);     // Activa panel de nivel
            panelFinal.SetActive(false);

            // LLama al método para actualizar panel nivel
            ActualizaPanelNivel();
        }
        // Sino si el juego terminó porque el jugador perdió o ganó => se activa el panel final
        else if(juegoCorriendo == false && nivel == false && gameOver == true)
        {
            // Desactiva los paneles del canvas
            panelTiempo.SetActive(false);
            panelPausa.SetActive(false);
            panelNivel.SetActive(false);
            panelFinal.SetActive(true);     // Activa panel final
            
            // Llama al método para gestionar el panel Final
            ActualizaPanelFinal();
        }

    }
    
    // Gestiona la actualización del panel juego
    private void ActualizaPanelJuego()
    {

        // Formatea el tiempo en minutos y segundos
        tiempoJuego.text = FormatoTiempo(tiempo);

        // Actualiza el color del panel del tiempo
        Image colorPanelTiempo = panelTiempo.GetComponent<Image>();

        // Si la escala de tiempo no es 1f => el color del panel del tiempo se convierte en azul (indicando que la velocidad es lenta)
        // Sino => el color por defoult es verde
        if (gameManager.EscalaTiempo() == 1f)
        {
            // Establece el color de fondo a verde (juego corriendo)
            colorPanelTiempo.color = Color.green;
            tiempoJuego.color = Color.black;

        }
        else if (gameManager.EscalaTiempo() == 0.3f)
        {
            // Establece el color de fondo a azul
            colorPanelTiempo.color = Color.blue;
            tiempoJuego.color = Color.white;

        }
        else if (!gameManager.EstadoJuego())
        {
            // Establece el color de fondo a azul
            colorPanelTiempo.color = Color.black;
            tiempoJuego.color = Color.white;
        }

        // Toma el valor de la galaxia o nivel de juego desde Game Manager
        numeroGalaxiaJuego.text = gameManager.EstadoGalaxiaActual().ToString();

        // Toma el valor del tiempo de la galaxia actual y lo muestra en el juego
        tiempoGalaxia.text = gameManager.EstadoTiempoGalaxiaActual().ToString("F2");

        // Llama al método que actualiza el indicador de vida de la nave
        ActualizaSlideVidaNave();
    }

    // Gestiona la actualización del panel final
    private void ActualizaPanelFinal()
    {
        // Actualiza el TextMeshPro con el tiempo formateado
        tiempoFinal.text = FormatoTiempo(tiempo);

        // Actualiza el texto de vida de la nave
        vidaNaveFinal.text = $"{controlVidaNave.EstadoVidaNave().ToString("F0"):F0}%";

        // Toma el valor de la galaxia o nivel de juego desde Game Manager
        numeroGalaxiaFinal.text = gameManager.EstadoGalaxiaActual().ToString();

        // Si el jugador gano => muestra resultado obtenido y el botón Menu

        if (gameManager.EstadoGanaste())
        {
            // Muestra el panel de GANASTE

            tituloFinal.text = "GANASTE";
        }
        else
        {
            // Muestra el panel de PERDISTE
            tituloFinal.text = "PERDISTE";
        }
    }

    // Gestiona la actualización del panel Nivel
    private void ActualizaPanelNivel()
    {
        // Actualiza el TextMeshPro con el tiempo formateado
        tiempoNivel.text = FormatoTiempo(tiempo);

        // Actualiza el texto de vida de la nave
        vidaNaveNivel.text = $"{controlVidaNave.EstadoVidaNave().ToString("F0"):F0}%";

        // Toma el valor de la galaxia o nivel de juego desde Game Manager
        numeroGalaxiaNivel.text = (gameManager.EstadoGalaxiaActual() - 1).ToString();
        numeroGalaxiaNivel2.text = gameManager.EstadoGalaxiaActual().ToString();
    }

    // Gestiona la actualización del panel Pausa
    private void ActualizaPanelPausa()
    {
        // Actualiza el TextMeshPro con el tiempo formateado
        tiempoPausa.text = FormatoTiempo(tiempo);

        // Actualiza el texto de vida de la nave
        vidaNavePausa.text = $"{controlVidaNave.EstadoVidaNave().ToString("F0"):F0}%";

        // Toma el valor de la galaxia o nivel de juego desde Game Manager
        numeroGalaxiaPausa.text = gameManager.EstadoGalaxiaActual().ToString();
    }

    // Gestiona la actualización de la vida de la nave
    private void ActualizaSlideVidaNave()
    {
        // El valor del slider = a la vida de la nave
        vidaNaveSlider.value = controlVidaNave.EstadoVidaNave();

        // Llama al método que controla el color del slider en función de la vida de la nave
        ActualizaColorSlider();

        // Actualiza el campo vidaNaveValor en %
        vidaNaveValor.text = $"{((controlVidaNave.EstadoVidaNave() / controlVidaNave.EstadoVidaNavemaxima()) * 100f).ToString("F0"):F0}%";
    }

    // Gestiona la actulización del color de la barra de vida de la nave
    private void ActualizaColorSlider()
    {
        // Accede al componente Image del fill
        Image fillImage = vidaNaveSlider.fillRect.GetComponent<Image>();

        // Si la vida actual < 20% vida máxima => color rojo
        // Sino Si la vida actual < 80% vida máxima => color amarillo
        // Sino color verde
        if (controlVidaNave.EstadoVidaNave() < controlVidaNave.EstadoVidaNavemaxima() * 0.2f)
        {
            fillImage.color = Color.red;
        }
        else if (controlVidaNave.EstadoVidaNave() < controlVidaNave.EstadoVidaNavemaxima() * 0.8f)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.green;
        }
    }
    // Genera cálculo de tiempo
    private string FormatoTiempo(float tiempo)
    {
        // Formatea el tiempo en minutos y segundos
        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        int milisegundos = Mathf.FloorToInt((tiempo * 100) % 100);

        return string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, milisegundos); ;
    }

    // Gestiona la acción del botón continuar
    public void BotonContinuar()
    {
        // Setea nivel = false para continuar con el siguiente nivel
        gameManager.SetNivel(false);
    }

    // Gestiona la accción del botón Manu
    public void BotonMenu()
    {
        // Setea gameOver = true para iniciar variables del juego
        gameManager.SetGameOver(false);


    }
}
