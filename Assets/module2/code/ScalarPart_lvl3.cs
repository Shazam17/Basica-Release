using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScalarPart_lvl3 : MonoBehaviour
{
    public int loadedWeight = 0;
    private Rigidbody2D rb;

    public ScalarPart_lvl3 otherScale;
    public Scalers_lvl3 scales;

    public ButtonEnd_lvl3 endButton;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Move(float val)
    {
        transform.Translate(new Vector3(0, val, 0));
    }

    public void SetWeight(int weight)
    {
        loadedWeight = weight;
        Move(-weight * 15);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
     
        scales.PlusDelta(-other.GetComponent<ScalableItem_lvl3>().weight);
        loadedWeight += other.GetComponent<ScalableItem_lvl3>().weight;
        Move(-other.GetComponent<ScalableItem_lvl3>().weight * 15);
        otherScale.Move(other.GetComponent<ScalableItem_lvl3>().weight);

        var weight = other.GetComponent<ScalableItem_lvl3>().weight;
        if(!scales.Way && otherScale.loadedWeight != weight)
        {
            SaveLoad save = new SaveLoad(levels.numbers);
            save.AddM(weight.ToString());
            save.AddM(otherScale.loadedWeight.ToString());
            other.GetComponent<ScalableItem_lvl3>().ToInit();
            Hooks.GetInstance().PlayDis(scales.audioSource);
        }


        endButton.End();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        scales.PlusDelta(other.GetComponent<ScalableItem_lvl3>().weight);
        loadedWeight -= other.GetComponent<ScalableItem_lvl3>().weight;
        Move(other.GetComponent<ScalableItem_lvl3>().weight * 15);
        otherScale.Move(-other.GetComponent<ScalableItem_lvl3>().weight);

    }
}
