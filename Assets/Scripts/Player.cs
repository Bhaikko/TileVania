using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("Effects Climp and Move Speed Of Player")]
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float jumpSpeed = 5.0f;

    Rigidbody2D myRigidbody = null;
    Animator animator = null;
    CapsuleCollider2D myBodyCollider = null;
    BoxCollider2D myFeetCollider = null;

    float gravityScaleAtStart = 1.0f;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();

        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead) { 
            Run();
            Jump();
            Climb();
            CheckDeath();
        }
    }

    private void Jump()
    {
        // Testing for collision with Layer
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (Input.GetButtonDown("Jump"))
        {
            myRigidbody.velocity += new Vector2(0.0f, jumpSpeed);
        }

    }

    private void Run()
    {
        float xSpeed = Input.GetAxis("Horizontal") * moveSpeed;
        Vector2 playerVelocity = new Vector2(xSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        if (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon) {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1.0f);
            animator.SetBool("IsRunning", true);
        } else {
            animator.SetBool("IsRunning", false);
        }
    }

    private void Climb()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            myRigidbody.gravityScale = gravityScaleAtStart;
            animator.SetBool("IsClimbing", false);
            return;
        }
        
        float climbSpeed = Input.GetAxis("Vertical") * moveSpeed;
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, climbSpeed);
        myRigidbody.gravityScale = 0.0f;

        animator.SetBool("IsClimbing", Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon);
        
    }

    private void CheckDeath()
    {
        if (isDead) {
            return;
        }


        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))) {
            FindObjectOfType<GameSession>().ProcessDeath();
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("IsDead", true);
        myRigidbody.velocity = new Vector2(1.0f, 10.0f);
        isDead = true;
    }
}
