using System;
using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private TopDownController _controller;

    [SerializeField]
    private Transform _projectileSpawnPos;
    private Vector2 _aimDirection = Vector2.right;

    public GameObject testPrefab;

    private void Awake()
    {
        _controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        _controller.OnAttackEvent += OnShoot;

        _controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        _aimDirection = direction;
    }

    private void OnShoot()
    {
        CreateProjectile();
    }

    private void CreateProjectile()
    {
        // TODO : 날아가지 않기 때문에 날아가게 만들 것
        Instantiate(testPrefab, _projectileSpawnPos.position, Quaternion.identity);
    }
}