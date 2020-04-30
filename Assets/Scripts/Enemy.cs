using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;

    Rigidbody2D myRigidBody = null;
    BoxCollider2D myBoxCollider = null;

    bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingRight) { 
            myRigidBody.velocity = new Vector2((Vector2.right * moveSpeed).x, myRigidBody.velocity.y);
        } else {
            myRigidBody.velocity = new Vector2((Vector2.left * moveSpeed).x, myRigidBody.velocity.y);
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }


}
