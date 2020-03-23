using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : BaseController
{

    public void Attack(IDamageable target, float damage, Vector3 position, float attackSpeed)
    {
        target.TakeDamage(damage);
        StartCoroutine(AttackAnimation(position, attackSpeed));
    }

    IEnumerator AttackAnimation(Vector3 position, float attackSpeed)
    {
        Vector3 originalPosition = transform.position;
        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, position, formula);
            yield return null;
        }
    }
}
