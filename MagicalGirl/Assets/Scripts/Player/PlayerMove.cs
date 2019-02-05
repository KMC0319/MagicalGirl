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
    private int jumpcount = 2;

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
        moveX = Input.GetAxis("Horizontal") * speed * 0.7f;
        if ((Input.GetButtonDown("Vertical") && Physics.SphereCast(ray, -0.1f, out hit, 0.8f, LayerMask.GetMask("Ground"))) || (Input.GetButtonDown("Vertical") && jumpcount > 0))
        {
            rb.AddForce(transform.up * speed * 0.5f,ForceMode.Impulse);
            jumpcount--;
        }
        if(jumpcount<=0&& Physics.SphereCast(ray, -0.1f, out hit, 0.8f, LayerMask.GetMask("Ground")))
        {
            jumpcount = 2;
        }
        if (moveX < 0)
        {
            transform.localScale = new Vector3(-1,1,1); 
        }
        else if(moveX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
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
        Gizmos.DrawWireSphere(transform.position + transform.up*-0.5f, 0.1f);
    }

    void setBool()
    {
        animator.SetBool("attack", false);
    }
}
