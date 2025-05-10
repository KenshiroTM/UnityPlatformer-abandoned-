using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class VisionSeekerBehav : MonoBehaviour
{
    public float fadeDuration = 1f;

    bool activated;

    private void Awake() //zawsze zgasza swiatlo gdy jest na mapie
    {
        GameObject.FindGameObjectWithTag("GlobalLightning").GetComponent<Light2D>().intensity = 0f;
    }

    private void FixedUpdate()
    {
        if(activated != true)
        {
            this.GetComponent<Light2D>().color = Color.magenta;
        }
        else
        {
            this.GetComponent<Light2D>().color = Color.white;
        }
    }

    public bool GetActivated()
    {
        return activated;
    }
    public float GetFadeDuration()
    {
        return fadeDuration;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        activated = true;
    }
}
