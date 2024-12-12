using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

   

    public GameObject BatGreen, BatYellow, BatRed;

    public enum BatteryState
    {
        Green,
        Yellow,
        Red,
        Empty
    }

    private BatteryState batState;

    public BatteryState BatState
    {
        get { return batState; }

        set { batState = value; 

            BatGreen.SetActive(false);
            BatYellow.SetActive(false);
            BatRed.SetActive(false);

            if(batState == BatteryState.Green)
            {
                BatGreen.SetActive(true);
            }
            else if(batState == BatteryState.Yellow)
            {
                BatYellow.SetActive(true);
            }
            else if (batState == BatteryState.Red)
            { BatRed.SetActive(true);}
            else if (batState == BatteryState.Empty)
            {
                BatGreen.SetActive(false);
                BatYellow.SetActive(false);
                BatRed.SetActive(false);
            }
        
        }
    }


    

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
