using UnityEngine;
using System.Collections;

//MOVE AWAY
public enum MovementMode
{
    RANDOM,
    FOLLOW_PLAYER
}

public abstract class Enemy : MonoBehaviour, IDamageable, IDestructible
{
    protected MovementController movementController;
    protected Transform target;
    protected Vector3 playerDirection;
    protected float playerDistance;
    public EnemyProperties properties;
    public float lifePoints;

    public void Awake()
    {
        movementController = GetComponent<MovementController>();
        target = GameObject.FindGameObjectWithTag("Player")?.transform;

        Debug.Log(target);

        lifePoints = properties.lifePoints;
    }

    protected void Move()
    {
        playerDirection = target.position - transform.position;
        playerDistance = Vector3.Distance(transform.position, target.position);

        switch (properties.movementMode)
        {
            case MovementMode.RANDOM:
                break;
            case MovementMode.FOLLOW_PLAYER:
                if (properties.stopDistance <= playerDistance)
                {
                    movementController.Rotate(playerDirection.normalized);
                    movementController.Move(playerDirection.normalized, properties.speed);
                }
                else
                {
                    movementController.Move(Vector2.zero, 0);
                }
                break;
        }
    }

    public void TakeDamage(float damge)
    {
        lifePoints -= damge;
        if (lifePoints <= 0)
        {
            DestroyItSelf();
        }
    }

    public void DestroyItSelf()
    {
        Destroy(gameObject);
    }
}
