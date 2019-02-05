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


    float moveX = 0f;
    float moveY = 0f;
    [SerializeField]
    int EquipIndex;
    int EquipNo;

    [SerializeField]
    private float speed;
    [SerializeField]
    private int jumpcount = 1;
    [SerializeField]
    PlayerAttack playerAttack;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        EquipIndex = 0;
        EquipNo = 0;
        playerAttack = GetComponent<PlayerAttack>();
    }
    private void Update()
    {
        Vector3 pos = transform.position;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.up*-0.5f);
        float x = Input.GetAxisRaw("Horizontal");
        bool onGround = Physics.Raycast(pos, Vector3.down, out hit, 2f, LayerMask.GetMask("Ground"));
        moveX = Input.GetAxis("Horizontal") * speed * 0.7f;

        if (Input.GetButtonDown("Jump") && Physics.Raycast(pos, Vector3.down, out hit, 2f, LayerMask.GetMask("Ground")))
        {
            animator.SetBool("jump", true);
            rb.AddForce(transform.up * speed * 0.5f, ForceMode.Impulse);
        }
        else if (Input.GetButtonDown("Jump") && jumpcount > 0)
        {
            animator.Play("PlayerJump");
            rb.velocity=new Vector3(moveX,0,0);
            rb.AddForce(transform.up * speed * 0.5f,ForceMode.Impulse);
            jumpcount--;
        }

        if (onGround)
        {
            animator.SetBool("jump", false);
            jumpcount = 1;
            Debug.Log("true");
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
        if (Input.GetButton("Horizontal"))
        {
            rb.velocity = new Vector3(moveX,rb.velocity.y,0);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        switch (EquipIndex)
        {
            case 0:
                animator.SetBool("punch", true);
                animator.SetBool("ironpipe", false);
                animator.SetBool("magicalstick", false);
                break;

            case 1:
                animator.SetBool("punch", false);
                animator.SetBool("ironpipe", true);
                animator.SetBool("magicalstick", false);
                break;

            case 2:
                animator.SetBool("punch", false);
                animator.SetBool("ironpipe", false);
                animator.SetBool("magicalstick", true);
                break;
        }

        if (Input.GetButtonDown("Submit"))
        {
            animator.SetBool("attack", true);
        }
        if (Input.GetButton("Submit"))
        {
            animator.SetBool("chage", true);
        }
        if (Input.GetButtonUp("Submit"))
        {
            animator.SetBool("chage", false);
        }

        if (Input.GetButton("Cancel"))
        {
            EquipNo++;
            if (EquipNo > playerAttack.Items.Count|| EquipNo > playerAttack.Items.Count)
            {
                EquipNo = 0;
            }
            if (playerAttack.Items[EquipNo].ItemName == (EItemName)Enum.ToObject(typeof(EItemName),0))
            {
                EquipIndex = 0;
            }
            else if (playerAttack.Items[EquipNo].ItemName == (EItemName)Enum.ToObject(typeof(EItemName), 1))
            {
                EquipIndex = 1;
            }
            else if (playerAttack.Items[EquipNo].ItemName == (EItemName)Enum.ToObject(typeof(EItemName), 2))
            {
                EquipIndex = 2;
            }
        }
    }

    void setBool()
    {
        animator.SetBool("attack", false);
        animator.SetBool("chage", false);
    }
}
