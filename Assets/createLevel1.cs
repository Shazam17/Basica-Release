using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createLevel1 : MonoBehaviour
{

    public GameObject uiPart;
    private AudioSource audioSource;
    void Start()
    {
        Sprite[] images = Resources.LoadAll<Sprite>("животные_картинки/Уровень 1");


        GetComponent<RectTransform>().sizeDelta = new Vector2(images.Length * 655, 437);

        string path = Hooks.GetVoicePath();

        audioSource = GetComponent<AudioSource>();



        foreach (var image in images){
            var go = Instantiate(uiPart, gameObject.transform);
            go.GetComponent<Image>().sprite = image;
            go.GetComponent<animalBatch>().text.text = image.name;
            AudioClip clip = Resources.Load<AudioClip>(path + "Животные/Уровень 1/" + image.name);
            AudioClip ch = Resources.Load<AudioClip>(path + "Животные/Уровень 1/" + image.name + "1");
            AudioClip animalSound = Resources.Load<AudioClip>(path + "Животные/Уровень 2 Звуки/" + image.name );
            Debug.Log(path + "Уровень 2 Звуки/" + image.name);
            go.GetComponent<animalBatch>().clip = animalSound; 
            go.GetComponent<animalBatch>().ch = ch;
            //go.GetComponent<animalBatch>().animalSound = animalSound;
            go.GetComponent<animalBatch>().audioSource = audioSource;

        }



        GetComponent<playProlog>().PlayProlog();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
