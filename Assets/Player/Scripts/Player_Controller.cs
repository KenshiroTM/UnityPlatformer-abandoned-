using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player_Controller : MonoBehaviour
{
    public Player_Movement playerMovement;
    public Player_Input playerInput;
    public Collider2D feetPos;
    public Collider2D ceilingPos;
    public Rigidbody2D playerRigidbody;
    public ContactFilter2D whatIsJumpable;
    public Light2D playerLight;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Hazard"))
        {
            if(this.enabled == true)
                {
                GameManager.Instance.UpdateGameState(GameState.PlayerLoses);
                this.enabled = false;
            }
        }
    }
}
