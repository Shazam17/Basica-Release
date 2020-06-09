using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dragFigure : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public string type;
    public bool enbld = true;
    public bool isActive = false;
    private Vector2 initPlace;


    public AudioSource audioSource;
    public bool stopped = false;
    public bool outSide = true;

   
    void Start()
    {

        //audioSource = GetComponent<AudioSource>();
        initPlace = GetComponent<RectTransform>().anchoredPosition;
        StartCoroutine(waitForAudio());
    }


    public bool isAudio = false;
    public IEnumerator waitForAudio()
    {
        isAudio = true;
        lck = true;
        yield return new WaitForSeconds(4.0f);
        lck = false;
        isAudio = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        if (enbld && !lck)
        {
            stopped = false;
            transform.position = eventData.position;
        }
    }

    public bool active = false;
    public void OffDrag(Vector2 pos)
    {
        GetComponent<RectTransform>().localPosition = pos;
        enbld = false;
        active = true;
    }

    public void StopAct()
    {
        StartCoroutine(StopActivity());
    }

    public void GoBack()
    {
        StartCoroutine(toInitPlace());
    }

    public bool lck = false;
    private IEnumerator toInitPlace()
    {
        lck = true;

        //
        var rect = GetComponent<RectTransform>();
        Vector2 diff = initPlace - rect.anchoredPosition;
        while (diff.magnitude > 1.5f)
        {
            diff = initPlace - rect.anchoredPosition;
            rect.anchoredPosition += diff.normalized;
            yield return new WaitForSeconds(0.01f);

        }
        rect.anchoredPosition = initPlace;
        if (!isAudio)
        {
            lck = false;
        }
        stopped = false;
        outSide = true;
    }


    IEnumerator StopActivity()
    {
        isActive = true;
        yield return new WaitForSeconds(1.0f);
        isActive = false;

    }

    public string tType;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
        if (enbld)
        {
            if (!outSide)
            {
                SaveLoad save = new SaveLoad(levels.figures);
               
                save.AddM(type);
                save.AddM(tType);
                if (audioSource != null)
                {
                    Hooks.GetInstance().PlayDis(audioSource);
                }
                GoBack();
            }

            stopped = true;
            
        }
       

    }
}
