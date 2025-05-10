using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollectibleScript : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision) //ontrigger rejestruje tylko czy by³a kolizja, nie pokazuje lokalizacji
    {
        if (collision.gameObject.CompareTag("Player")) //jezeli gracz sie z nim zderzy³
        {
            if(this.enabled == true) //zapobieganie podwojnemu triggerowi i przypisaniu score 2 razy
            {
                Destroy(this.gameObject); // zniszcz obiekt jak juz wezmiesz
                this.enabled = false;

                GameManager.Instance.currentLevelScore += 1; //dodaj score do gamemanagera ktory trzeyma staty progresji levelu
            }
        }
    }
}
