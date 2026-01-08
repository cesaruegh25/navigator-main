using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        escena = SceneManager.GetActiveScene().buildIndex;
        reiniciarScore();

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ganarJuego()
    {
        win();
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
        reiniciarScore();
        SceneManager.LoadScene(escena);
    }
    public void resetToMenu()
    {
        reiniciarScore();
        escena = 0;
        SceneManager.LoadScene(escena);
    }
    private void reiniciarScore()
    {
        score = 0;
    }
    private void win()
    {
        //carga pantalla de win
        Debug.Log("Has ganado");
        aumentarScore(100);
        if (escena < 5)
        {
            SceneManager.LoadScene(escena++);
        }
    }
    private void lose()
    {
        //carga pantalla de restart o menu
        Debug.Log("Has perdido");
    }   
}
