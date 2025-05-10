using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Player_Controller playerController;
    public float moveSpeed = 8;
    public float jumpForce = 5;
    public float jumpDuration = 0.5f;
    private float movement;

    bool isGrounded = true;
    float jumpEnd;

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGrounded();
        CheckCeiling();

        if(jumpEnd <= Time.time)
        {
            playerController.playerRigidbody.velocity = new Vector2(movement * moveSpeed + Time.fixedDeltaTime, playerController.playerRigidbody.velocity.y);
        }
        else
        {
            playerController.playerRigidbody.velocity = new Vector2(movement * moveSpeed + Time.fixedDeltaTime, jumpForce);
        }
    }

    public void SetMovement(float x)
    {
        movement = x;
    }

    public void Jump()
    {
        if(isGrounded == true)
        {
            jumpEnd = Time.time + jumpDuration;
        }
    }

    public void BreakJump()
    {
        if(isGrounded == false)
        {
            jumpEnd = Time.time - 1;
        }
    }
    
    public void CheckGrounded()
    {
            if (playerController.feetPos.OverlapCollider(playerController.whatIsJumpable, playerController.feetPos.GetComponents<BoxCollider2D>()) > 0)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        
    }
    public void CheckCeiling()
    {
            if (playerController.ceilingPos.OverlapCollider(playerController.whatIsJumpable, playerController.ceilingPos.GetComponents<CircleCollider2D>()) > 0)
            {
                jumpEnd = Time.time - 1;
            }
        
    }
}
