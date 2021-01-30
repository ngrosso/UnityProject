﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool _isAttacking;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void LateUpdate()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            _isAttacking = true;
        }
        else
        {
            _isAttacking = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAttacking == true)
        {
            Debug.Log("attacking on colision");
            if (collision.CompareTag("Enemy") || collision.CompareTag("BigBullet"))
            {
                Debug.Log("Attacking " + collision.tag);
                collision.SendMessageUpwards("AddDamage");
            }
        }
    }
}
