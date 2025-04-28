using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPitchChanger : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.pitch = Random.Range(0.6f, 1.4f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
