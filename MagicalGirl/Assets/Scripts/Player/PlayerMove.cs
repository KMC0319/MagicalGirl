using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    private float speed;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Vector3 pos = transform.position;


        if (Input.GetAxis("Horizontal") > 0.25)
        {
            pos.x += speed*0.025f;

        }
        else if(Input.GetAxis("Horizontal") < -0.25)
        {
            pos.x -= speed*0.025f;
        }

        if (Input.GetButton("Vertical"))
        {
            rigidbody.AddForce(transform.up*speed*5);
        }
        else if(Input.GetAxis("Vertical") == 0&&transform.position.y > 0.8)
        {
            rigidbody.AddForce(transform.up * speed * -5);
        }
        if (Input.GetButtonDown("Submit"))
        {
            animator.SetBool("attack", true);
        }
        transform.position = pos;
    }

    void setBool()
    {
        animator.SetBool("attack", false);
    }
}
