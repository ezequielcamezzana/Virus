using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsManager : MonoBehaviour
{
    public float leftConstraint;
    public float rightConstraint;
    public float topConstraint;
    public float bottomConstraint;
    private float buffer = 1.0f;
    private Transform target;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        leftConstraint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.05f, 0, 0)).x;
        rightConstraint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.95f, 0, 0)).x;
        topConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height * 0.95f, 0)).y;
        bottomConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height * 0.05f, 0)).y;
    }

    void Update()
    {
        if (target)
        {
            if (target.position.x < leftConstraint - buffer)
            {
                target.position = new Vector2(rightConstraint + buffer, target.position.y);
            }
            else if (target.position.x > rightConstraint + buffer)
            {
                target.position = new Vector2(leftConstraint - buffer, target.position.y);
            }

            if (target.position.y < bottomConstraint - buffer)
            {
                target.position = new Vector2(target.position.x, topConstraint + buffer);
            }
            else if (target.position.y > topConstraint + buffer)
            {
                target.position = new Vector2(target.position.x, bottomConstraint - buffer);
            }
        }
    }
}
