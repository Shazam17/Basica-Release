using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelStatisticScript : MonoBehaviour
{

    public Text levelName;
    public Text stat;


    public void SetStat(string level, string info)
    {
        levelName.text = level;
        stat.text = info;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
