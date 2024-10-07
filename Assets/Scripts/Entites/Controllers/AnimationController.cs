using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    protected Animator _animator;
    protected TopDownController _controller;

    protected virtual void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _controller = GetComponent<TopDownController>();
    }
}
