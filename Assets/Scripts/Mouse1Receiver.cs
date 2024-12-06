using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse1Receiver : MonoBehaviour
{

    public Light AttackLight;

    #region variables
    public float lightDuration;

    public float coneRadius;
    public float coneDepth;
    public float coneAngle;

    #endregion

    private Physics physics;

    #region CombatInfo
    public string EnemyTag;
    bool isShining;

    #endregion

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        GM_Final.Instance.shootingEvent += ShiningLight;

        AttackLight.intensity = 0f;
    }

    private void OnDisable()
    {
        GM_Final.Instance.shootingEvent -= ShiningLight;
    }

    public void ShiningLight(bool IsClicked)
    {
        isShining = IsClicked;
        Debug.Log("Light GameObject Activated");
        StartCoroutine(LightFlashEffect());
        GM_Final.Instance.CurrBatteries -= 1;
        
    }
    IEnumerator LightFlashEffect()
    {
        AttackLight.intensity = 5f;

        while (AttackLight.intensity > 0f) {
            AttackLight.intensity -= Time.deltaTime * lightDuration;

            yield return null;
        }

        //for(int i = 0; i < lightDuration; i++)
        //{
        //    AttackLight.intensity -= 1f;
        //    yield return new WaitForSeconds(1);
        //}
        isShining = false;
        yield return 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        RaycastHit[] coneHits = physics.ConeCastAll(transform.position, transform.forward, coneRadius, coneDepth, coneAngle);

        if(coneHits.Length > 0)
        {
            for(int i = 0; i < coneHits.Length; i++)
            {
                coneHits[i].collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                if(isShining && coneHits[i].collider.gameObject.CompareTag(EnemyTag))
                {
                    Debug.Log("Enemy Spotted");
                }
            }
        }
    }

    
}
