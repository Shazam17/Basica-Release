using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endLevel_lvl3 : MonoBehaviour
{

    public dragFigure3[] figures;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPressButton()
    {
        int count = 0;
        foreach(var figure in figures)
        {
            if (figure.active)
            {
                count++;
            }
        }

        if (count == 4)
        {
            audioSource.Stop();
            StartCoroutine(Hooks.GetInstance().ToNewLevel("figuresLevel3", audioSource));
        }
    
    }
}
