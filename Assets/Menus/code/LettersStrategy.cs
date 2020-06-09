using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public interface ModuleStrategy
{
    void LoadLevel1();
    void LoadLevel2();
    void LoadLevel3();
}

public class LettersStrategy: ModuleStrategy
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("lettersLevel1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("lettersLevel2");  
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("lettersLevel3");
    }
}

public class NumbersStrategy : ModuleStrategy
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("numbersLevel1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("numbersLevel2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("numbersLevel3");
    }
}

public class ColorsStrategy : ModuleStrategy
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("colorsLevel1Cubes");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("colorsLevel2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("colorsLevel3");
    }
}

public class FiguresStrategy : ModuleStrategy
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("figuresLevel1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("figuresLevel2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("figuresLevel3");
    }
}

public class AnimalsStrategy : ModuleStrategy
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("animalsLevel1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("animalsLevel2");
    }

    public void LoadLevel3()
    {
        throw new System.Exception("no third level in Animals module");
    }
}
