using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : BaseController
{
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void MoveAnimation(bool value)
    {
        animator.SetBool("IsMoving", value);
    }
}
