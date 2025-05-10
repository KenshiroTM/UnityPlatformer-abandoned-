using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complete_Level : MonoBehaviour
{
    public SpriteRenderer openedDoorRender;
    public Sprite openedDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            openedDoorRender.sprite = openedDoor;
            GameManager.Instance.UpdateGameState(GameState.LevelCompleted);
        }
    }
}
