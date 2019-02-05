using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;


    float moveX = 0f;
    float moveY = 0f;

    [SerializeField]
    private float speed;

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
        moveX = Input.GetAxis("Horizontal") * speed;
        if (Input.GetButtonDown("Vertical") && Physics.SphereCast(ray, -0.5f, out hit, 1.1f, LayerMask.GetMask("Ground")))
        {
            rb.AddForce(transform.up * speed * 0.7f,ForceMode.Impulse);
        }
        if (Input.GetButton("Horizontal"))
        {
            rb.velocity = new Vector3(moveX,rb.velocity.y,0);
        }
        if (Input.GetButtonDown("Submit"))
        {
            animator.SetBool("attack", true);
        }
    }
    void OnDrawGizmos()
    {
        //　Capsuleのレイを疑似的に視覚化
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.up*-0.5f, 0.5f);
    }

    void setBool()
    {
        animator.SetBool("attack", false);
    }
}
