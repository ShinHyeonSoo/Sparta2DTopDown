using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent; // Action은 무조건 void만 반환, 아니면 Func
    public event Action<Vector2> OnLookEvent;
    public event Action OnAttackEvent;

    protected bool IsAttacking { get; set; }

    private float _timeSinceLastAttack = float.MaxValue;

    // protected 한 이유 : 나만 바꾸고 싶지만 가져가는건 내 상속받는 클래스들도 볼 수 있게
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