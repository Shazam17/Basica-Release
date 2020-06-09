using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class dragNumber : MonoBehaviour, IDragHandler
{

    public int weight = 1;
    private Vector2 initPlace;

    void Start()
    {
        initPlace = GetComponent<RectTransform>().anchoredPosition;
        StartCoroutine(lockProlog());
    }

    bool prlg = false;
    public IEnumerator lockProlog()
    {
        prlg = true;
        lck = true;
        yield return new WaitForSeconds(5.0f);
        lck = false;
        prlg = false;

    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!lck)
        {
            transform.position = eventData.position;

        }
    }

    public void LoadWithImage(int number)
    {
        Sprite spr = Resources.Load<Sprite>("цифры_картинки/Уровень 2/" + number.ToString());

        GetComponent<Image>().sprite = spr;
    }

    public bool lck = false;
    public void ToInit()
    {
        StartCoroutine(toInitPlace());
    }

    private IEnumerator toInitPlace()
    {
        lck = true;

        //
        var rect = GetComponent<RectTransform>();
        Vector2 diff = initPlace - rect.anchoredPosition;
        while (diff.magnitude > 0.5f)
        {
            diff = initPlace - rect.anchoredPosition;
            rect.anchoredPosition += diff.normalized;
            yield return new WaitForSeconds(0.01f);

        }
        rect.anchoredPosition = initPlace;
        if (!prlg)
        {
            lck = false;
        }
       
    }
}
