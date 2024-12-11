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

    public void SpawnEnemy()
    {
        Debug.Log("Spawning Obj");
        var obj = GM_Final.Instance.EnemyPoolRef.EnemyPoolObj.Get();

        obj.transform.position = Vector3.zero + this.transform.position;

        NavMeshTriangulation Triangulation = NavMesh.CalculateTriangulation();

        NavMeshHit Hit;

        if (NavMesh.SamplePosition(this.transform.position, out Hit, 2f, 0))
        {
            obj.Agent.Warp(this.transform.position);

            obj.Agent.enabled = true;

            //Debug.Log(obj.Agent.enabled == false ? "disabled" :  "enabled");
        }
    }
}
