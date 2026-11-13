using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference fire;
    [SerializeField] private InputActionReference acelerate;
    [SerializeField] private InputActionReference superAttack;
    [SerializeField] private GameControllerInput gameManager;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        movement.action.Enable();
        jump.action.Enable();
        fire.action.Enable();
        acelerate.action.Enable();
        superAttack.action.Enable();

        movement.action.performed += Move;
        jump.action.performed += Jump;
        fire.action.performed += Fire;

        superAttack.action.started += Started;
        superAttack.action.performed += Performed;
    }

    private void OnDisable()
    {
        movement.action.performed -= Move;
        jump.action.performed -= Jump;
        fire.action.performed -= Fire;

        superAttack.action.started -= Started;
        superAttack.action.performed -= Performed;

        movement.action.Disable();
        jump.action.Disable();
        fire.action.Disable();
        acelerate.action.Disable();
        superAttack.action.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float input = acelerate.action.ReadValue<float>();
        print(input);
    }

    private void Move(InputAction.CallbackContext context)
    {
        if (context.control.device is Gamepad)
        {
            gameManager.ControlDetectado("Mando");
        }
        else
        {
            gameManager.ControlDetectado("Teclado");
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (context.control.device is Gamepad)
        {
            gameManager.ControlDetectado("Mando");
        }
        else
        {
            gameManager.ControlDetectado("Teclado");
        }
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (context.control.device is Gamepad)
        {
            gameManager.ControlDetectado("Mando");
        }
        else
        {
            gameManager.ControlDetectado("Teclado");
        }
    }

    private void Started(InputAction.CallbackContext context)
    {
        print("SUPERMEGAATAQUE DETECTADO");
    }

    private void Performed(InputAction.CallbackContext context)
    {
        print("SUPERMEGAATAQUE DETECTADO");
    }
}