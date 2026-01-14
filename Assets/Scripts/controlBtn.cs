using UnityEngine;
using UnityEngine.SceneManagement;

public class controlBtn : MonoBehaviour
{

    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject nivelesMenu;
    public GameObject endMenu;

    public GameObject btnNivelFacil;
    public GameObject btnNivelMedio;
    public GameObject btnNivelDificil;
    public GameObject victoria;
    public GameObject derrota;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startMenu.SetActive(true);
        nivelesMenu.SetActive(false);
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
            GameController.instance.endgame = false;
        }
        if(GameController.instance.nivelMedio)
        {
            mostrarNivelesMenu();
            activarBotoneDificultadMedia();
            GameController.instance.nivelMedio = false;
        }
        if(GameController.instance.nivelDificil)
        {
            mostrarNivelesMenu();
            activarBotoneDificultadDificil();
            GameController.instance.nivelDificil = false;
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
            
    }
    public void ocultarEndMenu()
    {
        endMenu.SetActive(false);
        startMenu.SetActive(true);
    }
    public void pausarJuego()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void reanudarJuego()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void salirJuego()
    {
        Application.Quit();
    }
    public void reiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void cargarNivel(int nivel)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nivel);
    }
    public void cargarNivelFacil()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
        GameController.instance.escena = 1;
        Debug.Log("Cargando nivel facil, escena:" + GameController.instance.escena);
    }
    public void cargarNivelMedio()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
        GameController.instance.escena = 2;
    }
    public void cargarNivelDificil()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
        GameController.instance.escena = 3;
    }
    public void volverAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void activarBotoneDificultadMedia()
    {
        btnNivelMedio.SetActive(true);
    }
    public void activarBotoneDificultadDificil()
    {
        btnNivelDificil.SetActive(true);
    }
    public void desactivarBotonesDificultad()
    {
        btnNivelMedio.SetActive(false);
        btnNivelDificil.SetActive(false);
    }
}
