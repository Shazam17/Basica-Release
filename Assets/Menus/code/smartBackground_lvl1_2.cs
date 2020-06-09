using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smartBackground_lvl1_2 : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        int lvl = PlayerPrefs.GetInt("lvl1_2_back");
        if (lvl == 0)
        {
            PlayerPrefs.SetInt("lvl1_2_back", 1);
            lvl = 1;
        }else if(lvl == 4)
        {
            PlayerPrefs.GetInt("lvl1_2_back", 1);
        }
        else
        {
            PlayerPrefs.SetInt("lvl1_2_back", lvl + 1);

        }

        Texture2D txt = (Texture2D)Resources.Load("lvl1_2_back/" + lvl.ToString());
        Rect rect = new Rect(0, 0, txt.width, txt.height);
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(txt, rect, new Vector2(0.5f, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
