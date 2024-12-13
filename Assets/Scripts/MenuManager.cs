using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject VictoryScreen;

    public GameObject GameOverScreen;

    public GameObject PauseScreen;

    public GameObject MainUI;

    

    //public GameObject 

    public static MenuManager Instance;

   

    public GameObject BatGreen, BatYellow, BatRed;

    public GameObject LifeThree, LifeTwo, LifeOne;

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

    public enum LivesState
    {
        Three,
        Two,
        One,
        Zero
    }

    private LivesState livesState;

    public LivesState CurrLivesState
    {
        get { return livesState; }

        set
        {
            livesState = value;

            

            if(livesState == LivesState.Three)
            {
                LifeThree.SetActive(true);
                LifeTwo.SetActive(true);
                LifeOne.SetActive(true);
            }
            else if(livesState == LivesState.Two) {

                LifeThree.SetActive(false);
                LifeTwo.SetActive(true);
                LifeOne.SetActive(true);

            }
            
            else if(livesState == LivesState.One)
            {
                LifeThree.SetActive(false);
                LifeTwo.SetActive(false);
                LifeOne.SetActive(true);
            }

            else if(livesState == LivesState.Zero)
            {
                LifeThree.SetActive(false);
                LifeTwo.SetActive(false);
                LifeOne.SetActive(false);
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
            //DontDestroyOnLoad(gameObject);
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

        ActivatePanel(GameOverScreen);
        DeactivatePanel(MainUI);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Victory()
    {
        StopGame();

        ActivatePanel(VictoryScreen);
        DeactivatePanel(MainUI);
    }

    public void Pause()
    {
        StopGame();

        ActivatePanel(PauseScreen);
        DeactivatePanel(MainUI);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GM_Final.Instance.GameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Pause();
        }
    }
}
