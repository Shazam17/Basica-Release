using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class leader : MonoBehaviour
{

    public Sprite maleLead;

    // Start is called before the first frame update
    void Start()
    {
        var v = Hooks.GetVoicePath();
        if (v.Equals("мужской/"))
        {
            GetComponent<Image>().sprite = maleLead;
        }
        StartCoroutine(disaper());
    }


    public IEnumerator disaper()
    {
        yield return new WaitForSeconds(4.0f);

        var rt = GetComponent<RectTransform>();
        while(rt.anchoredPosition.x < 650)
        {
            rt.anchoredPosition += new Vector2(4f, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
