using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playProlog : MonoBehaviour
{

    public enum LevelType
    {
        FIGURES,
        ANIMALS
    }

    public LevelType type;
    public AudioSource audioSource;
    AudioClip clip;
    // Start is called before the first frame update
    
    public void PlayProlog()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(clip);
            GameObject[] found = GameObject.FindGameObjectsWithTag("Respawn");
            foreach (var obj in found)
            {
                var comp = obj.GetComponent<AudioOnPress>();
                if(comp != null)
                {
                    comp.StartCoroutine(comp.lockProlog());
                }
                var compAnimal = obj.GetComponent<animalBatch>();
                if (compAnimal != null)
                {
                    compAnimal.StartCoroutine(compAnimal.lockProlog());
                }
            }
        }
    }

    void Awake()
    {
   
       
   
        var path = Hooks.GetVoicePath();

        if(type == LevelType.FIGURES)
        {
            AudioClip clip = Resources.Load<AudioClip>(path + "Фигуры/Уровень 1 пролог/поизучаем фигуры");
            this.clip = clip;
            PlayProlog();
        }
        else if(type == LevelType.ANIMALS)
        {
            AudioClip clip = Resources.Load<AudioClip>(path + "Животные/Уровень 1 пролог/поизучаем животных");
            this.clip = clip;
        }


       
    }
}
