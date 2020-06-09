using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class stattistitcsNavigator : MonoBehaviour
{

    public void PressLetterStat()
    {
        PlayerPrefs.SetString("level", levels.letters);
        SceneManager.LoadScene("StatDetails");
    }

    public void PressNumberStat()
    {
        PlayerPrefs.SetString("level", levels.numbers);
        SceneManager.LoadScene("StatDetails");
    }

    public void PressColorsStat()
    {
        PlayerPrefs.SetString("level", levels.colors);
        SceneManager.LoadScene("StatDetails");
    }

    public void PressFiguresStat()
    {
        PlayerPrefs.SetString("level", levels.figures);
        SceneManager.LoadScene("StatDetails");
    }

    public void PressAnimalsStat()
    {
        PlayerPrefs.SetString("level", levels.animals);
        SceneManager.LoadScene("StatDetails");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
