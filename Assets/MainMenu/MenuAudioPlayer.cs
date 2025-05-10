using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuAudioPlayer : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer audioMixer; // mikser audio, zawiera Effects i Music

    [Header("Menu Music")]
    public AudioSource menuMusic; // tutaj puszczana jest muzyka
}