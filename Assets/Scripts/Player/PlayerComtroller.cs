using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerComtroller : MonoBehaviour
{
    private const float GravityValue = -9.81f;

    [SerializeField] public float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpCoef;
    [SerializeField] private Transform cam;

    private CharacterController _controller;

    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private Vector3 moveDir;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        CheckDeathSurface();
    }

    private void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
            _playerVelocity.y = 0f;

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (direction.magnitude >= .1f)
        {
            moveDir = Quaternion.Euler(transform.rotation.eulerAngles) * direction;
        }
        else
        {
            moveDir = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.Space) && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -1.0f * GravityValue) * jumpCoef;
        }


        _playerVelocity.y += GravityValue * Time.deltaTime * jumpCoef;
        _controller.Move(moveDir.normalized * Time.deltaTime * speed + _playerVelocity * Time.deltaTime);
}

    private void CheckDeathSurface()
    {
        var surface = Physics.OverlapBox(Vector3.one, Vector3.one / 2).ToList().Find(collider => collider.CompareTag("DeathSurface"));
        if (_groundedPlayer && surface != null)
        {
            GetComponent<Health>().DamageMe(surface.GetComponent<DeathSurface>().damage * Time.deltaTime);
            Debug.Log("damaged");
        }
    }
}