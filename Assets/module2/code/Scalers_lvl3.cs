using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Scalers_lvl3 : MonoBehaviour
{
    public ScalarPart_lvl3 left;
    public ScalarPart_lvl3 right;

    public GameObject image;

    public Image selfImage;

    public Sprite lowerImage;
    public Sprite upperImage;
    public Sprite eqImage;

    public GameObject text;
    public AudioSource audioSource;

    public int _delta = 0;

    public void PlayIntro()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioSource.clip);
            GameObject[] found = GameObject.FindGameObjectsWithTag("Respawn");
            foreach(var obj in found){
                obj
                    .GetComponent<ScalableItem_lvl3>()
                    .StartCoroutine(obj.GetComponent<ScalableItem_lvl3>()
                    .waitForAudio());
            }
        }
    }
    public bool Way = false;
    public void SetDelta(int number)
    {
        if (number == 0 )
        {
            selfImage.sprite = eqImage;
        }
        if (number < 0)
        {
            selfImage.sprite = lowerImage;
        }
        if (number > 0)
        {
            selfImage.sprite = upperImage;
        }
        _delta = number;
    }

    public void PlusDelta(int number)
    {
        SetDelta(_delta + number);
    }

    public void Start()
    {
        int randNumber = Random.Range(1, 10);
        SetDelta(randNumber);


        audioSource = GetComponent<AudioSource>();
        string path = Hooks.GetVoicePath();
        
        AudioClip clip = Resources.Load<AudioClip>(path + "Цифры/Уровень 3/конфетки/" + randNumber.ToString());

        int way = Random.Range(0, 2);
        Way = Convert.ToBoolean(way);
        var found = GameObject.FindGameObjectsWithTag("Respawn");
        if(way == 0)
        {
            var count = 1;
            foreach(var obj in found)
            {
                Sprite spr = Resources.Load<Sprite>("Грани для букв и цифр/" + count.ToString());
                obj.GetComponent<Image>().sprite = spr;
                obj.GetComponent<ScalableItem_lvl3>().weight = count;
                count++;
            }
            //here place to modify
            Sprite[] texts = Resources.LoadAll<Sprite>("numbers_images/" + randNumber.ToString());
            image.GetComponent<Image>().sprite = texts[Random.Range(0, texts.Length)];
            text.SetActive(false);
        }
        else
        {
            image.SetActive(false);
            text.GetComponent<Text>().text = randNumber.ToString();
        }
       
        right.SetWeight(randNumber);
        audioSource.PlayOneShot(clip);
        audioSource.clip = clip;
    }

    public bool scale()
    {
        if(left.loadedWeight == right.loadedWeight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
