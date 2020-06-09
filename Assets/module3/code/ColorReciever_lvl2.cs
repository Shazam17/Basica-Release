using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorReciever_lvl2 : MonoBehaviour
{
    public string color;
    public bool matched;
    public AudioSource audioSource;

    public endLevel_lvl2 end;

    void OnTriggerEnter2D(Collider2D other)
    {
        SaveLoad save = new SaveLoad(levels.colors);
        DragColor_lvl2 drag = other.GetComponent<DragColor_lvl2>();
        if (drag.color.Equals(color))
        {
            save.AddP(color);
            matched = true;
           
        }
        else
        {
            Hooks.GetInstance().PlayDis(audioSource);
            drag.ToInit();
            save.AddM(color);
            save.AddM(drag.color);
        }
        Debug.Log("color in");

        end.OnButtonPress();
    }

    

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<DragColor_lvl2>().color.Equals(color))
        {
            matched = false;
        }
    }
}
