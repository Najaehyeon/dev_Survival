using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("isMove");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void IsMove(Vector2 obj)
    {
        _animator.SetFloat("Xdir", obj.x);
        _animator.SetFloat("Ydir", obj.y);
        _animator.SetBool(IsMoving, true);
    }

    public void IsIdle()
    {
        _animator.SetBool(IsMoving, false);
    }
}
