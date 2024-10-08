using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private float _healthChangeDelay = 0.5f;

    private CharacterStatHandler _statHandler;
    private float _timeSinceLastChange = float.MaxValue;
    private bool _isAttacked = false;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public float CurrentHealth { get; private set; }

    public float MaxHealth => _statHandler.CurrentStat._maxHealth;

    private void Awake()
    {
        _statHandler = GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if(_isAttacked && _timeSinceLastChange < _healthChangeDelay)
        {
            _timeSinceLastChange += Time.deltaTime;

            if(_timeSinceLastChange > _healthChangeDelay )
            {
                OnInvincibilityEnd?.Invoke();
                _isAttacked = false;
            }
        }
    }

    public bool ChangeHealth(float change)
    { 
        if(_timeSinceLastChange < _healthChangeDelay)
        {
            // 공격을 하지 않고 끝나는 상황
            return false;
        }

        _timeSinceLastChange = 0.0f;
        CurrentHealth += change;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if(CurrentHealth <= 0f)
        {
            CallDeath();
            return true;
        }

        if(change >=0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            OnDamage?.Invoke();
            _isAttacked = true;
        }

        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}
