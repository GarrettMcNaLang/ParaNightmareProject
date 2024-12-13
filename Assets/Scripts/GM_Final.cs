using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GM_Final : MonoBehaviour
{

    public bool GameStarted;


    public bool playerCanShoot;

    private float currBatteries;


    public float EnvironLightIntensity;
  

    public float CurrBatteries { 

        get { return currBatteries;} 

        set { currBatteries = Mathf.Clamp(value, 0, 3);

            if (currBatteries > 0)
            {
                playerCanShoot = true;
            }

            switch (currBatteries)
            {
                case 3:
                    {
                        MenuManager.Instance.BatState = MenuManager.BatteryState.Green; break;
                    }
                case 2:
                    {
                        MenuManager.Instance.BatState = MenuManager.BatteryState.Yellow; break;
                    }
                case 1:
                    {
                        MenuManager.Instance.BatState = MenuManager.BatteryState.Red; break;
                    }
                case 0:
                    {
                        MenuManager.Instance.BatState = MenuManager.BatteryState.Empty;
                        playerCanShoot = false;
                        break;
                    }
            }
              
        }
    }

    private float enemyCount;

    public float EnemyCount
    {
        get { return enemyCount; }

        set { enemyCount = value;

            if (enemyCount <= 0)
            {
                LevelEndedFunction();
            }
        }
    }


    private float playerLives;

    public float PlayerLives
    {
        get { return playerLives; }

        set { playerLives = value;

            if (playerLives == 3)
                MenuManager.Instance.CurrLivesState = MenuManager.LivesState.Three;
            else if (playerLives == 2)
                MenuManager.Instance.CurrLivesState = MenuManager.LivesState.Two;
            else if (playerLives == 1)
                MenuManager.Instance.CurrLivesState = MenuManager.LivesState.One;

            if (playerLives <= 0)
            {
                MenuManager.Instance.CurrLivesState = MenuManager.LivesState.Zero;
                MenuManager.Instance.GameOver();
            }
                
        }
    }

    public enum Levels
    {
        One,
        Two
    }
    private Levels currLevel;

    public Levels CurrLevel
    {
        get { return currLevel; }

        set { currLevel = value;

            Debug.Log(currLevel.ToString());
            
            
            
        }
    }

    #region Prefabs Reference
    public BatteryScript BatteryPrefab;
    public BatteryPoolScript BatteryPoolRef;

    public EnemyScript EnemyPrefab;
    public EnemyPool EnemyPoolRef;

    #endregion

    public static GM_Final Instance;

    private BatterySpawnerScript[] batterySpawners;

    private List<BatterySpawnerScript> LvlOneBatteries = new List<BatterySpawnerScript>();

    private List<BatterySpawnerScript> LvlTwoBatteries = new List<BatterySpawnerScript>();

    private EnemySpawner[] EnemySpawners;

    private List<EnemySpawner> LvlOneEnemies = new List<EnemySpawner>();

    private List<EnemySpawner> LvlTwoEnemies = new List<EnemySpawner>();

    private GameObject[] LightsObjs;

    private List<Light> Lights = new List<Light>();

    /// <summary>
    /// Shooting Function
    /// </summary>

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
           
        }
        else
        {
             Instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        batterySpawners = GameObject.FindObjectsByType<BatterySpawnerScript>(FindObjectsSortMode.None);

        EnemySpawners = GameObject.FindObjectsByType<EnemySpawner>(FindObjectsSortMode.None);

        LightsObjs = GameObject.FindGameObjectsWithTag("LightPrefab");

        

        CurrBatteries += 3;

        PlayerLives += 3;

       
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       for(int i = 0; i < batterySpawners.Length; i++)
        {
            if (batterySpawners[i].gameObject.CompareTag("Level1Objs"))
            {
                LvlOneBatteries.Add(batterySpawners[i]);


            }
            else if (batterySpawners[i].gameObject.CompareTag("Level2Objs"))
            {
                LvlTwoBatteries.Add(batterySpawners[i]);
            }
        }

       for(int i = 0; i< EnemySpawners.Length; i++)
        {
            if (EnemySpawners[i].gameObject.CompareTag("Level1Objs"))
            {
                LvlOneEnemies.Add(EnemySpawners[i]);
            }
            else if (EnemySpawners[i].gameObject.CompareTag("Level2Objs"))
            {
                LvlTwoEnemies.Add(EnemySpawners[i]);
            }
        }

       for(int i = 0; i < LightsObjs.Length; i++)
        {
            var light = LightsObjs[i].GetComponent<Light>();

            Lights.Add(light);
        }

        for (int i = 0; i < Lights.Count; i++)
        {
            Lights[i].intensity = 0f;
        }


        Debug.Log("Level One Batteries: " + LvlOneBatteries.Count);
        Debug.Log("Level Two Batteries: " + LvlTwoBatteries.Count);

        Debug.Log("Level One Enemies: " + LvlOneEnemies.Count);
        Debug.Log("Level Two Enemies: " + LvlTwoEnemies.Count);

        CurrLevel = Levels.One;

        Cursor.lockState = CursorLockMode.Confined;
    }

    public void LevelDecider(string level)
    {
       


        if(level == Levels.One.ToString())
        {
            Debug.Log("Loading Scene 1");
            LevelFunction(Levels.One);
        }
        else if(level == Levels.Two.ToString())
        {
            Debug.Log("Loading Scene 2");
            LevelFunction(Levels.Two);
        }

       
    }

    public void LevelFunction(Levels level)
    {
        Debug.Log("In Level Function");
        switch(level) {
            case Levels.One:
                {
                    for (int i = 0; i < LvlOneBatteries.Count; i++)
                    {
                        Debug.Log("Level 1 Batteries Spawning");
                        LvlOneBatteries[i].SpawnBattery();
                        
                    }

                    for(int i = 0; i < LvlOneEnemies.Count; i++)
                    {
                        Debug.Log("Level 1 Enemeis Spawning");
                        LvlOneEnemies[i].SpawnEnemy();
                    }

                   
                    

                    SpawnPlayerFunction();
                    break;
                }
            case Levels.Two:
                {
                    for (int i = 0; i < LvlTwoBatteries.Count; i++)
                    {
                        Debug.Log("Level 2 Batteries Spawning");
                        LvlTwoBatteries[i].SpawnBattery();
                        
                    }

                    for (int i = 0; i < LvlTwoEnemies.Count; i++)
                    {
                        Debug.Log("Level 2 Enemies Spawning");
                        LvlTwoEnemies[i].SpawnEnemy();
                    }
                    break;
                }

        }
    }
    public delegate void ShootingEvent(bool buttonAction);

    public event ShootingEvent shootingEvent;

    public void LevelEndedFunction()
    {
        foreach (Light obj in Lights)
        {
            obj.intensity = EnvironLightIntensity;
        }
    }
    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {
        
    }

    public void Mouse1Event(bool IsClicked)
    {
        Debug.Log("Shooitng in GM");
        shootingEvent(IsClicked);
        
    }

    public delegate void SpawnPlayer();

    public event SpawnPlayer spawnPlayerEvent;

    public void SpawnPlayerFunction()
    {
        Debug.Log("InSpawnPlayerFunction");
        //spawnPlayerEvent();
    }
   
    

    // Update is called once per frame
    void Update()
    {
        if(GameStarted == false)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }



    }
}
