using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class numberCounter_lvl2 : MonoBehaviour
{

    public int counter = 0;
    public int targerCount = 3;

    public RectTransform startSpawnPoint;
    public GameObject spawningObject;
    public Transform parent;
    public AudioSource audioSource;

    List<dragNumber> numbers;

    bool lck = false;
    public void PlayIntro()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioSource.clip);
            StartCoroutine(lockEndButton());
            foreach (var n in numbers)
            {
                n.StartCoroutine(n.lockProlog());
            }
        }
    }

    IEnumerator lockEndButton()
    {
        lck = true;
        yield return new WaitForSeconds(2.0f);
        lck = false;
    }

    void Start()
    {
        int type = Random.Range(1, 10);
        string path = Hooks.GetVoicePath();

        AudioClip[] targetClips = Resources.LoadAll<AudioClip>(path + "Цифры/Уровень 2/" + type.ToString());



        numbers = new List<dragNumber>();
        audioSource = GetComponent<AudioSource>();
        int randNumber = Random.Range(1, 10);

        Sprite textureTemp = Resources.Load<Sprite>("lvl2_2_basket/" + randNumber.ToString());
        GetComponent<Image>().sprite = textureTemp;

        foreach(var clip in targetClips)
        {
            if (clip.name.Contains(" " + randNumber.ToString() + " "))
            {
                audioSource.PlayOneShot(clip);
                audioSource.clip = clip;
            }
        }
           
        

        

        targerCount = randNumber;

        int n = 10;

        Vector3 move = new Vector3(100, 0,0);
        for (int i = 0; i < n && i < 5; i++)
        {
            var go = Instantiate(spawningObject, startSpawnPoint);
            go.transform.SetParent(parent);
            go.GetComponent<dragNumber>().LoadWithImage(type);
            startSpawnPoint.localPosition += move ;
            numbers.Add(go.GetComponent<dragNumber>());
        }

        startSpawnPoint.localPosition += new Vector3(0, -80, 0);
        startSpawnPoint.localPosition += new Vector3(-500,0 ,0);
        for (int i = 5; i < n; i++)
        {
            var go = Instantiate(spawningObject, startSpawnPoint);
            go.transform.SetParent(parent);
            go.GetComponent<dragNumber>().LoadWithImage(type);
            startSpawnPoint.localPosition += move;
            numbers.Add(go.GetComponent<dragNumber>());
        }
    }

  
    public void EndLevel()
    {
        
        SaveLoad save = new SaveLoad(levels.numbers);
        if (counter == targerCount )
        {
            foreach (var n in numbers)
            {
                n.lck = true;
            }
            save.AddP(targerCount.ToString());
            audioSource.Stop();
            StartCoroutine(Hooks.GetInstance().ToNewLevel("numbersLevel2", audioSource));
        }
        else
        {
            foreach(var n in numbers)
            {
                n.ToInit();
            }
            save.AddM(targerCount.ToString());
            save.AddM(counter.ToString());
            Hooks.GetInstance().PlayDis(audioSource);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        counter += other.GetComponent<dragNumber>().weight;
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        counter -= other.GetComponent<dragNumber>().weight;
        Debug.Log("letter goes");
    }


}
