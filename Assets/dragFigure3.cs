using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dragFigure3 : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public string type;
    public bool enbld = false;
    public bool outSide = true;
    public bool stopped = false;
    public bool lck = false;

    public AudioSource audioSource;
    private Vector2 initPlace;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        initPlace = GetComponent<RectTransform>().anchoredPosition;
        StartCoroutine(waitForAudio());
    }

    public IEnumerator waitForAudio()
    {
        lck = true;
        yield return new WaitForSeconds(4.0f);
        lck = false;
    }

    public void setOutside()
    {
        //StartCoroutine(outS());
        outSide = true;
    }

    IEnumerator outS()
    {
        
        yield return new WaitForSeconds(0.01f);
        outSide = true;
    }

 

    public bool active = false;
    public void OffDrag(Vector2 pos)
    {
        GetComponent<RectTransform>().localPosition = pos;
        enbld = false;
        active = true;
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (enbld && !lck)
        {
            stopped = false;
            transform.position = eventData.position;
        }
    }


    public void GoBack()
    {
        StartCoroutine(toInitPlace());
    }
    private IEnumerator toInitPlace()
    {
        lck = true;

        //
        var rect = GetComponent<RectTransform>();
        Vector2 diff = initPlace - rect.anchoredPosition;
        while (diff.magnitude > 3f)
        {
            diff = initPlace - rect.anchoredPosition;
            rect.anchoredPosition += diff.normalized * Time.deltaTime * 100;
            yield return new WaitForSeconds(0.01f);

        }
        rect.anchoredPosition = initPlace;
        lck = false;
        stopped = false;
        outSide = true;
    }


    public string tType;

    public bool clicked = false;
    // Update is called once per frame
    void Update()
    {
        Debug.Log("click");
        if (enbld && clicked)
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
            clicked = false;
        }
    }

    IEnumerator setClicked()
    {
        clicked = true;
        yield return new WaitForSeconds(0.01f);
        clicked = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(setClicked());
    }
}
