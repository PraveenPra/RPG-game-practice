using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 5;
    private float xInput;
    private int facingDir = 1;//for future use 
    private bool facingRight = true;
    private Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        Movement();
        CheckInputs();
        FlipController();
        AnimationControllers();

    }
    private void CheckInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal");


        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }
    private void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);
    }

     private void Flip()
    {
       facingDir *= -1;
       facingRight = !facingRight;
       transform.Rotate(0,180,0);
    }

     private void FlipController()
    {  //when player start moving right but was looking leftward
       if(rb.velocity.x >0 && !facingRight) Flip();
       //when player start moving left but was looking rightward
       if(rb.velocity.x <0 && facingRight) Flip();
    }
}
