using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required when using Event data.

public class DragColor_lvl2 : MonoBehaviour, IDragHandler
{
    public string color;
    Vector2 initPos;


    void Start()
    {
        initPos = GetComponent<RectTransform>().anchoredPosition;
        StartCoroutine(lockProlog());
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!lck)
        {
            transform.position = eventData.position;
        }
      
    }

    bool lck = false;
    public void ToInit()
    {
        StartCoroutine(toInitPlace());

    }
    
    public void lockColor()
    {
        lck = true;
    }

    public IEnumerator lockProlog()
    {
        lck = true;
        yield return new WaitForSeconds(4.0f);
        lck = false;
    }
    private IEnumerator toInitPlace()
    {
        lck = true;

        //
        var rect = GetComponent<RectTransform>();
        Vector2 diff = initPos - rect.anchoredPosition;
        while (diff.magnitude > 0.5f)
        {
            diff = initPos - rect.anchoredPosition;
            rect.anchoredPosition += diff.normalized;
            yield return new WaitForSeconds(0.01f);

        }
        rect.anchoredPosition = initPos;
        lck = false;
    }

}
