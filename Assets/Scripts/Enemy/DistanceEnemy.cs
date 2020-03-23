using UnityEngine;
using System.Collections;

public class DistanceEnemy : Enemy
{
    private ShootingController shootingController;
    private IDamageable damageable;
    private float attackTime;

    void Awake()
    {
        base.Awake();
        shootingController = GetComponent<ShootingController>();
        damageable = target.GetComponent<Virus>();
    }
    void Update()
    {
        if (target)
        {
            Move();
            if (properties.stopDistance >= playerDistance)
            {
                movementController.Rotate(playerDirection);
                if (Time.time >= attackTime)
                {
                    shootingController.Shoot(properties.projectileSpeed, properties.damage);
                    attackTime = Time.time + properties.timeBetweenAttack;
                }
            }
        }
    }
}
