using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public Vector2 DirectionVector { get; private set; }
    public Vector2 MovementVector { get; private set; }
    public bool ShootAction { get; private set; }
    public bool DashAction { get; private set; }

    public bool useMouse = true;

    //public static event Action OnJumpButtonDown;
    //public static event Action OnJumpButtonUp;
    //public static event Action OnJumpButtonPress;
    //public static event Action<float> OnMovingHorizontally;
    //public static event Action OnChangeRight;
    //public static event Action OnChangeLeft;

    void Update()
    {
        MovementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        ShootAction = Input.GetButtonDown("Fire1") || Input.GetAxis("Fire1") == -1;
        DashAction = Input.GetButton("Fire3");
        if (useMouse)
        {
            DirectionVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }
        else
        {
            DirectionVector = new Vector2(Input.GetAxis("DirectionX"), Input.GetAxis("DirectionY"));
        }
    }
}
