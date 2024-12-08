using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyScript : MonoBehaviour
{
    public Transform Target;
    public NavMeshAgent Agent;

    public float DetectionFieldRadius;

    public string PlayerTag;

    //how many seconds in Idle state before moving to a new location
    public float idleWaitTime;

    //keeps track of the time since the last patrol
    private float idleWaitTimeCounter = 0;

    //how long between each attack
    public float combatCooldown;
    //the counter that keeps track since the last attack
    private float combatCooldownCounter = 0;

    public float visionRange;

    public LayerMask Obscuring;

    public enum EnemyStates
    {
        Idle,
        Spotted,
        ChasingPlayer,
        AttackPlayer,
        Dead

    }

    private EnemyStates currState;

    public EnemyStates CurrState
    {
           get { return currState; }
           set { 
            if(value == currState) return;

            switch (value)
            {
                case EnemyStates.Idle:
                    {
                        Debug.Log("Enemy is Idle");
                        break;
                    }
                case EnemyStates.Spotted: {
                        Debug.Log("Player Spotted");
                        break;
                    }
                case EnemyStates.ChasingPlayer:
                    {
                        Debug.Log("Chasing Player");
                        break;
                    }
                case EnemyStates.AttackPlayer:
                    {
                        Debug.Log("Attacking Player");
                        break;
                    }
            }

            
            currState = value;
        
            }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    //bool isTargetDetected()
    //{
        
    //    if(Physics.SphereCast(this.transform.position, DetectionFieldRadius, out RaycastHit hitInfo)
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
