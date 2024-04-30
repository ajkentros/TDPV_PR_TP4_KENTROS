using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelMenu;               // Referencia al panel menu
    [SerializeField] private GameObject panelInstrucciones;      // Referencia al panel instrucciones
    private void Start()
    {
        panelMenu.SetActive(true);
        panelInstrucciones.SetActive(false);
    }
    // Gestiona la selección y carga de la escena
    public void SiguienteEscena()
    {
        /*
         * Obtiene el índice de la escena actual
         * Calcula el índice de la siguiente escena en orden
         * Carga la siguiente escena en orden
         */

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        SceneManager.LoadScene(nextSceneIndex);
    }

    // Gestiona el cierre de la aplicación
    public void SaleJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void BotonInstrucciones()
    {
        panelMenu.SetActive(false);
        panelInstrucciones.SetActive(true);
    }
    public void BotonMenu()
    {
        panelMenu.SetActive(true);
        panelInstrucciones.SetActive(false);
    }
}
