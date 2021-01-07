using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Walking")]

    [SerializeField] float playerSpeed = 100f;

    [Header("Jump")]

    [SerializeField] float jumpForce = 10f;
    [SerializeField] float fallMultiplier = 2f;


    [Header("References")]

    [SerializeField] Rigidbody2D rb;
    
    private bool facingRight = true;


    public float playerInput;
    public bool grounded;
    

    private void Update()
    {
        playerInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        if (playerInput != 0)
        {
            MovePlayer();

            if (playerInput < 0 && facingRight)
            {
                FlipSprite();
            }
            if (playerInput > 0 && !facingRight)
            {
                FlipSprite();
            }
        }

        if (!grounded && rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        }
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(playerInput * playerSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void FlipSprite()
    {   
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        grounded = false;
    }
}
