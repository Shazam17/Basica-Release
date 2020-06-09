using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseVoice : MonoBehaviour
{

    public string file;
    public string level;
    public string module;
    // Start is called before the first frame update

    AudioSource audioSource;
    AudioClip clip;


    public bool lck = false;

    public void PlayProlog()
    {
        if (!audioSource.isPlaying && !lck)
        {
            audioSource.PlayOneShot(clip);
            LockItems();


        }
    }
    public void LockItems()
    {
        var found = GameObject.FindGameObjectsWithTag("Respawn");

        foreach (var obj in found)
        {
            var comp = obj.GetComponent<dragFigure>();
            comp.StartCoroutine(comp.waitForAudio());
        }

    }
    void Start()
    {
        string path = PlayerPrefs.GetString("voicePath");

        AudioClip clip = Resources.Load<AudioClip>(path + module + "/" +level +"/"+ file);
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
        this.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
