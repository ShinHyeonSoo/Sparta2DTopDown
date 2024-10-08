using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // ���� �ε����� �� ������鼭 ����Ʈ ������ �ؾߵż� ���̾ �˰� �־�� �ؿ�!
    [SerializeField] 
    private LayerMask _levelCollisionLayer;

    private RangedAttackSO _attackData;
    private float _currentDuration;
    private Vector2 _direction;
    private bool _isReady;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private TrailRenderer _trailRenderer;

    public bool _fxOnDestory = true;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (!_isReady)
        {
            return;
        }

        _currentDuration += Time.deltaTime;

        if (_currentDuration > _attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody.velocity = _direction * _attackData.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // levelCollisionLayer�� ���ԵǴ� ���̾����� Ȯ���մϴ�.
        if (IsLayerMatched(_levelCollisionLayer.value, collision.gameObject.layer))
        {
            // �������� �浹�� �������κ��� �ణ �� �ʿ��� �߻�ü�� �ı��մϴ�.
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - _direction * .2f;
            DestroyProjectile(destroyPosition, _fxOnDestory);
        }
        // _attackData.target�� ���ԵǴ� ���̾����� Ȯ���մϴ�.
        else if (IsLayerMatched(_attackData.target.value, collision.gameObject.layer))
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                bool isAttackApplied = healthSystem.ChangeHealth(-_attackData.power);

                if(isAttackApplied && _attackData.isOnKnockback)
                {
                    ApplyKnockback(collision);
                }
            }
            // �浹�� �������� �߻�ü�� �ı��մϴ�.
            DestroyProjectile(collision.ClosestPoint(transform.position), _fxOnDestory);
        }
    }

    private void ApplyKnockback(Collider2D collision)
    {
        TopDownMovement movement = collision.GetComponent<TopDownMovement>();
        if (movement != null)
        {
            movement.ApplyKnockback(transform, _attackData.knockbackPower, _attackData.knockbackTime);
        }
    }

    // ���̾ ��ġ�ϴ��� Ȯ���ϴ� �޼ҵ��Դϴ�.
    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }

    public void InitializeAttack(Vector2 direction, RangedAttackSO attackData)
    {
        this._attackData = attackData;
        this._direction = direction;

        UpdateProjectileSprite();
        _trailRenderer.Clear();
        _currentDuration = 0;
        _spriteRenderer.color = attackData.projectileColor;

        transform.right = this._direction;

        _isReady = true;
    }

    private void UpdateProjectileSprite()
    {
        transform.localScale = Vector3.one * _attackData.size;
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            // TODO : ParticleSystem�� ���ؼ� ����, ���� NameTag�� �ش��ϴ� FX��������
        }
        gameObject.SetActive(false);
    }
}
