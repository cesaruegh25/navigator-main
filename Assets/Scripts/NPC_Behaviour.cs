using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.iOS;

public class NPC_Behaviour : MonoBehaviour
{

    [SerializeField] private Vector3 destination;
    [Tooltip("Si no se le asigna nada, el movimiento será independiente")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameController game;

    [SerializeField] private int childrenIndex;
    [SerializeField] private Transform path;
    [SerializeField] private bool isNPC;
    [SerializeField] private bool playerDetected;
    private Coroutine runningPatroll;
    NavMeshAgent agent;
    NavMeshAgent agentPlayer;

    public void Start()
    {
        agentPlayer = player.GetComponent<NavMeshAgent>();
        agent = GetComponent<NavMeshAgent>();
        
        if (isNPC)
        {
            runningPatroll = StartCoroutine("Patroll");
            //StartCoroutine("DistanceDetection");
           
        }

    }

    void Update()
    {


    }

    // #region y #endregion te permite hacer codigo desplegable sin que sean funciones
    //el nombre de la region se pone despues de #region

    #region Always Detect

    IEnumerator Follow()
    {
        while (true)
        {
            agentPlayer.acceleration = 3;
            agentPlayer.speed = 4;
            agent.acceleration = 8;
            agent.speed = 5;
            
            destination = player.transform.position;
            GetComponent<NavMeshAgent>().SetDestination(destination);
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(1);
        }
        
    }

    #endregion

    #region Patroll Movement

    IEnumerator Patroll()
    {
        destination = path.GetChild(childrenIndex).position;
        GetComponent<NavMeshAgent>().SetDestination(destination);

        while(true)
        {
            agentPlayer.acceleration = 8;
            agentPlayer.speed = 5;
            agent.acceleration = 6;
            agent.speed = 3;
            //Debug.Log("while patroll");

            //Debug.Log("Posicion " + transform.position + "; Destino: " + destination);

            if (Vector3.Distance(transform.position, destination) < 0.5f)
            {

                //Debug.Log("if patroll");
                //Debug.Log(childrenIndex);

                childrenIndex++;
                childrenIndex = childrenIndex % path.childCount;

                destination = path.GetChild(childrenIndex).position;
                GetComponent<NavMeshAgent>().SetDestination(destination);

               yield return new WaitForEndOfFrame();
                
            }

            yield return new WaitForSeconds(1);

        }
    }
    #endregion

    #region Collider Detection

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (runningPatroll != null)
            {

                StopCoroutine("Patroll");
                runningPatroll = null;

            }

            playerDetected = true;
            StartCoroutine("Follow");

        }
    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine("Follow");
            playerDetected = false;
            
            if(runningPatroll == null)
            {
                runningPatroll = StartCoroutine("Patroll");
            }
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            game.recivirDaño();
            
        }
    }

    #endregion

}


