using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioOnPress : MonoBehaviour,IPointerClickHandler
{
    public string module;
    public string level;
    public string fileName;

    public AudioSource audioSource;
    AudioClip clip;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!audioSource.isPlaying && !lck)
        {
            audioSource.PlayOneShot(clip);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        string path = PlayerPrefs.GetString("voicePath");
        clip = Resources.Load<AudioClip>(path + module + "/" + level + "/" + fileName);
        StartCoroutine(lockProlog());
    }

    public bool lck = false;
    public IEnumerator lockProlog()
    {
        lck = true;
        yield return new WaitForSeconds(3.0f);
        lck = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
