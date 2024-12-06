using UnityEngine;

public class BatterySpawnerScript : MonoBehaviour
{
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBattery()
    {
        Debug.Log("Spawning Battery");
        var obj = GM_Final.Instance.BatteryPoolRef.BatteriesPool.Get();

        obj.transform.position = this.transform.position + new Vector3(0, 0.514f, 0);
    }
}
