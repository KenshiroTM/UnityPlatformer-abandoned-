using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardTilemap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.gameObject.CompareTag("Player"))
            {
                if (this.enabled == true)
                {
                    GameManager.Instance.UpdateGameState(GameState.PlayerLoses);
                    this.enabled = false;
                }
            }
    }
}
