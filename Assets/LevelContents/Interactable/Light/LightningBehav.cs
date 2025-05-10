using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightningBehav : MonoBehaviour
{

    private Light2D torchLight;
    public VisionSeekerBehav assignedBlinder;

    public SpriteRenderer lightTexture;
    public Sprite litTexture;
    public Sprite unlitTexture;

    private float baseIntensity;

    float fadeDuration;
    float tElapsed;
    bool activated;

    private void Awake()
    {
        torchLight = GetComponent<Light2D>();
        baseIntensity = torchLight.intensity;

        if(assignedBlinder != null) { torchLight.intensity = 0; activated = false; }
        else { activated = true; torchLight.intensity = baseIntensity; }

        fadeDuration = assignedBlinder.GetFadeDuration();
    }

    private void FixedUpdate()
    {
        activated = assignedBlinder.GetActivated();

        if (activated && torchLight.intensity != baseIntensity)
        {
            if (tElapsed < fadeDuration)
            {
                torchLight.intensity = Mathf.Lerp(0f, 1 ,tElapsed / fadeDuration); //fading swiatla z lerpa
                tElapsed += Time.fixedDeltaTime;
            }
            else
            {
                torchLight.intensity = 1f; // zaokragla sie do wartosci jak minine czas lerpa
                tElapsed = 0;
            }
        }
        if(activated)
        {
            lightTexture.sprite = litTexture;
        }
        else { lightTexture.sprite = unlitTexture; }
    }
}
