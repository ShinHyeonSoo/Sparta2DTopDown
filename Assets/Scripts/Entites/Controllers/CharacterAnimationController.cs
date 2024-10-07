using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : AnimationController
{
    private static readonly int isWalking = Animator.StringToHash("isWalking");
    private static readonly int isHit = Animator.StringToHash("isHit");
    private static readonly int Attack = Animator.StringToHash("attack");

    private readonly float magnituteThreshold = 0.5f;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _controller.OnAttackEvent += Attacking;
        _controller.OnMoveEvent += Move;
    }
    private void Move(Vector2 vector)
    {
        _animator.SetBool(isWalking, vector.magnitude > magnituteThreshold);
    }

    private void Attacking(AttackSO sO)
    {
        _animator.SetTrigger(Attack);
    }

    private void Hit()
    {
        _animator.SetBool(isHit, true);
    }

    private void InvincibilityEnd()
    {
        _animator.SetBool(isHit, false);
    }

}
