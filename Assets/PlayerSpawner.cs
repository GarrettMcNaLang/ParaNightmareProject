using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerObj;
    public GameObject Camera;

    private void OnEnable()
    {
        GM_Final.Instance.spawnPlayerEvent += SetUpPlayer;
    }

    private void OnDisable()
    {
        GM_Final.Instance.spawnPlayerEvent -= SetUpPlayer;
    }

    private void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpPlayer()
    {

        Debug.Log("Should have moved player");
        PlayerObj.transform.position = this.transform.position;
    }
}
