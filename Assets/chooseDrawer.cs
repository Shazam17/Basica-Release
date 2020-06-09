using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseDrawer : MonoBehaviour
{

    public GameObject[] paintings;



    // Start is called before the first frame update
    void Start()
    {
        var chsn = paintings[Random.Range(0, paintings.Length)];
        var go = Instantiate(chsn, this.transform);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
