using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Properties/Enemy")]
public class EnemyProperties : Properties
{
    [Header("Spawn Properties")]
    public int stage = 1;
    [Header("Movement Properties")]
    public MovementMode movementMode;
    public float speed;
    public float stopDistance;
    [Header("Attack Properties")]
    public float damage;
    public float timeBetweenAttack;
    [Header("Melee Attack Properties")]
    public float attackSpeed;
    [Header("Distance Attack Properties")]
    public float projectileSpeed;
}
