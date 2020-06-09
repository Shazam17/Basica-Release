using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class voiceChanger : MonoBehaviour
{
    public Image img;

    public Sprite sprPlus;
    public Sprite sprMinus;

    void Start() {
        var audio = PlayerPrefs.GetString("voicePath");
        if (audio == gameObject.name)
        {
            img.sprite = sprPlus;
        }
        else
        {
            img.sprite = sprMinus;

        }
    }

    public void SetMaleVoice()
    {
        GameObject[] found = GameObject.FindGameObjectsWithTag("Respawn");
        foreach(var obj in found)
        {
            obj.GetComponent<voiceChanger>().img.sprite = sprMinus;
        }
        img.sprite = sprPlus;
        PlayerPrefs.SetString("voicePath", "мужской/");
    }

    public void SetFemaleVoice()
    {
        GameObject[] found = GameObject.FindGameObjectsWithTag("Respawn");

        foreach (var obj in found)
        {
            obj.GetComponent<voiceChanger>().img.sprite = sprMinus;

        }
        img.sprite = sprPlus;
        PlayerPrefs.SetString("voicePath", "женский/");
    }


  

    // Update is called once per frame
    void Update()
    {
        
    }
}
