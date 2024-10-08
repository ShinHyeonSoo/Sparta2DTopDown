using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _healthSystem.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        _rigidBody.velocity = Vector2.zero;

        foreach(SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour behaviour in GetComponentsInChildren<Behaviour>())
        {
            behaviour.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
}
