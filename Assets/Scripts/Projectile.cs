using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string targetTag = "Enemy"; // The tag of the target GameObjects
    public float searchRange = 10f;
    public Transform target; // The target to move towards
    public float speed = 5f; // The speed of the projectile
    public float rotationSpeed = 12f;
    private ParticleSystem ps;
    private DamageCaster damageCaster;

    private float autoDestructTimer = 1f;
    private float autoDestructCD;


    private void Awake()
    {
        damageCaster = GetComponent<DamageCaster>();
        ps = GetComponentInChildren<ParticleSystem>();
    }
    private void Start()
    {
        if(target == null)
            FindClosestTarget();
        
    }
    private void Update()
    {
        if (target == null)
        {
            autoDestructCD += Time.deltaTime;

            if(autoDestructCD > autoDestructTimer)
            {
                Destroy(gameObject);
            }
        }
       else
        {
            // Calculate the direction to the target
            Vector3 direction = target.position - transform.position;
            direction.Normalize(); // Normalize the direction vector

            

            /*Quaternion targetRotation = Quaternion.LookRotation(-direction, Vector3.up);
            particleSystem.transform.rotation = Quaternion.RotateTowards(particleSystem.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);*/

            // Move the projectile towards the target
            transform.Translate(direction * speed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(-direction, Vector3.up);

            // Apply the calculated rotation directly without interpolation
            ps.transform.rotation = targetRotation;
            // If the projectile reaches the target, destroy it
            /*if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                Destroy(gameObject);
            }*/
        }
    }

    private void FindClosestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance < searchRange)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy.transform;
            damageCaster.EnableDamageCaster();
        }
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
        damageCaster.EnableDamageCaster();
    }
}