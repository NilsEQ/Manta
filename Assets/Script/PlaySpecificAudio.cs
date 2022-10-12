using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySpecificAudio : MonoBehaviour
{
    [SerializeField] private AudioSource myAudiosource;
    [SerializeField] private AudioClip myAudioClip1;
    [SerializeField] private float volume = 1.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))

        {
            myAudiosource.PlayOneShot(myAudioClip1, volume);

        }


    }
}
