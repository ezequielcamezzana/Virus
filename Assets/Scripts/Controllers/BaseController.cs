using UnityEngine;
using System.Collections;

public abstract class BaseController : MonoBehaviour
{
    protected Rigidbody2D rigidbody2D;
    protected void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

}
