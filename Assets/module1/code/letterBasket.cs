using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;






public class letterBasket : MonoBehaviour
{
    public char letter;
    public dragLetter[] letters;

    private AudioSource audioSource;
    private AudioClip intro;

    private string path;
    private SaveLoad save;
    private List<char> cLEts;

    public static char GetRandomLetter()
    {
        int letterIndex = Random.Range(0, 31);

        char lettterChar = (char)(letterIndex + 1072);
      
        if(lettterChar == 'ы' || lettterChar == 'ь' || lettterChar == 'ъ' || lettterChar == 'ѐ') {
            return GetRandomLetter();
        }    
        return lettterChar;
    }

  

    public void PlayIntro()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(intro);
            foreach(var let in letters)
            {
                let.StartCoroutine(let.startLock());
            }
        }  
    }

    public void Start()
    {
        cLEts = new List<char>();
        for(int i = 0; i < 33; i++)
        {
            char lettterChar = (char)(1072 + i);
            if (lettterChar != 'ы' && lettterChar != 'ь' && lettterChar != 'ъ' && lettterChar != 'ѐ')
            {
                cLEts.Add((char)(1072 + i));
            }  
        }
        save = new SaveLoad(levels.letters);
        var letterChar = GetRandomLetter();
        path = PlayerPrefs.GetString(playIntro.voicePath);
  
        AudioClip txt = Resources.Load<AudioClip>(path + "Буквы/Уровень 2/перетащи..на букву " + letterChar.ToString());
        letter = letterChar;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(txt);
        intro = txt;
        cLEts.Remove(letter);
        List<dragLetter> letts = new List<dragLetter>(letters);
        var chsn = letts[Random.Range(0, letts.Count)];
        letts.Remove(chsn);
        Sprite textureTemp = Resources.Load<Sprite>("буквы_картинки/Уровень 2/корзины/" + letterChar.ToString().ToUpper());
        GetComponent<Image>().sprite = textureTemp;
        chsn.setLetter(letter);

        foreach(var l in letts)
        {
            char tLetter = cLEts[Random.Range(0, cLEts.Count)];
            cLEts.Remove(tLetter);
            l.setLetter(tLetter);
        }

    }
    bool endLevel = false;
    void OnTriggerEnter2D(Collider2D other)
    {
       
            if (letter == other.GetComponent<dragLetter>().letter)
            {
                save.AddP(letter.ToString());
                AudioClip clip = OpenGteets.GetGreet();
                audioSource = GetComponent<AudioSource>();
                audioSource.Stop();

                foreach (var obj in letters)
                {
                    StartCoroutine(obj.startLock());
                }
            audioSource.PlayOneShot(clip);
                endLevel = true;

            }
            else
            {
                save.AddM(letter.ToString());
                save.AddM(other.GetComponent<dragLetter>().letter.ToString());
                AudioClip clip = OpenGteets.GetDis();
                audioSource = GetComponent<AudioSource>();

               
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(clip);
                }
                other.GetComponent<dragLetter>().GoBack();

                foreach (var obj in letters)
                {
                    StartCoroutine(obj.startLock());
                }

            }
        
      
    }

    void Update()
    {
        if(!audioSource.isPlaying && endLevel)
        {
            SceneManager.LoadScene("lettersLevel2");

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("letter goes");
    }
   
}
