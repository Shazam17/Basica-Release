using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class containerFigure : MonoBehaviour
{
    public string type;
    public AudioSource audioSource;

    public void OnTriggerEnter2D(Collider2D other)
    {
        SaveLoad save = new SaveLoad(levels.figures);
       
        dragFigure fig = other.GetComponent<dragFigure>();
        if (type == fig.type)
        {
            if(!fig.isActive)
            {
                var obj = GameObject.Find("Canvas").GetComponent<chooseVoice>();
                obj.lck = true;
                obj.LockItems();
                save.AddP(type.Split(' ')[0]);
                audioSource.Stop();
                StartCoroutine(Hooks.GetInstance().ToNewLevel("figuresLevel2", audioSource));
                fig.StopAct();
            }
        }
        else
        {
            if (!fig.isActive && !audioSource.isPlaying)
            {              
                Debug.Log(type.Split(' ')[0]);
                Debug.Log(fig.type.Split(' ')[0]);
                save.AddM(fig.type.Split(' ')[0]);
                save.AddM(type.Split(' ')[0]);

                Hooks.GetInstance().PlayDis(audioSource);
                fig.StopAct();
                fig.GoBack();
            }
        }
    }

}
