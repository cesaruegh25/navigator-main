using UnityEngine;

public class btnControllerEscene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void renaudar() { 
        GameController.instance.reanudarJuego();
    }
    public void salirJuego() {
        GameController.instance.salir();
    }
    public void backToNivel() {
        GameController.instance.backToNivel();
    }
}
