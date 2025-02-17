using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    private Collider _damageCasterCollider;
    public string targetTag;
    private List<Collider> _damagedTargetList;

    [SerializeField] private DamageData damageData;
    [SerializeField] private GameObject impactVfx;
    [SerializeField] private Vector3 impactSpawnOffset;
    [SerializeField] private bool enableByDefault;
    [HideInInspector] public List<GameObject> toAvoidList;

    private void Awake()
    {
        _damageCasterCollider = GetComponent<Collider>();
        _damagedTargetList = new List<Collider>();

        if(!enableByDefault)
        {
            DisableDamageCaster();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag && !toAvoidList.Contains(other.gameObject) && !_damagedTargetList.Contains(other))
        {
            Health targetHealth = other.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.ApplyDamage(damageData, transform.position);

                // destroy projectile and play impactvfx

                if(impactVfx != null)
                {
                    Instantiate(impactVfx,transform.position + impactSpawnOffset, Quaternion.identity);
                }
                Destroy(gameObject);
            }
            _damagedTargetList.Add(other);
        }
    }


    public void EnableDamageCaster()
    {
        _damagedTargetList.Clear();
        _damageCasterCollider.enabled = true;

    }

    public void DisableDamageCaster()
    {
        _damagedTargetList.Clear();
        _damageCasterCollider.enabled = false;

    }


    /*public void SetDamageData(StatusEffectType seType, float buildUpAmount, int damageAmt, GameObject toAvoid )
    {
        damageData = new DamageData( damageAmt, seType, buildUpAmount);
        this.toAvoidList.Add(toAvoid);
    }*/
}
