using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.iOS;
using UnityEngine.InputSystem.LowLevel;

public class NPC_Behaviour : MonoBehaviour
{

    [SerializeField] private Vector3 destination;
    [Tooltip("Si no se le asigna nada, el movimiento será independiente")]
    [SerializeField] private GameObject player;
    [SerializeField] private Animator npc;
    [SerializeField] private int childrenIndex;
    [SerializeField] private Transform path;
    [SerializeField] private bool isNPC;
    [SerializeField] private bool playerDetected;

    private Coroutine runningFollow;
    private Coroutine runningPatroll;
    private Coroutine losePlayerCoroutine;

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
        setAceleracionPlayer(3);
        setVelocidadPlayer(4);
        setAceleracion(8);
        setVelocidad(5);
        agent.isStopped = true;
        yield return new WaitForSeconds(1);
        agent.isStopped = false;
        while (true)
        {
            destination = player.transform.position;
            agent.SetDestination(destination);
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(1);
        }
        
    }

    #endregion

    #region Patroll Movement

    IEnumerator Patroll()
    {
        destination = path.GetChild(childrenIndex).position;
        agent.SetDestination(destination);

        setAceleracionPlayer(8);
        setVelocidadPlayer(5);
        setAceleracion(6);
        setVelocidad(3);

        while(true)
        {
            if (Vector3.Distance(transform.position, destination) < 2.5f)
            {
                agent.isStopped = true;
                npc.SetBool("pausa", true);
                yield return new WaitForSeconds(1f);
                npc.SetBool("pausa", false);
                
                childrenIndex++;
                childrenIndex = childrenIndex % path.childCount;

                destination = path.GetChild(childrenIndex).position;
                agent.isStopped = false;
                agent.SetDestination(destination);

                yield return new WaitForEndOfFrame();
                
            }
            yield return new WaitForSeconds(2f);
        }
    }
    #endregion

    #region Collider Detection

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && !other.GetComponent<PlayerController>().isCrouch)
        {
            if (runningPatroll != null)
            {
                StopCoroutine(runningPatroll);
                runningPatroll = null;

            }
            if (losePlayerCoroutine != null)
            {
                StopCoroutine(losePlayerCoroutine);
                losePlayerCoroutine = null;
            }
            npc.SetTrigger("Run");
            playerDetected = true;
            if (runningFollow == null)
                runningFollow = StartCoroutine(Follow());
        }
    
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            losePlayerCoroutine = StartCoroutine(LosePlayer());
        }        
    }
    IEnumerator LosePlayer()
    {
        // Sigue persiguiendo 3 segundos más
        yield return new WaitForSeconds(3f);

        if (runningFollow != null)
        {
            StopCoroutine(runningFollow);
            runningFollow = null;
            playerDetected = false;
        }

        npc.SetTrigger("Walk");
        
        if (runningPatroll == null)
        {
            runningPatroll = StartCoroutine(Patroll());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(GameController.instance);
            GameController.instance.recivirDaño();
            
        }
    }

    #endregion

    public void setVelocidad(int v)
    {
        agent.speed = v;
    }
    public void setAceleracion(int a)
    {
        agent.acceleration = a;
    }
    public void setVelocidadPlayer(int v)
    {
        agentPlayer.speed = v;
    }
    public void setAceleracionPlayer(int a)
    {
        agentPlayer.acceleration = a;
    }
}


