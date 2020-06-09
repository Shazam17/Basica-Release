using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class printStat : MonoBehaviour
{

    Text text;
    public string type;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        SaveLoad[] saves = new SaveLoad[5];

        saves[0] = new SaveLoad(levels.letters);
        saves[1] = new SaveLoad(levels.numbers);
        saves[2] = new SaveLoad(levels.colors);
        saves[3] = new SaveLoad(levels.figures);
        saves[4] = new SaveLoad(levels.animals);

        

        foreach(var save in saves)
        {
            text.text += $"{save.module} \n";
            foreach (var letter in save.letters)
            {
                text.text += $"item| name: {letter.Key} value: {letter.Value.toNumber().ToString()} \n";
            }
        }
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
