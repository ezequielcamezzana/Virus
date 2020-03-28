using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class RotationObject : MonoBehaviour {

    [Range(-100,100)] public float rotationSpeed;
    [Range(0, 1)] public float x,y,z;

    private void Start()
    {
        UpdateManager.Instance.Execute += Execute;
    }

    private void OnDestroy()
    {
        UpdateManager.Instance.Execute -= Execute;
    }
    
    private void Execute()
    {
        transform.Rotate(new Vector3(x, y, z) * (Time.deltaTime * rotationSpeed * 10000));
    }
}
