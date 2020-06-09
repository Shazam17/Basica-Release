using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayProlog : MonoBehaviour
{

    public void PlayIntro()
    {
        string path = Hooks.GetVoicePath();
        AudioClip clip = Resources.Load<AudioClip>(path + "Цвета/Уровень 3/давай порисуем");
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayIntro();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
