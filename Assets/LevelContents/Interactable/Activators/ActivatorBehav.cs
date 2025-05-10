using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ActivatorBehav : MonoBehaviour
{
    private bool activated;
    private Light2D activatorLight;

    private void Start()
    {
        activatorLight = GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Activator")
        {
            ChangeActivated();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Activator")
        {
            ChangeActivated();
        }
    }

    void ChangeActivated()
    {
        activated = !activated;
        if(activated.Equals(false))
        {
            activatorLight.intensity = 0;
        }
        else
        {
            activatorLight.intensity = 1;
        }
    }

    public bool GetActivated()
    {
        return activated;
    }
}
