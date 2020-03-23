using UnityEngine;
using System.Collections;

public class MeleeEnemy : Enemy
{
    private MeleeController meleeController;
    private IDamageable damageable;
    private float nextAttackTime;

    void Awake()
    {
        base.Awake();
        meleeController = GetComponent<MeleeController>();
        damageable = target.GetComponent<Virus>();
    }
    void Update()
    {
        if (target)
        {
            Move();
            if (properties.stopDistance >= playerDistance && Time.time >= nextAttackTime)
            {
                meleeController.Attack(damageable, properties.damage, target.position, properties.attackSpeed);
                nextAttackTime = Time.time + properties.timeBetweenAttack;
            }
        }
    }
}
