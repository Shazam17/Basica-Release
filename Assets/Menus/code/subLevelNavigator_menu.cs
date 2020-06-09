using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class subLevelNavigator_menu : MonoBehaviour
{
    public GameObject content;
    ModuleStrategy subMenuStrategy;

    public VideoPlayer[] player;
    // Start is called before the first frame update
    void Start()
    {
        string subLevelType = PlayerPrefs.GetString("SubMenuType");
        
        switch (subLevelType)
        {
            case "letterSubMenu":
                subMenuStrategy = new LettersStrategy();
                player[0].clip = Resources.Load<VideoClip>("видосы уровней/буквы1");
                player[1].clip = Resources.Load<VideoClip>("видосы уровней/буквы2");
                player[2].clip = Resources.Load<VideoClip>("видосы уровней/буквы3");
                break;
            case "numbersSubMenu":
                subMenuStrategy = new NumbersStrategy();
                player[0].clip = Resources.Load<VideoClip>("видосы уровней/цифры1");
                player[1].clip = Resources.Load<VideoClip>("видосы уровней/цифры2");
                player[2].clip = Resources.Load<VideoClip>("видосы уровней/цифры3");
                break;
            case "colorsSubMenu":
                subMenuStrategy = new ColorsStrategy();
                player[0].clip = Resources.Load<VideoClip>("видосы уровней/цвета1");
                player[1].clip = Resources.Load<VideoClip>("видосы уровней/цвета2");
                player[2].clip = Resources.Load<VideoClip>("видосы уровней/цвета3");
                break;
            case "figuresSubMenu":
                subMenuStrategy = new FiguresStrategy();
                player[0].clip = Resources.Load<VideoClip>("видосы уровней/фигуры1");
                player[1].clip = Resources.Load<VideoClip>("видосы уровней/фигуры2");
                player[2].clip = Resources.Load<VideoClip>("видосы уровней/фигуры3");
                break;
            case "animalsSubMenu":

                Vector2 prevSize = content.GetComponent<RectTransform>().sizeDelta;
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(prevSize.x * 2/3,prevSize.y);
                GameObject.Find("Block 3").SetActive(false);
                subMenuStrategy = new AnimalsStrategy();
                player[0].clip = Resources.Load<VideoClip>("видосы уровней/животные1");
                player[1].clip = Resources.Load<VideoClip>("видосы уровней/животные2");
                break;

            default:
                Debug.Log("undefined behavior");
                break;
        }

      
    }
    
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void OpenLevel1()
    {
        subMenuStrategy.LoadLevel1();
    }
    public void OpenLevel2()
    {
        subMenuStrategy.LoadLevel2();
    }
    public void OpenLevel3()
    {
        subMenuStrategy.LoadLevel3();
    }
}
