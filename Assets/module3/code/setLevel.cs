using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setLevel : MonoBehaviour
{

    public ColorReciever_lvl2[] recievers;
    public DragColor_lvl2[] items;

    private AudioSource audioSource;

    public  void PlayIntroAgain()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioSource.clip);
            foreach (var obj in items)
            {
                obj.StartCoroutine(obj.lockProlog());
            }
        }
    }

    public void PlayIntro()
    {
        string path = Hooks.GetVoicePath();
        audioSource = GetComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>(path + "Цвета/Уровень 2/перетащи.. цветом");
        audioSource.PlayOneShot(clip);
        audioSource.clip = clip; 
    }

    void Start()
    {
        PlayIntro();
        Sprite[] colors = Resources.LoadAll<Sprite>("цвета/цвета");

        var colorList = new List<Sprite>(colors);
        foreach(var reciever in recievers)
        {
            var tSprite = colorList[Random.Range(0, colorList.Count)];
            colorList.Remove(tSprite);
            reciever.GetComponent<Image>().sprite = tSprite;
            reciever.color = tSprite.name;
            reciever.audioSource = audioSource;
        }

        var recList = new List<ColorReciever_lvl2>(recievers);
        for(int i = 0; i < items.Length; i++)
        {
            ColorReciever_lvl2 tRes = recList[Random.Range(0, recList.Count)];
            recList.Remove(tRes);
            Sprite[] colorItems = Resources.LoadAll<Sprite>("цвета/предметы/" + tRes.color);

            items[i].GetComponent<Image>().sprite = colorItems[Random.Range(0, colorItems.Length)];
            items[i].color = tRes.color;
        } 

    }
    
}
