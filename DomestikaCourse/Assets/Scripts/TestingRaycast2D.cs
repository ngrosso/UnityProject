using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingRaycast2D : MonoBehaviour
{

    private Animator _animator;
    private Weapon _weapon;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _animator.SetBool("isIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) _animator.SetTrigger("shoot");
    }
    void canShoot()
    {
        if (_weapon != null)
        {
            //llamo al metodo de shoot con raycast
            StartCoroutine(_weapon.ShootWithRaycast());
        }
    }
}
