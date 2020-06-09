using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endLevel_lvl2 : MonoBehaviour
{
    public ColorReciever_lvl2[] recievers;
    public DragColor_lvl2[] colors;
    public AudioSource audioSource;

 
  
    public void OnButtonPress()
    {
        SaveLoad save = new SaveLoad(levels.colors);
        bool flag = true;
        foreach(var elem in recievers)
        {
            if (!elem.matched)
            {
                flag = false;  
            }
        }

        if (flag)
        {
          foreach(var color in colors)
            {
                color.lockColor();
                save.AddP(color.color);
            }
            audioSource.Stop();
            StartCoroutine(Hooks.GetInstance().ToNewLevel("colorsLevel2", audioSource));
        }
     
        
    }

 
}
