using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public ObjectPool<EnemyScript> EnemyPoolObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyPoolObj = new ObjectPool<EnemyScript>(CreateEnemy, OnRetrieve, OnReturn, OnGhostDefeated,true, 10, 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private EnemyScript CreateEnemy()
    {
        EnemyScript enemyScript = Instantiate(GM_Final.Instance.EnemyPrefab, gameObject.transform.position, Quaternion.identity);

        Debug.Log("EnemyPrefab Created");
        enemyScript.SetPool(EnemyPoolObj);

        return enemyScript;
    }

    private void OnRetrieve(EnemyScript Enemy)
    {
        Debug.Log("EnemyRetrieved");
        Enemy.gameObject.SetActive(true);
    }

    private void OnReturn(EnemyScript Enemy)
    {
        Debug.Log("EnemyReturned");
        Enemy.gameObject.SetActive(false);
    }

    private void OnGhostDefeated(EnemyScript Enemy)
    {
        Destroy(Enemy.gameObject);
    }
}
