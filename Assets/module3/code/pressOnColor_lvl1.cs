using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class pressOnColor_lvl1 : MonoBehaviour, IPointerClickHandler
{
    public string color;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        string path = Hooks.GetVoicePath();
        AudioClip clip = Resources.Load<AudioClip>(path + "Цвета/Уровень 1/" + color);
        audioSource.PlayOneShot(clip); 
    }
}
