using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class GameController : MonoBehaviour
{

    public bool endgame;
    public bool win = false;

    public static GameController instance;
    public int escena;
    public bool nivelMedio = false;
    public bool nivelDificil = false;
    private int score;
    public bool menuPausa = false;
    private LoadSceneMode mode;
    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(pauseMenu);
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuPausa)
            {
                reanudarJuego();
            }
            else
            {
                pausarJuego();
            }
        }
        //Debug.Log("Escena update:" + escena);
    }

    public void ganarJuego()
    {
        Debug.Log("Ganar Juego");
        Debug.Log("Escena actual-1:" + escena);
        TimerManager.Instance.TimerStart(false);
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

    public void pausarJuego()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        menuPausa = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void reanudarJuego()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        menuPausa = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void resetToMenu()
    {
        reiniciarScore();
        escena = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
    public void lose()
    {
        TimerManager.Instance.TimerStart(false);
        Debug.Log("Has perdido");
        win = false;
        escena = 0;
        EndGame();
        Debug.Log("Score actual:" + score);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void salir() { 
        Application.Quit();
    }
    public void backToNivel() {
        escena = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(escena);
    }
}
