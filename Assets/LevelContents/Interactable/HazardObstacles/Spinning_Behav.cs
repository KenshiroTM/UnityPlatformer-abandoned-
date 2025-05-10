using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning_Behav : MonoBehaviour
{
    public Transform rotationPivot;
    public float rotationSpeed = 100;
    public bool reverseRotation;

    private int rotationMultiplier;

    private void Awake()
    {
        rotationMultiplier = 1;

        if(reverseRotation)
        {
            rotationMultiplier = -1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(rotationPivot.position, Vector3.forward, rotationSpeed * Time.deltaTime * rotationMultiplier);
    }
}
