using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _armRenderer;
    [SerializeField]
    private Transform _armPivot;

    [SerializeField]
    private SpriteRenderer _characterRenderer;

    private TopDownController _controller;

    private void Awake()
    {
        _controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        RotateArm(direction);
    }

    private void RotateArm(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;

        _armPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
