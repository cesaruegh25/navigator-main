using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public bool endgame;
    public bool win = false;

    public static GameController instance;
    public int escena;
    public bool nivelMedio = false;
    public bool nivelDificil = false;
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
        Debug.Log("Escena actual-0:" + escena);
        nivelMedio = false;
        nivelDificil = false;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Escena update:" + escena);
    }

    public void ganarJuego()
    {
        Debug.Log("Ganar Juego");
        Debug.Log("Escena actual-1:" + escena);
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
        Debug.Log("Reiniciando nivel:" + escena);
        SceneManager.LoadScene(escena);
    }
    public void nivel(int nivel)
    {
        Debug.Log("Cargando nivel:" + nivel);
        escena = nivel;
        Debug.Log("Nivel cargado:" + escena);
        SceneManager.LoadScene(escena);
    }
    
    public void EndGame()
    {
        Debug.Log("puntuación = " + score);
        SceneManager.LoadScene(escena);
        endgame = true;

    }
    private void reiniciarScore()
    {
        score = 0;
    }
    private void winGame()
    {
        //carga pantalla de win
        Debug.Log("Has ganado");
        Debug.Log("numero de escena-2:" + escena);
        aumentarScore(100);
        if (escena == 1)
        {
            Debug.Log("Cargando siguiente nivel:" + escena);
            escena = 0;
            nivelMedio = true;
            SceneManager.LoadScene(escena);
            Debug.Log("Nivel cargado:" + escena);
            Debug.Log("Score actual:" + score);
        }
        if (escena == 2)
        {
            Debug.Log("Cargando siguiente nivel:" + escena);
            escena = 0;
            nivelDificil = true;
            SceneManager.LoadScene(escena);
            Debug.Log("Nivel cargado:" + escena);
            Debug.Log("Score actual:" + score);
        }
        if (escena == 3)
        {
            Debug.Log("Juego terminado");
            escena = 0;
            win = true;
            EndGame();
        }
    }
    private void lose()
    {
        //carga pantalla de restart o menu
        Debug.Log("Has perdido");
        win = false;
        escena = 0;
        EndGame();
        Debug.Log("Score actual:" + score);
    }

}
