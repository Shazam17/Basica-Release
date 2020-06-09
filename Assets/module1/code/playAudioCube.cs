using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudioCube : MonoBehaviour
{


    AudioSource audioSource;

    public void Start()
    {
        PlayerPrefs.SetInt("playing", 0);
        audioSource = GetComponent<AudioSource>();

        
    }

    public void Play()
    {
        int playingGlobal = PlayerPrefs.GetInt("playing");
        if (!audioSource.isPlaying && playingGlobal == 0)
        {
            StartCoroutine(LockPlaying());
            audioSource.PlayOneShot(audioSource.clip);
        }
     
    }
   
    public IEnumerator LockPlaying()
    {
        PlayerPrefs.SetInt("playing", 1);
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetInt("playing", 0);

    }

}
