using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 movement;
    private SpriteRenderer sr;
    private Animator animator;
    private Rigidbody2D rb;

    void Awake() {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();     
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 || movement.y != 0) {
            animator.SetBool("Walking", true);
        }
        else {
            animator.SetBool("Walking", false);
        }
        
        if (movement.x > 0.1f) {
            sr.flipX = true;
        }
        else if (movement.x < -0.1f) {
            sr.flipX = false;
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
