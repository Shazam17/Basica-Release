using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hooks
{
    static Hooks instance;
    public static Hooks GetInstance()
    {
        if(instance == null)
        {
            instance = new Hooks();
        }
        return instance;
    }

    public static string GetVoicePath()
    {
        return PlayerPrefs.GetString("voicePath");
    }


    
    public IEnumerator ToNewLevel(string level, AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
            var clip = OpenGteets.GetGreet();
            audioSource.PlayOneShot(clip);
        }
        return ToLevelDelayed(level);
    }

    public void PlayDis(AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
            var clip = OpenGteets.GetDis();
            audioSource.PlayOneShot(clip);
        }
    }


    IEnumerator ToLevelDelayed(string level)
    {
        Debug.Log("to level");
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(level);

    }
}
