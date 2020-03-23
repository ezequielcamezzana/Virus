using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : BaseController
{

    void Awake()
    {
        base.Awake();
    }

    public void Move(Vector2 direction, float speed)
    {
        Vector2 velocity = direction * speed;
        rigidbody2D.velocity = velocity;
    }

    public void Dash(Vector2 direction, float speed, ref float dashTime)
    {
        if (dashTime > 0)
        {
            dashTime -= Time.deltaTime;
            Vector2 velocity = direction * (speed * 3);
            rigidbody2D.velocity = velocity;
        }
    }


    public void Rotate(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = rotation;
        }
    }
}
