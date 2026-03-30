//using System.Threading.Tasks.Dataflow;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // movimiento
    private Vector2 lookInput;
    public float mouseSensivity = 0.15f;
    private Vector2 moveInput;
    private Rigidbody rb;
    // velocidad
    [SerializeField] private float speed = 5f;
    [SerializeField] private float crouchMultipler = 0.5f;
    private bool isCrouch = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //PlayerMove();
        ComprobarCtrl();
        float yaw = lookInput.x * mouseSensivity;
        transform.Rotate(0, yaw, 0, Space.Self);
    }
    private void FixedUpdate()
    {
        Vector3 direccion = transform.TransformDirection(
            new Vector3(moveInput.x, 0, moveInput.y));
        float currentSpeed = speed;
        if (isCrouch)
        {
            currentSpeed = speed * crouchMultipler;
        }

        Vector3 velocity = direccion * currentSpeed;
        Vector3 newVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        rb.linearVelocity = newVelocity;
    }
    public void OnMove(InputValue value )
    {
        moveInput = value.Get<Vector2>();
    }
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }
    // se ejecuta antes que el start se usa para centrar la camara
    private void OnEnable()
    {
        lookInput = Vector2.zero;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Finish"))
        {
            GameController.instance.ganarJuego();
        }

    }
    public void ComprobarCtrl()
    {
        bool ctrlPressed = Keyboard.current != null &&
            (Keyboard.current.rightCtrlKey.isPressed ||
            Keyboard.current.leftCtrlKey.isPressed);
        if (ctrlPressed)
        {
            isCrouch = true;
        }
        else
        {
            isCrouch = false;
        }
    }
}
