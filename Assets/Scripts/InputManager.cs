using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    {
        get { return _instance; }

       
    }
    
    private PlayerControls playerCtrls;
 

    private void Awake()
    {
        playerCtrls = new PlayerControls();

       if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void OnEnable()
    {
       playerCtrls.Enable();
    }

    private void OnDisable()
    {
        playerCtrls.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    //Movement Function

    public Vector3 PlayerMovement()
    {
       return playerCtrls.Player.Movement.ReadValue<Vector3>();
    }

    //Mouse Movement
    public Vector2 MouseMovement()
    {
        return playerCtrls.Player.Looking.ReadValue<Vector2>();
    }

    //jumping button on this frame
    public bool JumpingFunction()
    {
        return playerCtrls.Player.Jump.triggered;
    }

    public bool ShootingFunction()
    {
        return playerCtrls.Player.ShineOn.triggered;
    }
    // Update is called once per frame
  
}
