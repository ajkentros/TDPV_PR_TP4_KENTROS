using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalVida : MonoBehaviour
{
    private ControlPortalVida controlador;         // Referencia variable del tipo ControlPortalVida

    // Gestiona la instancia del game object del tipo ControlPortalVida que recibe
    public void SetControlador(ControlPortalVida _controlador)
    {
        controlador = _controlador;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision " + collision.gameObject.name);

        // Si el meteorito colisiona con el AreaJuego o con la nave se destruye
        if (collision.gameObject.CompareTag("AreaJuego"))
        {
            
            controlador.DestruirPortalVida(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Nave"))
        {
            
            controlador.DestruirPortalVida(this.gameObject);
        }

    }

    
}
