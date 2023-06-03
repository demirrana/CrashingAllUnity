using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon; //Tower's to be turned part
    [SerializeField] ParticleSystem projectileParticles; //Since the particle effect will be activated and deactivated
    [Tooltip("Tower's range that it can shoot.")]
    [SerializeField] float range = 15f;

    Transform target; //The target that the tower will aim at

    void Update()
    {
        FindClosestEnemy();
        AimWeapon();
    }

    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.transform.position); //The distance between tower and the closest enemy to the tower

        weapon.LookAt(target); //Tower's reversible part turns towards the target.

        if (targetDistance <= range) //Attacks the enemy if it is in the range
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float minDistance = Mathf.Infinity;
        float targetDistance = minDistance;
        Transform closestTarget = null;

        foreach (Enemy enemy in enemies) //Finds the closest enemy to the tower
        {
            targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < minDistance)
            {
                minDistance = targetDistance;
                closestTarget = enemy.transform;
            }
        }

        target = closestTarget;
    }

    void Attack(bool isActive) //Activates the particle effects if the enemy is in the range, deactivates otherwise
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }
}
