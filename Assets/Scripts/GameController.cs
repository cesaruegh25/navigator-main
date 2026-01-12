using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject nivelesMenu;
    public GameObject endMenu;

    public GameObject btnNivelFacil;
    public GameObject btnNivelMedio;
    public GameObject btnNivelDificil;

    public bool endgame;
    public bool win = true;

    public static GameController instance;
    public int escena;
    private int score;
    private LoadSceneMode mode;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        reiniciarScore();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        escena = SceneManager.GetActiveScene().buildIndex;
        startMenu.SetActive(true);
        nivelesMenu.SetActive(false);
        endMenu.SetActive(false);
        btnNivelMedio.SetActive(false);
        btnNivelDificil.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ganarJuego()
    {
        Debug.Log("Ganar Juego");
        nivelesMenu.SetActive(true);
        winGame();

    }

    public void recivirDaño()
    {
        lose();
    }

    public int getScore()
    {
        return score;
    }

    public void aumentarScore(int cantidad)
    {
        score += cantidad;
    }

    public void disminuirScore(int cantidad)
    {
        score -= cantidad;
    }
    
    public void resetGame()
    {
        disminuirScore(20);
        Debug.Log("Reiniciando nivel:" + escena);
        SceneManager.LoadScene(escena);
        Debug.Log("Nivel reiniciado:" + escena);
    }
    public void resetToMenu()
    {
        reiniciarScore();
        escena = 0;
        SceneManager.LoadScene(escena);
    }
    public void resetToNivel()
    {
        startMenu.SetActive(false);
        nivelesMenu.SetActive(true);
        escena = 0;
        SceneManager.LoadScene(escena);
    }
    public void nivel(int nivel)
    {
        Debug.Log("Cargando nivel:" + nivel);
        escena = nivel;
        SceneManager.LoadScene(escena);
    }
    
    public void EndGame()
    {
        escena = 0;
        SceneManager.LoadScene(escena);
        endgame = true;

        endMenu.SetActive(true);
        if (win)
            endMenu.transform.GetChild(0).gameObject.SetActive(true);
        else
            endMenu.transform.GetChild(1).gameObject.SetActive(true);
    }
    private void reiniciarScore()
    {
        score = 0;
    }
    private void winGame()
    {
        //carga pantalla de win
        Debug.Log("Has ganado");
        aumentarScore(100);
        if (escena == 1)
        {
            Debug.Log("Cargando siguiente nivel:" + escena);
            escena = 0;
            SceneManager.LoadScene(escena);
            btnNivelMedio.SetActive(true);
            Debug.Log("Nivel cargado:" + escena);
            Debug.Log("Score actual:" + score);
        }
        if (escena == 2)
        {
            Debug.Log("Cargando siguiente nivel:" + escena);
            escena = 0;
            SceneManager.LoadScene(escena);
            btnNivelDificil.SetActive(true);
            Debug.Log("Nivel cargado:" + escena);
            Debug.Log("Score actual:" + score);
        }
        if (escena == 3)
        {
            Debug.Log("Juego terminado");
            win = true;
            EndGame();
        }
    }
    private void lose()
    {
        //carga pantalla de restart o menu
        Debug.Log("Has perdido");
        resetToNivel();
        Debug.Log("Score actual:" + score);
    }

}
