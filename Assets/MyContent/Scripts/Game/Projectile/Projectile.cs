using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private float speed;
    private float damage;
    private float lifeTime;
    private GameObject parent;

    public void Init(GameObject parent, float speed, float lifeTime, float damage)
    {
        this.parent = parent;
        this.speed = speed;
        this.lifeTime = lifeTime;
        this.damage = damage;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null && collision.gameObject != parent)
        {
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
