using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{

    public static PlayerScript instance;
    #region delegates

   
    #endregion

    #region Components
    private CharacterController controller;
    private InputManager inputManager;
    private Transform CameraHead;
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

    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        CameraHead = Camera.main.transform;

        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(this.gameObject);


        //DontDestroyOnLoad(instance);

        
    }
    private void Start()
    {
        
    }

    private void OnEnable()
    {

        
    }

    void OnDisable()
    {
       
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
        move.y = 0f;
        move = CameraHead.transform.forward * move.z + CameraHead.transform.right * move.x;

        // scale by speed
        move *= playerSpeed;

        //Vector3 Movement = new Vector3(move.x, 0f, move.z);
       // controller.Move(move * Time.deltaTime * playerSpeed);

        //if (move.magnitude > 0.05f)
        //{
        //    gameObject.transform.forward = move;
        //}

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


        if (inputManager.ShootingFunction())
        {
            GM_Final.Instance.Mouse1Event(true);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.TryGetComponent<BatteryScript>(out BatteryScript battery)){
            GM_Final.Instance.CurrBatteries += 1;
            battery.ReturnBattery();
        }



    }

}
