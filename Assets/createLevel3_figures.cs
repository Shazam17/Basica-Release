using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createLevel3_figures : MonoBehaviour
{

    public GameObject[] forms;

    void Start()
    {
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>("формы/");
        List<Sprite> sprites = new List<Sprite>(loadedSprites);
        var aS = GetComponent<AudioSource>();
        for (int i = 0; i < 4;i++)
        {
            Sprite selected = sprites[Random.Range(0, sprites.Count)];
            sprites.Remove(selected);
            forms[i].GetComponent<Image>().sprite = selected;
            forms[i].GetComponent<container_figure_lvl3>().type = selected.name;
            forms[i].GetComponent<container_figure_lvl3>().audioSource = aS;

        }


        GameObject[] found = GameObject.FindGameObjectsWithTag("Respawn");
        foreach(var obj in found)
        {
            obj.GetComponent<dragFigure3>().audioSource = GetComponent<AudioSource>();
        }
    }
}
