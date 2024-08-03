using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepAudio : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource footstepSource;

    [Header("Footstep Clips")]
    public AudioClip[] footstepClips;

    [Header("Settings")]
    public float footstepInterval = 0.5f;

    private float lastFootstepTime = 0f;

    // Play a random footstep sound
    public void PlayFootstep()
    {
        if (Time.time - lastFootstepTime >= footstepInterval)
        {
            int index = Random.Range(0, footstepClips.Length);
            footstepSource.PlayOneShot(footstepClips[index]);
            lastFootstepTime = Time.time;
        }
    }
}