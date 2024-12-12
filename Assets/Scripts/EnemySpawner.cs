using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SpawnEnemy()
    {
        Debug.Log("Spawning Obj");
        var obj = GM_Final.Instance.EnemyPoolRef.EnemyPoolObj.Get();

        obj.transform.position = Vector3.zero + this.transform.position;

        obj.Agent.Warp(this.transform.position);

        obj.Agent.enabled = true;

        //NavMeshTriangulation Triangulation = NavMesh.CalculateTriangulation();

        //NavMeshHit Hit;

        //if (NavMesh.SamplePosition(this.transform.position, out Hit, 2f, 0))
        //{
            

            
        //}
    }
}
