using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required when using Event data.


public class ScalableItem_lvl3 : MonoBehaviour, IDragHandler
{

    public int weight = 1;
    private Vector2 initPlace;

    bool lck = false;
    void Start()
    {
        initPlace = GetComponent<RectTransform>().anchoredPosition;
        StartCoroutine(waitForAudio());
    }
    public IEnumerator waitForAudio()
    {
        lck = true;
        yield return new WaitForSeconds(4.0f);
        lck = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!lck)
        {
            transform.position = eventData.position;

        }
    }
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
        lck = false;
    }




}
