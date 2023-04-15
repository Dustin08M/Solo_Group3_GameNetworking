using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPun
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float horizontal;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform CheckGround;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private List<Color> _playerAssignmentColors;
    [SerializeField] private float JumpPower = 12f;

    private SpriteRenderer _spriteRenderer;
    //private Transform HpBarPlacement;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //HpBarPlacement.transform.position = transform.position; backup in case i need
        //PhotonNetwork.Instantiate("Prefabs/Player_Hp_Bar", gameObject.transform.position, Quaternion.identity); backup
        var assignment =
            _playerAssignmentColors[Mathf.Min(photonView.Controller.ActorNumber - 1, _playerAssignmentColors.Count - 1)];
        _spriteRenderer.color = assignment;
    }

    private void Update()
    {
        //HpBarPlacement.transform.position = new Vector2(transform.position.x, transform.position.y + 5f);
        if (photonView.IsMine)
        {
            horizontal = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpPower);
            }

            if (Input.GetButtonDown("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * _moveSpeed, rb.velocity.y);
        Vector2 direction = new Vector2(horizontal, 0).normalized;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(CheckGround.position, 0.2f, groundLayer);
    }
}
