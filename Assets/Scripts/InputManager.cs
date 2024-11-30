using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    
    private PlayerControls playerCtrls;
 

    private void Awake()
    {
        playerCtrls = new PlayerControls();

       
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
        return playerCtrls.Player.ShineOn.ReadValue<bool>();
    }
    // Update is called once per frame
  
}
