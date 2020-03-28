using UnityEngine;
using System.Collections;

public class ShootingController : BaseController
{
    public Projectile projectile;
    public Transform[] shootPoints;

    private float proyectileLifeTime = 5;

    void Awake()
    {
        base.Awake();
    }

    public void Shoot(float speed, float damage)
    {
        foreach (Transform shootPoint in shootPoints)
        {
            Projectile projectileInstance = Instantiate(projectile, shootPoint.position, shootPoint.rotation);
            projectileInstance.Init(gameObject, speed, proyectileLifeTime, damage);
        }

    }
}