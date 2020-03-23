using UnityEngine;
using System.Collections;

public class Virus : MonoBehaviour, IDamageable, IDestructible
{
    private PlayerInput playerInput;
    private MovementController movementController;
    private ShootingController shootingController;

    public VirusProperties properties;

    private float dashTime;
    private float attackTime;
    private float LifePoints { get; set; }


    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        movementController = GetComponent<MovementController>();
        shootingController = GetComponent<ShootingController>();
        dashTime = properties.dashTime;
        LifePoints = properties.lifePoints;
    }

    void Update()
    {
        movementController.Rotate(playerInput.DirectionVector);
        movementController.Move(playerInput.MovementVector, properties.speed);
        if (playerInput.ShootAction && Time.time >= attackTime)
        {
            shootingController.Shoot(properties.projectileSpeed, properties.damage);
            attackTime = Time.time + properties.timeBetweenAttack;
        }
        if (playerInput.DashAction)
        {
            Vector2 direction = playerInput.MovementVector != Vector2.zero ? playerInput.MovementVector : (Vector2)transform.up;
            movementController.Dash(direction, properties.speed, ref dashTime);
        }
        else
        {
            dashTime = properties.dashTime;
        }
    }

    public void TakeDamage(float damage)
    {
        LifePoints -= damage;
        if (LifePoints <= 0)
        {
            DestroyItSelf();
        }
    }

    public void DestroyItSelf()
    {
        Destroy(gameObject);
    }
}
