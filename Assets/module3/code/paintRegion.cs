using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class paintRegion : MonoBehaviour, IPointerClickHandler
{
    Image img;
    public void Start()
    {
        img = GetComponent<Image>();
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        colorPicker picker = GameObject.Find("colorPicker").GetComponent<colorPicker>();
        img.color = picker.selectedColor;
    }

   
}
