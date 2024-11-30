using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    #region Components
    private InputSystem_Actions testingInputActions;
    Rigidbody rb;
    #endregion

    #region variables

    public float movSpeed;

    float rotateSpeed;

    Vector2 Movement;
    #endregion



    private void Awake()
    {
        testingInputActions = new InputSystem_Actions();

        rb = GetComponent<Rigidbody>();
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
       Movement = testingInputActions.Player.Move.ReadValue<Vector2>();

       

        Debug.Log(Movement);

        if(testingInputActions.Player.Jump.triggered)
        {
            Debug.Log("Player has jumped");
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Movement * movSpeed;
    }
}
