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
    private float tiempoGalaxiaRecorrido;      // Referencia a la variable tiempo de la �ltima generaci�n galaxia
    private int cantidadPortalTiempo;          // Referencia a la variable cantidad de portal tiempo a generar
    private bool portalTiempoGenerado;         // Referencia a la variable que habilita la generaci�n del portal tiempo

    private void Start()
    {
        IniciaStart();
    }

    void Update()
    {
        // Llama al m�todo que verifica en qu� momento generar un portal de tiempo
        VerificaGeneracionPortaTiempo();
    }

    // Inicia las variables en Start()
    private void IniciaStart()
    {
        cantidadPortalTiempo = 1;
        portalTiempoGenerado = true;
    }

    // Gestiona la gerenaci�n de meteroritos en funci�n del tiempo
    private void VerificaGeneracionPortaTiempo()
    {
        // Toma el tiempo de juego y de la galaxia de Game Manager
        tiempoJuego = gameManager.EstadoTiempoJuego();
        tiempoGalaxiaActual = gameManager.EstadoTiempoGalaxiaActual();
        tiempoGalaxiaRecorrido = gameManager.EstadoTiempoGalaxiarecorrido();

        // Establece intevalo de generaci�n al 60% y el 700% del tiempo de la galaxia actual
        float intervaloGeneracionMin = tiempoGalaxiaRecorrido - tiempoGalaxiaActual + tiempoGalaxiaActual * escalaGeneracionPortalTiempo;
        float intervaloGeneracionMax = tiempoGalaxiaRecorrido - tiempoGalaxiaActual + tiempoGalaxiaActual * (escalaGeneracionPortalTiempo + 0.1f);

        // Si el tiempo de juego = tiempo de la ultima galaxia + intervalo de generaci�n => generar portal vida
        // Si el tiempo de juego = tiempo de la ultima galaxia + intervalo de generaci�n => generar portal vida
        if (tiempoJuego >= intervaloGeneracionMin && tiempoJuego <= intervaloGeneracionMax && portalTiempoGenerado == true)
        {
            //Debug.Log(tiempoJuego + " portalTiempo 50 " + " tiempoGalaxiaActual = " + tiempoGalaxiaActual + " tiempoGalaxiaRecorrido = " + tiempoGalaxiaRecorrido);

            // Llama al m�todo que genera poral de tiempo
            //GenerarPortalTiempo();

            // Marca el portal de vida como generado para este intervalo
            portalTiempoGenerado = false;
        }

        // Si el tiempo de juego = tiempo de la ultima galaxia + intervalo de generaci�n => generar portal tiempo
        if (tiempoJuego >= intervaloGeneracionMax && tiempoJuego <= tiempoGalaxiaRecorrido && portalTiempoGenerado == false)
        {
            //Debug.Log(tiempoJuego + " portalTiempo 80 " + " tiempoGalaxiaActual = " + tiempoGalaxiaActual + " tiempoGalaxiaRecorrido = " + tiempoGalaxiaRecorrido);

            // Llama al m�todo que genera poral de tiempo
            //GenerarPortalTiempo();

            portalTiempoGenerado = true;

        }
    }

    // Gestiona la generaci�n de meteorito
    private void GenerarPortalTiempo()
    {
        for (int i = 0; i< cantidadPortalTiempo; i++)
        {
            // Instancia la variable posX como posici�n aleatoria en el eje X entre l�mites y el eje Y
            float posX = Random.Range(-24f, 24f);
            float posY = Random.Range(15f, 20f);
            
            // Crea el meteorito en la posici�n
            GameObject portalTiempo = Instantiate(portalTiempoPrefab, new Vector2(posX, posY), Quaternion.identity);

            // Asigna el controlador al meteorito
            portalTiempo.GetComponent<PortalTiempo>().SetControlador(this);
        }
        
    }
    
    // Gestiona la destrucci�n del meteorito
    public void DestruirPortaTiempo(GameObject portalTiempo)
    {
        Destroy(portalTiempo);
    }

  
}
