using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class createLevel_lvl1_3 : MonoBehaviour
{

    public GameObject letterPrefab;
    public GameObject parent;

    private string path;

    public char targetLetter;

    public List<GameObject> letters;
    void CreateLevel()
    {
        letters = new List<GameObject>();
        int lvl = PlayerPrefs.GetInt("lvl1_3_letter");
        if (lvl == 0)
        {
            PlayerPrefs.SetInt("lvl1_3_letter", 1);
            lvl = 1;
        }
        else if (lvl == 32)
        {
            PlayerPrefs.GetInt("lvl1_3_letter", 1);
        }
        else
        {
            PlayerPrefs.SetInt("lvl1_3_letter", lvl + 1);

        }

        var cLEts = new List<char>();
        for (int i = 0; i < 32; i++)
        {
            char lettterChar = (char)(1072 + i);
            if (lettterChar != 'ё' && lettterChar !='ѐ')
            {
                cLEts.Add((char)(1072 + i));
            }
        }

        path = Hooks.GetVoicePath();

        //chooseLetter to find

        var vRand = Random.insideUnitCircle  * new Vector2(300,100);
        vRand.y = Math.Abs(vRand.y);
        GameObject letter = Instantiate(letterPrefab, parent.transform);
        letter.GetComponent<RectTransform>().anchoredPosition = vRand;
        letters.Add(letter);

        char let = cLEts[Random.Range(0, cLEts.Count)];
        cLEts.Remove(let);
        targetLetter = let;
        letter.GetComponent<pressToAudio>().letter = let;

        string del = " ";
        if (path == "мужской/"){
            del = "";
        }

        AudioClip txt = Resources.Load<AudioClip>(path + "lvl1_3_introSounds/найди.." + del  +  "с буквой " + let.ToString());

        Debug.Log(path + "lvl1_3_introSounds/найди.. с буквой " + let.ToString());

        GetComponent<AudioSource>().clip = txt;
        GetComponent<AudioSource>().PlayOneShot(txt);

        Debug.Log("letters/" + let);
        Sprite texture = Resources.Load<Sprite>("letters/" + let.ToString().ToUpper());
        letter.GetComponent<Image>().sprite = texture;
        letter.GetComponent<pressToAudio>().audioSource = GetComponent<AudioSource>();



        Vector2 last = new Vector2(-350, -100);
        for (int i = 0; i < 4; i++)
        {
          
            vRand = last  + new Vector2(100,0) + Random.Range(0.1f,0.5f) * new Vector2(100, 0);
            last = vRand;
            GameObject tempLet = Instantiate(letterPrefab, parent.transform);
            tempLet.GetComponent<RectTransform>().anchoredPosition = vRand;

            char letterCharTemp = cLEts[Random.Range(0, cLEts.Count)];
            cLEts.Remove(letterCharTemp);
            tempLet.GetComponent<pressToAudio>().letter = letterCharTemp;
            tempLet.GetComponent<pressToAudio>().audioSource = GetComponent<AudioSource>();
            Sprite textureTemp = Resources.Load<Sprite>("letters/" + letterCharTemp.ToString().ToUpper());
            letters.Add(tempLet);

            tempLet.GetComponent<Image>().sprite = textureTemp;
        }
        StartCoroutine(lockLettetsCoroutine());
    }

    public void lockLetters()
    {
        foreach(var letter in letters)
        {
            letter.GetComponent<pressToAudio>().lck = true;
        }
    }
    public void PlayTaskAgain()
    {
        AudioSource aS = GetComponent<AudioSource>();
        if (!aS.isPlaying)
        {
            aS.PlayOneShot(aS.clip);
            StartCoroutine(lockLettetsCoroutine());

        }
    }


    IEnumerator lockLettetsCoroutine()
    {
        foreach (var letter in letters)
        {
            letter.GetComponent<pressToAudio>().lck = true;
        }

        yield return new WaitForSeconds(3.0f);
        foreach (var letter in letters)
        {
            letter.GetComponent<pressToAudio>().lck = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

   
}
