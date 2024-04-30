using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorito : MonoBehaviour
{
    private ControlMeteoritos controlador;

    public void SetControlador(ControlMeteoritos _controlador)
    {
        controlador = _controlador;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision " + collision.gameObject.name);

        // Si el meteorito colisiona con el AreaJuego, Nave, POrtalTiempo o PortalVida => se destruye
        if (collision.gameObject.CompareTag("AreaJuego"))
        {
            
            controlador.DestruirMeteorito(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Nave"))
        {
            controlador.DestruirMeteorito(this.gameObject);
        }

        if (collision.gameObject.CompareTag("PortalVida"))
        {
            controlador.DestruirMeteorito(this.gameObject);
        }

        if (collision.gameObject.CompareTag("PortalTiempo"))
        {
            controlador.DestruirMeteorito(this.gameObject);
        }
    }

    
}
