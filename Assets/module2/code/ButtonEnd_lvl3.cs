using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEnd_lvl3 : MonoBehaviour
{

    public Scalers_lvl3 scalers;
    public AudioSource audioSource;
    public ScalableItem_lvl3[] items;

    public void End()
    {
        SaveLoad save = new SaveLoad(levels.numbers);
        if (scalers.scale())
        {
            GameObject[] found = GameObject.FindGameObjectsWithTag("Respawn");
            foreach(var obj in found)
            {
               StartCoroutine( obj.GetComponent<ScalableItem_lvl3>().waitForAudio());
            }
            save.AddP(scalers.left.loadedWeight.ToString());
            StartCoroutine(Hooks.GetInstance().ToNewLevel("numbersLevel3", audioSource));
        }
        //else
        //{
        //    foreach(var item in items)
        //    {
        //        item.ToInit();
        //    }
        //    save.AddM(scalers.left.loadedWeight.ToString());
        //    Hooks.GetInstance().PlayDis(audioSource);
        //}
    }
    
}
