using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsScript : MonoBehaviour
{
    public GameObject back;
    public GameObject voice;


    public void ChooseBack()
    {
        back.SetActive(true);
        voice.SetActive(false);
    }

    public void ChooseVoice()
    {
        back.SetActive(false);
        voice.SetActive(true);
    }
}
