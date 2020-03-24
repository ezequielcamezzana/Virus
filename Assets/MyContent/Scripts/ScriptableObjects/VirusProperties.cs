using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Properties/Virus")]
public class VirusProperties : Properties
{
    [Header("Movement Properties")]
    public float speed;
    public float dashTime;
    [Header("Attack Properties")]
    public float damage;
    public float timeBetweenAttack;
    [Header("Distance Attack Properties")]
    public float projectileSpeed;
}
