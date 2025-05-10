using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BasicDoorBehav : MonoBehaviour
{
    public ActivatorBehav assignedActivator;
    public ActivatorBehav assignedSecondActivator;

    private Light2D doorLight;

    public bool horizontalMove; //czy przesuwa sie horyzontalnie czy wertykalnie
    public bool revertedDestination; // czy w odwrotna strone ma sie przesunac

    private int revertedMultiplier;

    private float tElapsed;
    public float destinationTime = 1f;

    private bool activated;
    private bool activatedSecond;

    Vector3 doorStartingPos;
    Vector3 doorEndingPos;

    private void Start()
    {
        doorLight = GetComponent<Light2D>(); //bierze komponent z tego

        doorStartingPos = transform.position;
        activated = false;

        //czy destination ma byæ odwrotna, czyli prawo/lewo, góra/dó³
        if (!revertedDestination) { revertedMultiplier = 1; }
        else { revertedMultiplier = -1; };

        //pozycja gdzie drzwi maj¹ sie przemieœciæ, obejmuje wszystkie warianty
        if(!horizontalMove) { doorEndingPos = new Vector3(transform.position.x, doorStartingPos.y + GetComponent<SpriteRenderer>().bounds.size.y * revertedMultiplier); }
        else { doorEndingPos = new Vector3(doorStartingPos.x + GetComponent<SpriteRenderer>().bounds.size.x * revertedMultiplier, transform.position.y); }
    }

    private void FixedUpdate()
    {
        if(assignedActivator == null) { activated = false; }
        else { activated = assignedActivator.GetActivated(); } //pierwsza dzwignia

        if (assignedSecondActivator == null) { activatedSecond = false; }
        else { activatedSecond = assignedSecondActivator.GetActivated(); } //druga dzwignia deaktywuje drzwi po pociagnieciu}


        if (activatedSecond == activated) //gdy jest aktywowane drugi raz albo nieaktywne
        {
            if (transform.position != doorStartingPos && tElapsed < destinationTime) //drzwi wracaj¹ do pozycji spoczynkowej
            {
                transform.position = Vector3.Lerp(doorEndingPos, doorStartingPos, tElapsed / destinationTime);
                tElapsed += Time.fixedDeltaTime;
                doorLight.intensity = 0.5f;
            }
            else
            {
                transform.position = doorStartingPos;
                tElapsed = 0;
                doorLight.intensity = 0f;
            }
        }
        else
        {
           if (transform.position != doorEndingPos && tElapsed < destinationTime) // drzwi sie przesuwaj¹
           {
                transform.position = Vector3.Lerp(doorStartingPos, doorEndingPos, tElapsed / destinationTime);
                tElapsed += Time.fixedDeltaTime;
                doorLight.intensity = 0.5f;
           }
           else
           {
                transform.position = doorEndingPos;
                tElapsed = 0;
                doorLight.intensity = 0f;
           }
        }
    }
}
