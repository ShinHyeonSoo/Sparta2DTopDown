﻿using System;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    // 실제로 이동이 일어날 컴포넌트
    private TopDownController _controller;
    private Rigidbody2D _movementRigidbody;
    private CharacterStatHandler _characterStatHandler;

    private Vector2 _movementDirection = Vector2.zero;
    private Vector2 _knockback = Vector2.zero;
    private float _knockbackDuration = 0.0f;

    private void Awake()
    {
        // 주로 내 컴포넌트 안에서 끝나는 것
        _controller = GetComponent<TopDownController>();
        _movementRigidbody = GetComponent<Rigidbody2D>();
        _characterStatHandler=  GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
        _controller.OnMoveEvent += Move;
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void FixedUpdate()
    {
        // FixedUpdate는 물리업데이트 관련
        // rigidbody의 값을 바꾸니까 FixedUpdate
        ApplyMovement(_movementDirection);

        if(_knockbackDuration > 0.0f)
        {
            _knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    public void ApplyKnockback(Transform Other, float power, float duration)
    {
        _knockbackDuration = duration;
        _knockback = -(Other.position - transform.position).normalized * power;
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * _characterStatHandler.CurrentStat._speed;

        if(_knockbackDuration > 0.0f)
        {
            direction += _knockback;
        }

        _movementRigidbody.velocity = direction;
    }
}