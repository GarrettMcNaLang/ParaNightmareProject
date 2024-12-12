using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GM_Final : MonoBehaviour
{

    public bool GameStarted;


    public bool playerCanShoot;

    private float currBatteries;

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
                MenuManager.Instance.GameOver();
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
            LevelFunction(currLevel);
            
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

    
    /// <summary>
    /// Shooting Function
    /// </summary>

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

        batterySpawners = GameObject.FindObjectsByType<BatterySpawnerScript>(FindObjectsSortMode.None);

        EnemySpawners = GameObject.FindObjectsByType<EnemySpawner>(FindObjectsSortMode.None);

        CurrBatteries += 3;

        GameStarted = false;

       
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

        
        Debug.Log("Level One Batteries: " + LvlOneBatteries.Count);
        Debug.Log("Level Two Batteries: " + LvlTwoBatteries.Count);

        Debug.Log("Level One Enemies: " + LvlOneEnemies.Count);
        Debug.Log("Level Two Enemies: " + LvlTwoEnemies.Count);

        CurrLevel = Levels.One;
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
   

    // Update is called once per frame
    void Update()
    {
        if(GameStarted == false)
        {
            Time.timeScale = 0.0f;
        }
    }
}
