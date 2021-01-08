using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Walking")]

    [SerializeField] float playerSpeed = 100f;

    [Header("Jump")]

    [SerializeField] float jumpUpForce = 10f;
    [SerializeField] float jumpRightForce = 2f;
    [SerializeField] float fallMultiplier = 2f;


    [Header("References")]

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator _animatorRef;
    
    private bool facingRight = true;
    public bool jumping = false;


    public float playerInput;
    public bool grounded;
    

    private void Update()
    {
        playerInput = Input.GetAxis("Horizontal");

        _animatorRef.SetFloat("walking", Mathf.Abs(playerInput));

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
        if (grounded)
        {
            rb.velocity = new Vector2(playerInput * playerSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
    }

    private void Jump()
    {
        //rb.AddForce(Vector2.up * jumpUpForce, ForceMode2D.Impulse);

        if (facingRight)
        {
            jumping = true;
            rb.AddForce(new Vector2(jumpRightForce, jumpUpForce), ForceMode2D.Impulse);
        }
        else
        {
            jumping = true;
            rb.AddForce(new Vector2(-jumpRightForce, jumpUpForce), ForceMode2D.Impulse);
        }
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
        jumping = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        grounded = false;
    }
}
