using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPortalVida : MonoBehaviour
{
    [SerializeField] private GameObject portalVidaPrefab;           // Referencia a la variable del tipo Prefab del meteorito
    [SerializeField] private GameManager gameManager;               // Referencia a la variable del tipo GameManager
    [SerializeField] private float escalaGeneracionPortalVida;    // Referencia a la variable escala de generacion

    private float tiempoJuego;                 // Referencia a la variable tiempo de juego
    private float tiempoGalaxiaActual;         // Referencia variable tiempo de juego entre galaxias
    private float tiempoGalaxiaRecorrido;      // Referencia a la variable tiempo de la última generación galaxia
    private int cantidadPortalVida;            // Referencia a la variable cantidad de portal vida a generar
    private bool portalVidaGenerado;           // Referencia a la variable que habilita la generación del portal


    private void Start()
    {
        IniciaStart();
    }

    void Update()
    {
        // LLama al método que verifica en qué momento genrerar un portal de vida
        VerificaGeneracionPortalVida();
    }

    // Inicia las variables en Start()
    private void IniciaStart()
    {
        cantidadPortalVida = 1;
        portalVidaGenerado = true;

    }

    // Gestiona la gerenación de meteroritos en función del tiempo
    private void VerificaGeneracionPortalVida()
    {
        // Toma el tiempo de juego y de la galaxia de Game Manager
        tiempoJuego = gameManager.EstadoTiempoJuego();
        tiempoGalaxiaActual = gameManager.EstadoTiempoGalaxiaActual();
        tiempoGalaxiaRecorrido = gameManager.EstadoTiempoGalaxiarecorrido();

        // Establece intevalo de generación al 50% y el 80% del tiempo de la galaxia actual
        float intervaloGeneracionMin = tiempoGalaxiaRecorrido - tiempoGalaxiaActual + tiempoGalaxiaActual * escalaGeneracionPortalVida;
        float intervaloGeneracionMax = tiempoGalaxiaRecorrido - tiempoGalaxiaActual + tiempoGalaxiaActual * (escalaGeneracionPortalVida + 0.3f);

        
        // Si el tiempo de juego = tiempo de la ultima galaxia + intervalo de generación => generar portal vida
        if (tiempoJuego >= intervaloGeneracionMin && tiempoJuego <= intervaloGeneracionMax && portalVidaGenerado == true)
        {
            //Debug.Log(tiempoJuego + " portalVida 50 " + " tiempoGalaxiaActual = " + tiempoGalaxiaActual + " tiempoGalaxiaRecorrido = " + tiempoGalaxiaRecorrido);
          
            // Llama al método que genera poral de vida
            //GenerarPortalVida();

            portalVidaGenerado = false;
        }

        // Si el tiempo de juego = tiempo de la ultima galaxia + intervalo de generación => generar portal tiempo
        if (tiempoJuego >= intervaloGeneracionMax && tiempoJuego <= tiempoGalaxiaRecorrido && portalVidaGenerado == false)
        {
            //Debug.Log(tiempoJuego + " portalVida 80 " + " tiempoGalaxiaActual = " + tiempoGalaxiaActual + " tiempoGalaxiaRecorrido = " + tiempoGalaxiaRecorrido);

            // Llama al método que genera poral de vida
            //GenerarPortalVida(); 

        
            portalVidaGenerado = true;

        }
    }

    // Gestiona la generación de meteorito
    private void GenerarPortalVida()
    {
        
        for (int i = 0; i< cantidadPortalVida; i++)
        {
            // Instancia la variable posX como posición aleatoria en el eje X entre límites y el eje Y
            float posX = Random.Range(-24f, 24f);
            float posY = Random.Range(15f, 20f);
            
            // Crea el meteorito en la posición
            GameObject portalVida = Instantiate(portalVidaPrefab, new Vector2(posX, posY), Quaternion.identity);

            // Asigna el controlador al meteorito
            portalVida.GetComponent<PortalVida>().SetControlador(this);

        }
        
    }
    
    // Gestiona la destrucción del meteorito
    public void DestruirPortalVida(GameObject portalVida)
    {
        Destroy(portalVida);
    }


}
