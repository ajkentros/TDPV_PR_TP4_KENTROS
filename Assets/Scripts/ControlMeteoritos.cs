using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMeteoritos : MonoBehaviour
{
    [SerializeField] private GameObject meteoritoPrefab;      // Referencia a la variable del tipo Prefab del meteorito
    [SerializeField] private GameManager gameManager;         // Referencia a la variable del tipo GameManager
    
    private readonly float intervaloGeneracionMin = 2f;    // Referencia a la variable tiempo de generación de espera Mínimo
    private readonly float intervaloGeneracionMax = 2f;    // Referencia a la variable tiempo de generación de espera Máximo
    
    
    private float tiempoJuego;                 // Referencia a la variable tiempo de juego
    private float tiempoGalaxiaActual;         // Referencia a la variable tiempo de la galaxia actual
    private float tiempoUltimaGeneracion;      // Referencia a la variable tiempo de la última generación de meteoritos
    private float intervaloGeneracion;         // Referencia a la variable tiempo de generación de meteoritos
    private int cantidadMeteoritos;            // Referencia a la variable cantidad de meteoritos generados
    private float fuerzaMeteorito;             // Referencia a la variable que define la fuerza de impacto del meoteorito en la nave
    private int numeroGalaxiaActual;           // Referencia a la variable que define el nivel de galaxia o juego
    private float escalaMeteorito;             // Referencia a la variable que define la escala del meteorito

    private void Start()
    {
        IniciaStart();
    }

    void Update()
    {
        
        // Toma el tiempo de juego y de la galaxia de Game Manager
        tiempoJuego = gameManager.EstadoTiempoJuego();
        tiempoGalaxiaActual = gameManager.EstadoTiempoGalaxiaActual();

        // Toma el numero de galaxia actual
        numeroGalaxiaActual = gameManager.EstadoGalaxiaActual();

        VerificaGeneracionMeteoritos();
    }

    // Inicia las variables en Start()
    private void IniciaStart()
    {
        tiempoUltimaGeneracion = 0f;
        intervaloGeneracion = 0f;
        cantidadMeteoritos = 1;
        fuerzaMeteorito = 1f;
    }

    // Gestiona la gerenación de meteroritos en función del tiempo
    private void VerificaGeneracionMeteoritos()
    {
        
        // Define un intervalo de generación de meteoritos aleatorio entre un mín y un máx
        intervaloGeneracion = Random.Range(intervaloGeneracionMin, intervaloGeneracionMax);

        intervaloGeneracion = 1f;
        //Debug.Log(intervaloGeneracion);

        // Si el tiempo de juego - tiempo de la ultima generación > intervalo de generación => generar meteoritos
        if ((tiempoJuego - tiempoUltimaGeneracion >= intervaloGeneracion) && gameManager.EstadoJuego() == true && gameManager.EstadoGameOver() == false && gameManager.EstadoNivel() == false)
        {
            // Actualiza el tiempo de la última generación con el tiempo de juego
            tiempoUltimaGeneracion = tiempoJuego;

            // Calcula el tiempo transcurrido desde la última generación y redondea a valores enteros
            int tiempoTranscurrido = Mathf.RoundToInt(tiempoUltimaGeneracion);

            // Si han pasado 10 segundos del último incremento de meteoritos y el tiempo es menor a 2 segundos antes de que termine la galaxia actual, incrementa en 1 la cantidad de meteoritos a generar
            if (tiempoTranscurrido % 10 == 0 && tiempoUltimaGeneracion <= (tiempoGalaxiaActual - 2f))
            {
                cantidadMeteoritos++;
            }
            
            //Debug.Log("tiempoUltimaGeneracion = " + tiempoUltimaGeneracion + " tiempoTranscurrido = " + tiempoTranscurrido + " tiempoTranscurrido % 10 = " + tiempoTranscurrido % 10);

            //Debug.Log("cantidadMeteoritos = " + cantidadMeteoritos);
            
            // Llama al método que genera meoteoritos
            GenerarMeteorito();

        }
    }

    // Gestiona la generación de meteorito
    private void GenerarMeteorito()
    {
        
        for (int i = 0; i< cantidadMeteoritos; i++)
        {
            // Calcular la escala del meteorito (10%) en función de la galaxia actual
            int escalaMeteoritoMin = 1;
            int escalaMeteoritoMax = 5;
            
            escalaMeteorito = Random.Range(escalaMeteoritoMin, escalaMeteoritoMax);

            // Establece la fuerza de choque del meteorito relacionado al tamaño
            fuerzaMeteorito = numeroGalaxiaActual * escalaMeteorito;

            // Calcular el color del meteorito en función de la galaxia actual
            Color colorMeteorito = Color.Lerp(Color.red, Color.yellow, escalaMeteorito / escalaMeteoritoMax);

            // Instancia la variable posX como posición aleatoria en el eje X entre límites y el eje Y
            float posX = Random.Range(-24f, 24f);
            float posY = Random.Range(15f, 30f);
            
            // Crea el meteorito en la posición
            GameObject meteorito = Instantiate(meteoritoPrefab, new Vector2(posX, posY), Quaternion.identity);

            // Aplica la escala al meteorito
            meteorito.transform.localScale = new (escalaMeteorito, escalaMeteorito, 0);

            // Obtiene el componente Rigidbody del meteorito
            Rigidbody2D meteoritoRigidbody = meteorito.GetComponent<Rigidbody2D>();

            // Multiplica la gravedad por un factor proporcional a la escala del meteorito
            float factorGravedad = - escalaMeteorito / 10; 
            meteoritoRigidbody.gravityScale = Physics2D.gravity.y * factorGravedad;

            //Debug.Log("gravedad meteorito = " + meteoritoRigidbody.gravityScale);

            // Obtiene el componente SpriteRenderer del meteorito
            SpriteRenderer meteoritoSpriteRenderer = meteorito.GetComponent<SpriteRenderer>();

            // Cambia el color del meteorito
            meteoritoSpriteRenderer.color = colorMeteorito;

            // Asigna el controlador al meteorito
            meteorito.GetComponent<Meteorito>().SetControlador(this);
        }
        
    }
    
    // Gestiona la destrucción del meteorito
    public void DestruirMeteorito(GameObject meteorito)
    {
        Destroy(meteorito);
    }

    // Devuelve fuerza del meteorito
    public float GetFuerzaMeteorito()
    {
        return fuerzaMeteorito;
    }
}
