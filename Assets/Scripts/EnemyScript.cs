using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyScript : MonoBehaviour
{

    public GameObject IdleFace;

    public GameObject ChasingFace;

    private ObjectPool<EnemyScript> Pool;

    public Transform Target;

    public NavMeshAgent Agent;

    public float DetectionFieldRadius;

    public string PlayerTag;

    //how many seconds in Idle state before moving to a new location
    public float idleWaitTime;

    //keeps track of the time since the last patrol
    private float idleWaitTimeCounter = 0;

    //how long between each attack
    public float combatCooldown = 2f;
    //the counter that keeps track since the last attack
    private float combatCooldownCounter = 0;

    private bool coolingDown = false;

    public bool enemySpotted;

    public bool AttackingEnemy;



    public bool WasHit;
    

    public float speed;

    public ColliderComponent DetectionField;

    public ColliderComponent AttackField;

    public Vector3 PlayerPos;

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

            switch (currState)
            {
                case EnemyStates.Idle:
                    {
                        //Debug.Log("Enemy is Idle");
                        
                        break;
                    }
                case EnemyStates.Spotted: {
                        //Debug.Log("Player Spotted");
                        
                        break;
                    }
                case EnemyStates.ChasingPlayer:
                    {
                        //Debug.Log("Chasing Player");
                        
                        break;
                    }
                case EnemyStates.AttackPlayer:
                    {
                        //Debug.Log("Attacking Player");
                        
                        break;
                    }
                case EnemyStates.Dead:
                    {
                        Debug.Log("Enemy Dead");
                        
                        break;
                    }
            }

            
            currState = value;
        
            }
    }

    void Awake()
    {
        Target = GameObject.Find("Player").transform;

        Agent = GetComponent<NavMeshAgent>();   



    }

    private void OnEnable()
    {
        DetectionField.TriggerEnter += OnDetectionFieldEnter;
        DetectionField.TriggerExit += OnDetectionFieldExit;

        AttackField.TriggerEnter += OnAttackFieldEnter;
        AttackField.TriggerExit += OnAttackFieldExit;

        CurrState = EnemyStates.Idle;

        WasHit = false;

        coolingDown = false;

        GM_Final.Instance.EnemyCount += 1;

        Debug.Log(Agent.enabled == false ? "disabled" : "enabled");
    }

    private void OnDisable()
    {
        DetectionField.TriggerEnter -= OnDetectionFieldEnter;
        DetectionField.TriggerExit -= OnDetectionFieldExit;

        AttackField.TriggerEnter -= OnAttackFieldEnter;
        AttackField.TriggerExit -= OnAttackFieldExit;
       // Agent.enabled = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();

        
    }

    private void Idle()
    {
        Debug.Log("Idle State");

        IdleFace.SetActive(true);

        if (enemySpotted)
        {
            IdleFace.SetActive(false);
            CurrState = EnemyStates.Spotted;
        }
    }

    private void Spotted()
    {
        Debug.Log("Player Spotted");

        ChasingFace.SetActive(true);

        if (enemySpotted)
        {
            CurrState = EnemyStates.ChasingPlayer;
        }
       
    }

    private void ChasingPlayer()
    {
        Debug.Log("Chasing Player");
        Agent.destination = Target.position;

        if (AttackingEnemy)
        {

            CurrState = EnemyStates.AttackPlayer;
        }

        if (!enemySpotted)
        {
            CurrState = EnemyStates.Idle;
        }
    }

    private void AttackPlayer()
    {
        Agent.isStopped = true;

        if (!AttackingEnemy && enemySpotted)
        {
            CurrState = EnemyStates.ChasingPlayer;
        }

        //combatCooldownCounter += Time.deltaTime

        //Cool

        
    }

    private void Dead()
    {
        Debug.Log("Ghost is dead");
        Agent.isStopped = true;

        GM_Final.Instance.EnemyCount -= 1;
        ReturnEnemy();
        
    }

    void OnDetectionFieldEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerScript>(out PlayerScript player))
        {
            enemySpotted = true;
            //Debug.Log("Player is sensed");
        }
       
    }

    void OnDetectionFieldExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerScript>(out PlayerScript player))
        {
            enemySpotted = false;
            //Debug.Log("Lost Player");
        }
            
    }

    void OnAttackFieldEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerScript>(out PlayerScript player))
        {
            

            AttackingEnemy = true;

            //GM_Final.Instance.

            StartCoroutine(AttackCooldown());

            if (!AttackingEnemy)
            {
                StopCoroutine(AttackCooldown());
                
            }
            
        }
            
    }

    IEnumerator AttackCooldown()
    {
        

        while (AttackingEnemy)
        {
            if (!coolingDown)
            {
                Debug.Log("Should Attack Once");
                GM_Final.Instance.PlayerLives -= 1;
                coolingDown = true;
                
               
            }

            combatCooldownCounter += Time.deltaTime;

            if (combatCooldownCounter >= combatCooldown)
            {
                Debug.Log("Cooling down");
                coolingDown = false;
                combatCooldownCounter = 0f;
            }
             yield return null;
        }

        yield return 0;
    }

    void OnAttackFieldExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerScript>(out PlayerScript player))
        {
            AttackingEnemy = false;
            
            
            Debug.Log("No Longer attacking players");
        }
            
    }

    

    //bool isTargetDetected()
    //{
        
    //    if(Physics.SphereCast(this.transform.position, DetectionFieldRadius, out RaycastHit hitInfo)
    //}

    // Update is called once per frame
    void Update()
    {
        switch (CurrState)
        {
            case EnemyStates.Idle:
                {
                    //Debug.Log("Enemy is Idle");
                    Idle();
                    break;
                }
            case EnemyStates.Spotted:
                {
                    //Debug.Log("Player Spotted");
                    Spotted();
                    break;
                }
            case EnemyStates.ChasingPlayer:
                {
                    //Debug.Log("Chasing Player");
                    ChasingPlayer();
                    break;
                }
            case EnemyStates.AttackPlayer:
                {
                    //Debug.Log("Attacking Player");
                    AttackPlayer();
                    break;
                }
            case EnemyStates.Dead:
                {
                    Debug.Log("Enemy Dead");
                    Dead();
                    break;
                }
        }

        if(WasHit == true)
        {
            CurrState = EnemyStates.Dead;
        }

    }


    public void ReturnEnemy()
    {
        Debug.Log("Enemy Returned");
        Pool.Release(this);
    }

    public void SetPool(ObjectPool<EnemyScript> pool)
    {
        Pool = pool;
    }


}

