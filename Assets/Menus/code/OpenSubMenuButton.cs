using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSubMenuButton : MonoBehaviour
{

    void Start()
    {
        RectTransform tr = GetComponent<RectTransform>();
    
        
    }

    public void BackToSubMenu()
    {
        SceneManager.LoadScene("subMenuTemplate");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }
}
