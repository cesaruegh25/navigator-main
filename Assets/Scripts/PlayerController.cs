//using System.Threading.Tasks.Dataflow;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Vector3 destination;
    //public bool modoRandom;
    public bool modoClick;
    [SerializeField] private Vector3 max, min;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    if (modoRandom)
    //    {
    //        destination = RandomDestination();
    //        GetComponent<NavMeshAgent>().SetDestination(destination);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        //if (modoClick)
            //{
                if (Input.GetButtonDown("Fire1"))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit = new RaycastHit();

                    if(Physics.Raycast(ray, out hit, 1000))
                    {
                        GetComponent<NavMeshAgent>().SetDestination(hit.point);
                    }
                }
            //movimiento con click izq (desde la pestaña game)
        
            //}
        

            //if (modoRandom)
            //{
            //    if (Vector3.Distance(transform.position, destination) < 0.8f)
            //    {
            //        destination = RandomDestination();
            //        GetComponent<NavMeshAgent>().SetDestination(destination);
            //    }
            //}
    }

    //private Vector3 RandomDestination()
    //{
    //    return new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));
    //}

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Finish"))
        {
            GameController.instance.ganarJuego();
        }

    }
}
