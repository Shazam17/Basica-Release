using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class chooseBack : MonoBehaviour, IPointerClickHandler
{
    public string imageName;
    public Image img;

    public Sprite sprPlus;
    public Sprite sprMinus;

    void Start()
    {
        var name = PlayerPrefs.GetString("backPath");
        if (imageName == name)
        {
            img.sprite = sprPlus;
        }
        else
        {
            img.sprite = sprMinus;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject[] found = GameObject.FindGameObjectsWithTag("Respawn");
        foreach(var obj in found)
        {
            obj.GetComponent<chooseBack>().img.sprite = sprMinus;
        }
        PlayerPrefs.SetString("backPath", imageName);
        img.sprite = sprPlus;
    }

}
