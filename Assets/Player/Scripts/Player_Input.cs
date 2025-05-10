using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    public Player_Controller playerController;

    // Update is called once per frame
    void Update()
    {
        playerController.playerMovement.SetMovement(Input.GetAxis("Horizontal"));
        if(Input.GetButtonDown("Jump"))
        {
            playerController.playerMovement.Jump();
        }
        else if(Input.GetButtonUp("Jump"))
        {
            playerController.playerMovement.BreakJump();
        }
    }
}
