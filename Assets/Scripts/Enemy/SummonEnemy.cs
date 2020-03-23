using UnityEngine;
using System.Collections;

public class SummonEnemy : Enemy
{
    private SummoningController summoningController;
    private float nextAttackTime;

    void Awake()
    {
        base.Awake();
        summoningController = GetComponent<SummoningController>();
    }
    void Update()
    {
        if (target)
        {
            Move();
            if (properties.stopDistance >= playerDistance)
            {
                movementController.Rotate(playerDirection);
                if (Time.time >= nextAttackTime)
                {
                    summoningController.Summon();
                    nextAttackTime = Time.time + properties.timeBetweenAttack;
                }
            }
        }
    }
}
