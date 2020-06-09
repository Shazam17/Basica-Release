using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class playIntro : MonoBehaviour
{
    public const string voicePath = "voicePath";
    bool played = false;

    public AudioClip male;
    public AudioClip female;
    // Start is called before the first frame update
    void Start()
    {
        string path = PlayerPrefs.GetString("voicePath");
        var aS = GetComponent<AudioSource>();
        if (path == "женский/")
        {
            if (!played)
            {
                aS.PlayOneShot(female);
                played = true;
            }
        }
        else if(path == "мужской/")
        {  
            if (!played)
            {
                aS.PlayOneShot(male);
                played = true;
            }
        }
        else
        {
            PlayerPrefs.SetString("voicePath", "женский/");
            if (!played)
            {
                aS.PlayOneShot(female);
                played = true;
            }
        }

       
    }

  

    // Update is called once per frame
    void Update()
    {
        
    }
}
