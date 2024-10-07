using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent; // Action�� ������ void�� ��ȯ, �ƴϸ� Func
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
        // TODO : ���� �ѹ� ����
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
        OnMoveEvent?.Invoke(direction); // ?. : ������ ���� ������ ����
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