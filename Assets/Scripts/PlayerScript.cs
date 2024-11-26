using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private InputSystem_Actions testingInputActions;

    private void Awake()
    {
        testingInputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        testingInputActions.Enable();
    }

    private void OnDisable()
    {
        testingInputActions.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Vector2 Move = testingInputActions.Player.Move.ReadValue<Vector2>();

        Debug.Log(Move);

        if(testingInputActions.Player.Jump.triggered)
        {
            Debug.Log("Player has jumped");
        }
    }
}
