using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTiempo : MonoBehaviour
{
    private ControlPortalTiempo controlador;    // Referencia variable del tipo ControlPortalTiempo

    // Gestiona la instancia del game object del tipo ControlPortalTiempo que recibe
    public void SetControlador(ControlPortalTiempo _controlador)
    {
        controlador = _controlador;
    }

 
    // Gestiona la colisiones que puede tener el portal tiempo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision " + collision.gameObject.name);

        // Si colisiona con el AreaJuego => se destruye
        if (collision.gameObject.CompareTag("AreaJuego"))
        {
            
            controlador.DestruirPortaTiempo(this.gameObject);
        }

        // Si colisona con la Nave => incrementa el tiempo de juego en el GameManager
        if (collision.gameObject.CompareTag("Nave"))
        {
            
            controlador.DestruirPortaTiempo(this.gameObject);
        }

    }

    
}
