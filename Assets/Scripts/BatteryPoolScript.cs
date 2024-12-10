using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Pool;

public class BatteryPoolScript : MonoBehaviour
{

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public ObjectPool<BatteryScript> BatteriesPool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BatteriesPool = new ObjectPool<BatteryScript>(CreatedBattery, OnRetrieve, OnReturn, OnAcquireBattery, true, 8, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private BatteryScript CreatedBattery()
    {
        BatteryScript batteryScript = Instantiate(GM_Final.Instance.BatteryPrefab, gameObject.transform.position, Quaternion.identity);
        Debug.Log("Created new one");
        batteryScript.SetPool(BatteriesPool);

        return batteryScript;
    }

    private void OnRetrieve(BatteryScript CreatedBattery)
    {
        Debug.Log("RetrievedFromPool");
        
        CreatedBattery.gameObject.SetActive(true);

    }

    private void OnReturn(BatteryScript CreatedBattery)
    {
        Debug.Log("ReturnedToPool");
        CreatedBattery.gameObject.SetActive(false);
    }

    private void OnAcquireBattery(BatteryScript battery)
    {
        Destroy(battery.gameObject);
    }
}
