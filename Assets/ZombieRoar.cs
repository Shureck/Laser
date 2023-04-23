using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieRoar : MonoBehaviour
{
    public AudioClip audioClip;  // The audio clip to be played
    public float minTime = 10f;  // The minimum time between audio plays
    public float maxTime = 15f;  // The maximum time between audio plays

    private AudioSource audioSource;
    private float timer = 0f;
    private float nextTime = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nextTime = Random.Range(minTime, maxTime);  // Set the initial time for the first audio play
    }

    void Update()
    {
        timer += Time.deltaTime;

        // If it's time to play the audio, play it and set the next play time
        if (timer >= nextTime)
        {
            audioSource.PlayOneShot(audioClip);
            nextTime = Random.Range(minTime, maxTime);
            timer = 0f;
        }
    }
}