using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{
    #region Components
    private CharacterController controller;
    private InputManager inputManager;
    #endregion

    #region Variables
    private float verticalVelocity;
    private float groundedTimer;
    public bool groundedPlayer;

    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float gravityValue;
    #endregion

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
    }

    void Update()
    {

        groundedPlayer = controller.isGrounded;


        if (groundedPlayer)
        {
            // cooldown interval to allow reliable jumping even whem coming down ramps
            groundedTimer = 0.2f;
        }
        if (groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }

        if (groundedPlayer && verticalVelocity < 0)
        {
            // hit ground
            verticalVelocity = 0f;
        }

        // apply gravity always, to let us track down ramps properly
        verticalVelocity -= gravityValue * Time.deltaTime;

        //basic movement
        Vector3 Movement = inputManager.PlayerMovement();
        Vector3 move = new Vector3(Movement.x, 0f , Movement.z);

        // scale by speed
        move *= playerSpeed;

        //Vector3 Movement = new Vector3(move.x, 0f, move.z);
       // controller.Move(move * Time.deltaTime * playerSpeed);

        if (move.magnitude > 0.05f)
        {
            gameObject.transform.forward = move;
        }

        //jumping
        // Makes the player jump
        if (inputManager.JumpingFunction())
        {
            // must have been grounded recently to allow jump
            if (groundedTimer > 0)
            {
                // no more until we recontact ground
                groundedTimer = 0;

                // Physics dynamics formula for calculating jump up velocity based on height and gravity
                verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);
            }
        }

        //affects velocity
        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }

   
}
