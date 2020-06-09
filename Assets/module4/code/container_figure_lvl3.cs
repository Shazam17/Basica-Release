using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class container_figure_lvl3 : MonoBehaviour
{
    public string type;
    
    public AudioSource audioSource;

    public endLevel_lvl3 end;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        dragFigure3 fig = other.GetComponent<dragFigure3>();
        fig.outSide = false;
        //Debug.Log(other.gameObject.name);
        fig.tType = type;
        if (type == fig.type && !fig.lck)
        {
            SaveLoad save = new SaveLoad(levels.figures);
            save.AddP(type);
            fig.OffDrag(GetComponent<RectTransform>().localPosition);
            fig.GetComponent<Transform>().SetParent(gameObject.transform);
            end.OnPressButton();
        }
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        dragFigure3 fig = other.GetComponent<dragFigure3>();
        fig.setOutside();
    }
}
