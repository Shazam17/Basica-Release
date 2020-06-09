using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savePositionSubMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float pos = PlayerPrefs.GetFloat("subMenuPos");
        if(pos != 0.0f)
        {
            Vector2 localPos = GetComponent<RectTransform>().anchoredPosition;
            GetComponent<RectTransform>().anchoredPosition = new Vector2(pos, localPos.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable()
    {
        Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
        Debug.Log(GetComponent<RectTransform>().anchoredPosition);
        PlayerPrefs.SetFloat("subMenuPos", pos.x);
    }
}
