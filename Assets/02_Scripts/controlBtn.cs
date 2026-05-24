using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class controlBtn : MonoBehaviour
{

    public GameObject startMenu;
    public GameObject nivelesMenu;
    public GameObject endMenu;

    public GameObject btnNivelFacil;
    public GameObject btnNivelMedio;
    public GameObject btnNivelDificil;
    public GameObject victoria;
    public GameObject derrota;

    private Image imagenNiveles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startMenu.SetActive(true);
        nivelesMenu.SetActive(false);
        imagenNiveles = nivelesMenu.GetComponent<Image>();
        imagenNiveles.color = new Color32(255, 255, 255, 255);
        endMenu.SetActive(false);
        btnNivelMedio.SetActive(false);
        btnNivelDificil.SetActive(false);
        victoria.SetActive(false);
        derrota.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.instance.endgame)
        {
            mostrarEndMenu();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("Mostrando menu de fin de juego");
            desactivarBotonesDificultad();
        }
        if(GameController.instance.nivelMedio && !btnNivelMedio.activeSelf)
        {
            mostrarNivelesMenu();
            activarBotoneDificultadMedia();
        }
        if(GameController.instance.nivelDificil && !btnNivelDificil.activeSelf)
        {
            mostrarNivelesMenu();
            activarBotoneDificultadDificil();
        }
    }

    public void mostrarNivelesMenu()
    {
        startMenu.SetActive(false);
        nivelesMenu.SetActive(true);
    }

    public void ocultarNivelesMenu()
    {
        startMenu.SetActive(true);
        nivelesMenu.SetActive(false);
    }
    public void mostrarEndMenu()
    {
        //Debug.Log("Dentro de mostrar end menu");
        endMenu.SetActive(true);
        startMenu.SetActive(false);
        nivelesMenu.SetActive(false);
        if (GameController.instance.win)
        {
            victoria.SetActive(true);
        }

        else
        {
            derrota.SetActive(true);
        }
        GameController.instance.nivelMedio = false;
        GameController.instance.nivelDificil = false;
        GameController.instance.endgame = false;

    }
    public void ocultarEndMenu()
    {
        endMenu.SetActive(false);
        nivelesMenu.SetActive(false);
        startMenu.SetActive(true);
    }
    public void salirJuego()
    {
        Application.Quit();
    }
    public void cargarNivelFacil()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
        GameController.instance.escena = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Cargando nivel facil, escena:" + GameController.instance.escena);
    }
    public void cargarNivelMedio()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
        GameController.instance.escena = 2;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void cargarNivelDificil()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
        GameController.instance.escena = 3;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void activarBotoneDificultadMedia()
    {
        btnNivelMedio.SetActive(true);
        imagenNiveles.color = new Color32(255, 170, 170, 255);
    }
    public void activarBotoneDificultadDificil()
    {
        btnNivelDificil.SetActive(true);
        imagenNiveles.color = new Color32(255, 88, 88, 255);
    }
    public void desactivarBotonesDificultad()
    {
        btnNivelMedio.SetActive(false);
        btnNivelDificil.SetActive(false);
        imagenNiveles.color = new Color32(255, 255, 255, 255);
    }
    
}
