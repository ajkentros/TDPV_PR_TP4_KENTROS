using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlNave : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;                   // Referencia a la variable GameManager
    [SerializeField] private ControlVidaNave controlVidaNave;           // Referencia al script ControlVidaNave
    [SerializeField] private ControlMeteoritos controlMeteoritos;       // Referencia al game object del tipo ControlMeteoritos

    private Vector2 posicionNave;                   // Variable referencia para guardar la posici�n original del navePrefab
    private readonly float velocidadNormal = 25f;   // Referencia velocidad normal de la nave
    private float velocidadActual;                  // Referencia variable velocidad actual de la nave
  
    private readonly float minX = -24.9f;       // L�mite m�nimo en el eje X
    private readonly float maxX = 24.9f;        // L�mite m�ximo en el eje X
    private readonly float minY = -11f;         // L�mite m�nimo en el eje Y
    private readonly float maxY = 14f;          // L�mite m�ximo en el eje Y




    void Start()
    {
        
        IniciaControlNave();
    }

    void Update()
    {
        // Si se usa las teclas Horizontal y vertical
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Si se utiliza el mouse para mover al jugador, ajustamos el movimiento
        Vector2 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            movimientoHorizontal = Mathf.Clamp(posicionMouse.x - transform.position.x, -2f, 2f);
            movimientoVertical = Mathf.Clamp(posicionMouse.y - transform.position.y, -2f, 2f);
        }

        // Calcula la velocidad ajustada seg�n la escala del tiempo
        float escalaTiempo = gameManager.EscalaTiempo();
        
        if(escalaTiempo != 0f)
        {
            velocidadActual = velocidadNormal / gameManager.EscalaTiempo();
        }
        else
        {
            velocidadActual = 0f;
        }

        //Si clic en V => aumenta la velocidad de la nave un 10%
        //Sino clic en V => reestablece la velocidad normal de la nave
        if (Input.GetKeyDown(KeyCode.V))
        {
           
            velocidadActual = velocidadNormal * 1.2f;
        }
        // Restablecer Time.timeScale y clicT cuando se libera la tecla T
        else if (Input.GetKeyUp(KeyCode.V))
        {
            velocidadActual = velocidadNormal;

        }

        // Calcula el desplazamiento en los ejes X y Y
        float desplazamientoX = movimientoHorizontal * velocidadActual * Time.deltaTime;
        float desplazamientoY = movimientoVertical * velocidadActual * Time.deltaTime;

        // Calcula la posici�n objetivo
        float nuevaPosX = Mathf.Clamp(transform.position.x + desplazamientoX, minX, maxX);
        float nuevaPosY = Mathf.Clamp(transform.position.y + desplazamientoY, minY, maxY);


        // Actualiza la posici�n del jugador respetando los l�mites
        transform.position = new Vector2(nuevaPosX, nuevaPosY);

    }

    
    // Gestiona el inicio del start()
    public void IniciaControlNave()
    {
        // Obtiene la posici�n del navePrefab
        posicionNave = transform.position;

        // Asigna velocidad de movimiento = velocidad incial
        velocidadActual = velocidadNormal;

        // Instancia el player desde el prefab en la posici�n original
        transform.position = posicionNave;

        // Instancia el Ribidbody del player
        //rb = GetComponent<Rigidbody2D>();


    }

    // Gestiona la colisi�n con Meteoritos
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con un meteorito => decrementar la vida de la nave al seg�n la fuerza del meteorito
        if (collision.gameObject.CompareTag("Meteorito"))
        {
            float fuerzaMeteorito = controlMeteoritos.GetFuerzaMeteorito(); 
            controlVidaNave.DecrementarVida(fuerzaMeteorito); 
        }

        // Si colisiona con un portal vida => incrementa la vida de la nave 
        if (collision.gameObject.CompareTag("PortalVida"))
        {
            controlVidaNave.SetVidaActualNave();
            
        }

        // Si colisiona con un portal tiempo => incrementa el tiempo de navegaci�n (futuro)
        if (collision.gameObject.CompareTag("PortalTiempo"))
        {
            
           gameManager.IncrementarTiempoJuego();
            
        }
    }
}
