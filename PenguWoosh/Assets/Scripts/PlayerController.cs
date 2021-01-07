using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Walking")]

    [SerializeField] float playerSpeed = 100f;

    [Header("References")]

    [SerializeField] Rigidbody2D rb;


    public float playerInput;
    private bool facingRight = true;

    private void Update()
    {
        playerInput = Input.GetAxis("Horizontal");

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
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(playerInput * playerSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void FlipSprite()
    {   
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
