using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class generateFigures2 : MonoBehaviour
{


    public dragFigure[] figures;

    public containerFigure container;

    

    // Start is called before the first frame update
    void Start()
    {
        Sprite[] arr = Resources.LoadAll<Sprite>("фигуры_картинки/Уровень 2/формы");

        Sprite[] items = Resources.LoadAll<Sprite>("фигуры_картинки/Уровень 2/предметы");

     

        List<Sprite> forms = new List<Sprite>();
        forms.AddRange(arr);

        int r1 = Random.Range(0, forms.Count);
        Sprite targetForm = forms[r1];
        var filtered = new List<Sprite>();
        foreach (var item in items)
        {
            if (item.name.Contains(forms[r1].name))
            {
                filtered.Add(item);
            }
        }
        var chsn = filtered[Random.Range(0, filtered.Count)];

        container.GetComponent<Image>().sprite = chsn;
        container.type = chsn.name;
        container.GetComponent<containerFigure>().audioSource = GetComponent<AudioSource>();

        forms.Remove(forms[r1]);

        for (int i = 0; i < 3; i++)
        {
            int ind = Random.Range(0, forms.Count);
            figures[i].GetComponent<Image>().sprite = forms[ind];
            figures[i].type = forms[ind].name;

            forms.Remove(forms[ind]);
        }

        var targetFigure = Random.Range(0, 3);
        figures[targetFigure].GetComponent<Image>().sprite = targetForm;
        figures[targetFigure].type = chsn.name;
        //for(int i = 0; i < 3; i++)
        //{
        //    int r1 = Random.Range(0, forms.Count);

        //    var filtered = new List<Sprite>();
        //    foreach (var item in items)
        //    {
        //        if (item.name.Contains(forms[r1].name))
        //        {
        //            filtered.Add(item);
        //        }
        //    }
        //    var chsn = filtered[Random.Range(0, filtered.Count)];

        //    containers[i].GetComponent<Image>().sprite = chsn;
        //    containers[i].type = chsn.name;
        //    containers[i].GetComponent<containerFigure>().audioSource = GetComponent<AudioSource>();

        //    forms.Remove(forms[r1]);
        //}

        //var chsnForm = containers[Random.Range(0,containers.Length)];

        //foreach(var form in arr) {

        //    if (chsnForm.type.Contains(form.name))
        //    {

        //        figure.type = chsnForm.type;
        //        figure.GetComponent<Image>().sprite = form;
        //    }
        //}
    }
}
