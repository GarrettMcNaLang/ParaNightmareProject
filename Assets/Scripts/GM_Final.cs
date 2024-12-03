using UnityEngine;

public class GM_Final : MonoBehaviour
{
    private static GM_Final _instance;

    public static GM_Final instance
    {
        get { return _instance; }
    }
    /// <summary>
    /// Shooting Function
    /// </summary>

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }


        
    }
    public delegate void ShootingEvent();

    public event ShootingEvent shootingEvent;

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {
        
    }

    public void Mouse1Event()
    {
        Debug.Log("Shooitng in GM");
        //shootingEvent();
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
