using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;
    public GameObject timerMenu;
    private float timer =30f ;
    private bool time = false;
    private bool derrota = false;
    public TMPro.TextMeshProUGUI timeText;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(timerMenu);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time)
        {     
            timerMenu.SetActive(true);
            timer -= Time.deltaTime;
            timeText.text = timer.ToString("F1");
        }
        
        if (timer < 0 && !derrota)
        {
            derrota = true;
            GameController.instance.lose();
        }
    }
    public void resetTimer(float newTime)
    {
        timer = newTime;
    }
    public void TimerStart(bool start)
    {
        if (start)
        {
            time = true;
        }
        else
        {
            time = false;
        }
    }
}
