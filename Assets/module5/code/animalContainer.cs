using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class animalContainer : MonoBehaviour
{
    public string type;
    public AudioSource audioSource;

    public bool enbld = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        SaveLoad save = new SaveLoad(levels.animals);
        string otherType = other.GetComponent<animalChild>().type;
        if(string.Compare(type,otherType) == 0 && enbld)
        {
            enbld = false;
            other.GetComponent<animalChild>().lck = true;
            save.AddP(type);
            //StartCoroutine(Hooks.GetInstance().ToNewLevel("animalsLevel2", audioSource));
        }
        else
        {
            save.AddM(type);
            save.AddM(otherType);
            Hooks.GetInstance().PlayDis(audioSource);
            other.GetComponent<animalChild>().toInit();

        }
       
    }

    void OnTriggerExit2D(Collider2D other)
    {
      
        
    }
}
