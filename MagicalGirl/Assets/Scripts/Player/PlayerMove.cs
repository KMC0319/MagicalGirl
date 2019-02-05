using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    Vector3 temp;

    float moveX = 0f;
    float moveY = 0f;

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
        Ray ray = new Ray(transform.position, transform.up*-0.5f);
        float x = Input.GetAxisRaw("Horizontal");
        moveX = Input.GetAxis("Horizontal") * speed * 0.7f;
        if (Input.GetButtonDown("Jump") && Physics.Raycast(pos, Vector3.down, out hit, 2f, LayerMask.GetMask("Ground")))
        {
            rb.AddForce(transform.up * speed * 0.5f, ForceMode.Impulse);
        }
        else if (Input.GetButtonDown("Jump") && jumpcount > 0)
        {
            rb.AddForce(transform.up * speed * 0.5f,ForceMode.Impulse);
            jumpcount--;
        }
        if (jumpcount<=0 && Physics.Raycast(pos, Vector3.down, out hit, 2f, LayerMask.GetMask("Ground")))
        {
            jumpcount = 1;
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
    }
    void OnDrawGizmos()
    {
        //　Capsuleのレイを疑似的に視覚化
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.up*-0.5f, 0.1f);
    }

    void setBool()
    {
        animator.SetBool("attack", false);
        animator.SetBool("chage", false);
    }
}
