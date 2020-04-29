using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 50.0f;

    Rigidbody2D myRigidbody = null;
    Animator animator = null;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    private void Run()
    {
        float xSpeed = Input.GetAxis("Horizontal") * moveSpeed;
        Vector2 playerVelocity = new Vector2(xSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        if (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon) {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1.0f);
        }

        animator.SetBool("IsRunning", myRigidbody.velocity.magnitude > 0);

    }
}
