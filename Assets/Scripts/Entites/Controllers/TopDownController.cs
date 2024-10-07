using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent; // Action�� ������ void�� ��ȯ, �ƴϸ� Func
    public event Action<Vector2> OnLookEvent;
    public event Action OnAttackEvent;

    protected bool IsAttacking { get; set; }

    private float _timeSinceLastAttack = float.MaxValue;

    // protected �� ���� : ���� �ٲٰ� ������ �������°� �� ��ӹ޴� Ŭ�����鵵 �� �� �ְ�
    protected CharacterStatHandler Stats { get; private set; }

    protected virtual void Awake()
    {
        Stats = GetComponent<CharacterStatHandler>();
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if(_timeSinceLastAttack < Stats.CurrentStat._attackSO.delay)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }
        else if(IsAttacking && _timeSinceLastAttack >= Stats.CurrentStat._attackSO.delay)
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