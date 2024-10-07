using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent; // Action은 무조건 void만 반환, 아니면 Func
    public event Action<Vector2> OnLookEvent;
    public event Action OnAttackEvent;

    protected bool _isAttacking { get; set; }

    private float _timeSinceLastAttack = float.MaxValue;

    private void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        // TODO : 매직 넘버 수정
        if(_timeSinceLastAttack < 0.2f)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }
        else if(_isAttacking && _timeSinceLastAttack >= 0.2f)
        {
            _timeSinceLastAttack = 0f;
            CallAttackEvent();
        }
    }

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction); // ?. : 없으면 말고 있으면 실행
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }
}