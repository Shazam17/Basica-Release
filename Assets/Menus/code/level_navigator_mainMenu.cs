using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level_navigator_mainMenu : MonoBehaviour
{
    public void OpenLetterSubMenu()
    {
        SceneManager.LoadScene("subMenuTemplate");
        PlayerPrefs.SetString("SubMenuType", "letterSubMenu");
    }

    public void OpenNumbersSubMenu()
    {
        SceneManager.LoadScene("subMenuTemplate");
        PlayerPrefs.SetString("SubMenuType", "numbersSubMenu");
    }

    public void OpenColorsSubMenu()
    {
        SceneManager.LoadScene("subMenuTemplate");
        PlayerPrefs.SetString("SubMenuType", "colorsSubMenu");
    }

    public void OpenFiguresSubMenu()
    {
        SceneManager.LoadScene("subMenuTemplate");
        PlayerPrefs.SetString("SubMenuType", "figuresSubMenu");
    }

    public void OpenAnimalsSubMenu()
    {
        SceneManager.LoadScene("subMenuTemplate");
        PlayerPrefs.SetString("SubMenuType", "animalsSubMenu");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("settings");
    }

    public void OpenStatistics()
    {
        SceneManager.LoadScene("statistics");
    }

}
