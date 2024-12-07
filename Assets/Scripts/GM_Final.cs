using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GM_Final : MonoBehaviour
{
    public bool playerCanShoot;

    private float currBatteries;

    public float CurrBatteries { 

        get { return currBatteries;} 

        set { currBatteries = Mathf.Clamp(value, 0, 3);
            
                //Change graphic for batteries
             if(currBatteries == 0)
            {
                playerCanShoot = false;
            }
            else if(currBatteries > 0) {
                playerCanShoot = true;
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

            LevelFunction(currLevel);
            
        }
    }

    #region Prefabs Reference
    public BatteryScript BatteryPrefab;
    public BatteryPoolScript BatteryPoolRef;

    #endregion

    public static GM_Final Instance;

    private BatterySpawnerScript[] batterySpawners;

    private List<BatterySpawnerScript> LvlOneBatteries = new List<BatterySpawnerScript>();

    private List<BatterySpawnerScript> LvlTwoBatteries = new List<BatterySpawnerScript>();
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

        CurrBatteries += 3;
        
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

        CurrLevel = Levels.Two;
        Debug.Log("Level One Batteries: " + LvlOneBatteries.Count);
        Debug.Log("Level Two Batteries: " + LvlTwoBatteries.Count);
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
                    break;
                }
            case Levels.Two:
                {
                    for (int i = 0; i < LvlTwoBatteries.Count; i++)
                    {
                        Debug.Log("Level 2 Batteries Spawning");
                        LvlTwoBatteries[i].SpawnBattery();
                        
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
        
    }
}
