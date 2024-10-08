using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownContactEnemyController : TopDownEnemyController
{
    [SerializeField][Range(0f, 100f)] 
    private float _followRange;
    [SerializeField] 
    private string _targetTag = "Player";
    private bool _isCollidingWithTarget;

    [SerializeField] private SpriteRenderer _characterRenderer;

    private HealthSystem _healthSystem;
    private HealthSystem _collidingTargetHealthSystem;
    private TopDownMovement _collidingMovement;

    protected override void Start()
    {
        base.Start();

        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.OnDamage += OnDamage;
    }

    private void OnDamage()
    {
        _followRange = 100f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_isCollidingWithTarget)
        {
            ApplyHealthChange();
        }

        Vector2 direction = Vector2.zero;
        if (DistanceToTarget() < _followRange)
        {
            direction = DirectionToTarget();
        }

        CallMoveEvent(direction);
        Rotate(direction);
    }

    private void ApplyHealthChange()
    {
        AttackSO attackSO = Stats.CurrentStat._attackSO;
        bool isAttackable = _collidingTargetHealthSystem.ChangeHealth(-attackSO.power);

        if(isAttackable && attackSO.isOnKnockback && _collidingMovement != null)
        {
            _collidingMovement.ApplyKnockback(transform, attackSO.knockbackPower, attackSO.knockbackTime);
        }
    }

    private void Rotate(Vector2 direction)
    {
        // TopDownAimRotation���� �߾���? 
        // Atan2�� ���ο� ������ ������ �������� -����~����(-180��~180���� ����, * Rad2Deg�� �� ���)�ϴ� ���� ��Ÿ���ִ� �Լ����ٴ� �� ����Ͻ���?
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject receiver = collision.gameObject;

        if(!receiver.CompareTag(_targetTag)) return;

        _collidingTargetHealthSystem = collision.GetComponent<HealthSystem>();
        if(_collidingTargetHealthSystem != null )
        {
            _isCollidingWithTarget = true;
        }

        _collidingMovement = collision.GetComponent<TopDownMovement>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(_targetTag)) return;
        _isCollidingWithTarget = false;
    }
}
