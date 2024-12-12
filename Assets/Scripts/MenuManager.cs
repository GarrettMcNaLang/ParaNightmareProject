using JetBrains.Annotations;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject VictoryScreen;

    public GameObject GameOverScreen;

    //public GameObject 

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


    public void ExitGame()
    {
        Application.Quit();
    }

    public void ActivatePanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void DeactivatePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void StartGame()
    {
        GM_Final.Instance.GameStarted = true;
    }

    public void StopGame()
    {
        GM_Final.Instance.GameStarted = false;
    }

    public void GameOver()
    {
        StopGame();
    }

    public void Victory()
    {
        StopGame();
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
