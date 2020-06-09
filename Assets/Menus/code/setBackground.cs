using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setBackground : MonoBehaviour
{
    void Start()
    {
        string path = PlayerPrefs.GetString("backPath");
        if(path != "")
        {
           
            Texture2D txt = (Texture2D)Resources.Load(path);
            Rect rect = new Rect(0, 0, txt.width, txt.height);
            GetComponent<SpriteRenderer>().sprite = Sprite.Create(txt, rect, new Vector2(0.5f, 0.5f));
        }
        
    }
}
