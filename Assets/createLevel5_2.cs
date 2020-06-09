using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createLevel5_2 : MonoBehaviour
{
    public animalContainer[] targetImages;
    public animalChild[] children;

    public AudioSource audioSource;
    AudioClip clip;


    public void PlayProlog()
    {
       
        if (!audioSource.isPlaying && !lck)
        {
            audioSource.PlayOneShot(clip);
            foreach(var child in children)
            {
                child.StartCoroutine(child.waitForAudio());
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {  
        audioSource = GetComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>(Hooks.GetVoicePath() + "Животные/Уровень 2/перетащи.. родителю");
        audioSource.PlayOneShot(clip);
        this.clip = clip;
        Sprite[] par = Resources.LoadAll<Sprite>("животные_картинки/Уровень 2/нормальные");
        List<Sprite> lPar = new List<Sprite>(par);
        List<animalChild> childrenLIst = new List<animalChild>(children);

        foreach (var targetImage in targetImages)
        {
            Sprite target = lPar[Random.Range(0, lPar.Count)];
            lPar.Remove(target);
            targetImage.type = target.name;
            targetImage.GetComponent<Image>().sprite = target;
            targetImage.audioSource = audioSource;
            var targetChild = childrenLIst[Random.Range(0, childrenLIst.Count)];
            childrenLIst.Remove(targetChild);
            targetChild.SetParent(target.name);
        }
 
    }
    bool lck = false;
    // Update is called once per frame
    void Update()
    {
        bool chcd = false;
        foreach (var target in targetImages)
        {
            if (target.enbld)
            {
                chcd = true;
                break;
            }
        }

        if (!chcd)
        {
            lck = true;
            SaveLoad save = new SaveLoad(levels.animals);
            audioSource.Stop();
            StartCoroutine(Hooks.GetInstance().ToNewLevel("animalsLevel2",audioSource));
            foreach (var target in targetImages)
            {
                save.AddP(target.type);
                target.enbld = true;
            }
        }
    }
}
