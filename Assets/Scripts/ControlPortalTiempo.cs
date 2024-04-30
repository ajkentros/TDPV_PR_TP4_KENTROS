using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPortalTiempo : MonoBehaviour
{
    [SerializeField] private GameObject portalTiempoPrefab;         // Referencia a la variable del tipo Prefab del meteorito
    [SerializeField] private GameManager gameManager;               // Referencia a la variable del tipo GameManager
    [SerializeField] private float escalaGeneracionPortalTiempo;    // Referencia a la variable escala de generacion
    private float tiempoJuego;                 // Referencia a la variable tiempo de juego
    private float tiempoGalaxiaActual;         // Referencia variable tiempo de juego entre galaxias
    private float tiempoGalaxiaRecorrido;      // Referencia a la variable tiempo de la última generación galaxia
    private int cantidadPortalTiempo;          // Referencia a la variable cantidad de portal tiempo a generar
    private bool portalTiempoGenerado;         // Referencia a la variable que habilita la generación del portal tiempo

    private void Start()
    {
        IniciaStart();
    }

    void Update()
    {
        // Llama al método que verifica en qué momento generar un portal de tiempo
        VerificaGeneracionPortaTiempo();
    }

    // Inicia las variables en Start()
    private void IniciaStart()
    {
        cantidadPortalTiempo = 1;
        portalTiempoGenerado = true;
    }

    // Gestiona la gerenación de meteroritos en función del tiempo
    private void VerificaGeneracionPortaTiempo()
    {
        // Toma el tiempo de juego y de la galaxia de Game Manager
        tiempoJuego = gameManager.EstadoTiempoJuego();
        tiempoGalaxiaActual = gameManager.EstadoTiempoGalaxiaActual();
        tiempoGalaxiaRecorrido = gameManager.EstadoTiempoGalaxiarecorrido();

        // Establece intevalo de generación al 60% y el 700% del tiempo de la galaxia actual
        float intervaloGeneracionMin = tiempoGalaxiaRecorrido - tiempoGalaxiaActual + tiempoGalaxiaActual * escalaGeneracionPortalTiempo;
        float intervaloGeneracionMax = tiempoGalaxiaRecorrido - tiempoGalaxiaActual + tiempoGalaxiaActual * (escalaGeneracionPortalTiempo + 0.1f);

        // Si el tiempo de juego = tiempo de la ultima galaxia + intervalo de generación => generar portal vida
        // Si el tiempo de juego = tiempo de la ultima galaxia + intervalo de generación => generar portal vida
        if (tiempoJuego >= intervaloGeneracionMin && tiempoJuego <= intervaloGeneracionMax && portalTiempoGenerado == true)
        {
            //Debug.Log(tiempoJuego + " portalTiempo 50 " + " tiempoGalaxiaActual = " + tiempoGalaxiaActual + " tiempoGalaxiaRecorrido = " + tiempoGalaxiaRecorrido);

            // Llama al método que genera poral de tiempo
            //GenerarPortalTiempo();

            // Marca el portal de vida como generado para este intervalo
            portalTiempoGenerado = false;
        }

        // Si el tiempo de juego = tiempo de la ultima galaxia + intervalo de generación => generar portal tiempo
        if (tiempoJuego >= intervaloGeneracionMax && tiempoJuego <= tiempoGalaxiaRecorrido && portalTiempoGenerado == false)
        {
            //Debug.Log(tiempoJuego + " portalTiempo 80 " + " tiempoGalaxiaActual = " + tiempoGalaxiaActual + " tiempoGalaxiaRecorrido = " + tiempoGalaxiaRecorrido);

            // Llama al método que genera poral de tiempo
            //GenerarPortalTiempo();

            portalTiempoGenerado = true;

        }
    }

    // Gestiona la generación de meteorito
    private void GenerarPortalTiempo()
    {
        for (int i = 0; i< cantidadPortalTiempo; i++)
        {
            // Instancia la variable posX como posición aleatoria en el eje X entre límites y el eje Y
            float posX = Random.Range(-24f, 24f);
            float posY = Random.Range(15f, 20f);
            
            // Crea el meteorito en la posición
            GameObject portalTiempo = Instantiate(portalTiempoPrefab, new Vector2(posX, posY), Quaternion.identity);

            // Asigna el controlador al meteorito
            portalTiempo.GetComponent<PortalTiempo>().SetControlador(this);
        }
        
    }
    
    // Gestiona la destrucción del meteorito
    public void DestruirPortaTiempo(GameObject portalTiempo)
    {
        Destroy(portalTiempo);
    }

  
}
