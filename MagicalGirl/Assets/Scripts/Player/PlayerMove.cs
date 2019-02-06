using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;
using System;

public class PlayerMove : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    Vector3 temp;

    [SerializeField]
    private float speed;
    [SerializeField]
    private int jumpcount = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Vector3 pos = transform.position;
        RaycastHit hit;
        float x = Input.GetAxisRaw("Horizontal");
        bool onGround = Physics.Raycast(pos, Vector3.down, out hit, 2f, LayerMask.GetMask("Ground"));

        if (Input.GetButtonDown("Jump") && Physics.Raycast(pos, Vector3.down, out hit, 2f, LayerMask.GetMask("Ground")))
        {
            animator.SetBool("jump", true);
            rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
            rb.AddForce(transform.up * 10f, ForceMode.Impulse);
        }
        else if (Input.GetButtonDown("Jump") && jumpcount > 0)
        {
            animator.Play("PlayerJump");
            rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
            rb.AddForce(transform.up * 8f,ForceMode.Impulse);
            jumpcount--;
        }

        if (onGround)
        {
            animator.SetBool("jump", false);
            jumpcount = 1;
        }
        else
        {
            animator.SetBool("jump", true);
        }

        if (x != 0)
        {
            temp = transform.localScale;
            temp.x = x*4;
            transform.localScale = temp;
        }

        animator.SetBool("walk", x != 0);
        var s = speed;
        if (animator.GetBool("attack")) s /= 2f;
        if (Input.GetButton("Horizontal")) {
            rb.velocity = new Vector3(x * s, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}
