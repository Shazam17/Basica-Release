using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System;


public static class levels
{

    public static string letters = "letters";
    public static string numbers = "numbers";
    public static string colors = "colors";
    public static string figures = "figures";
    public static string animals = "animals";

}

[System.Serializable]
public class Del
{
    public float up;
    public float down;

    public Del(float i)
    {
        up = down = i;
    }
    public Del()
    {
        up = down = 0;
    }

    public Del(float i, float j)
    {
        up = i;
        down = j;
    }

    public float toNumber()
    {
        return up / down;
    }

    public void plus()
    {
        up++;
        down++;
    }

    public void med()
    {
        down++;
    }

}
 


public class SaveLoad
{

    
    public Dictionary<string, Del> letters = new Dictionary<string, Del>();

    public string module;

    public SaveLoad(string module)
    {
        this.module = module;
        if (File.Exists(Application.persistentDataPath + $"/{module}.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + $"/{module}.gd", FileMode.Open);
            letters = (Dictionary<string, Del>)bf.Deserialize(file);
            file.Close();
        }
    }

    public void AddP(string key)
    {
        if (letters.ContainsKey(key))
        {
            letters[key].plus();
        }
        else
        {
            letters.Add(key, new Del(1));
        }
        checkout();
    }

    public void AddM(string key)
    {
        if (letters.ContainsKey(key))
        {
            letters[key].med();
        }
        else
        {
            letters.Add(key, new Del(0,1));
        }
        checkout();
    }

    public void checkout()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + $"/{module}.gd");
        bf.Serialize(file, letters);
        file.Close();
    }
}

public class statisticsControl : MonoBehaviour
{

    SaveLoad save;

  
    void Start()
    {

        string level = PlayerPrefs.GetString("level");
        save = new SaveLoad(level);


    }

 
    void Update()
    {
        
    }
}
