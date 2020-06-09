using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
using TouchPhase = DigitalRubyShared.TouchPhase;


public class cubeCaster : MonoBehaviour
{
    public bool scroll = true;
    public enum levelType
    {
        letters,
        numbers,
        colors
    }

    private OneTouchRotateGestureRecognizer gest;
    public bool inCenter = false;
    string inCenterCube;
    public GameObject centerObject;


    AudioClip clip;
    public levelType type;
    private AudioSource audioSource;
    public GameObject[] cubes;
    public float sensitivity = 0.1f;
    public float offset = 3.0f;
    public float sizeOfPrefab = 4.0f;
    public float marginLeft = 1.0f;
    public float marginTop = 1.0f;

    public void PlayProlog()
    {
        int playingGlobal = PlayerPrefs.GetInt("playing");
        if (!audioSource.isPlaying && playingGlobal == 0)
        {

            audioSource.PlayOneShot(clip);
            StartCoroutine(waitPress(2.0f));

        }
    }

    public GameObject cubeParent;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        string path = Hooks.GetVoicePath();
        if(type == levelType.letters)
        {
            AudioClip clip = Resources.Load<AudioClip>(path + "Буквы/Уровень 1/поизучаем буквы");
            audioSource.PlayOneShot(clip);
            this.clip = clip;
            
        }else if(type == levelType.numbers)
        {
            AudioClip clip = Resources.Load<AudioClip>(path + "Цифры/Уровень 1 пролог/поизучаем цифры");
            audioSource.PlayOneShot(clip);
            this.clip = clip;
        }
        else if(type == levelType.colors)
        {
            AudioClip clip = Resources.Load<AudioClip>(path + "Цвета/Уровень 1 пролог/поизучаем цвета");
            audioSource.PlayOneShot(clip);
            this.clip = clip;
        }
        StartCoroutine(waitPress(2.0f));

        float top = 3.0f - marginTop;       
        for(int i = 0; i < 7; i++)
        {
            float left = -10.0f + offset;
            for (int j = 0; j < 5; j++)
            {
                if(j + 5 * i < cubes.Length)
                {
                    cubes[j].transform.position = new Vector3(left, top, 0);
                    cubes[j + 5 * i].transform.position = new Vector3(left, top - offset * i, 0);
                    
                }
             

                left += offset + marginLeft;
            }
        }
          foreach(var cube in cubes)
        {
            cube.GetComponent<cubeScript>().audioSource = audioSource;
        }
            top -= sizeOfPrefab + marginTop;
        


     
    }
    
    void Start()
    {
        gest = new OneTouchRotateGestureRecognizer();
        gest.Updated += callback;
        FingersScript.Instance.ResetState(true);
        FingersScript.Instance.AddGesture(gest);
    }

    void callback(GestureRecognizer gesture, ICollection<GestureTouch> touches)
    {
        if (isMoving)
        {
            Debug.Log("isMoving");
            return;
        }
        if(audioSource != null)
        {
            if(audioSource.isPlaying)
            {
                if (isMoving)
                {
                    return;
                }
            }
        }
        else
        {
            if (isMoving)
            {
                return;
            }
        }
       

        if (inCenter && !isMoving)
        {
            foreach(var touch in touches)
            {
                float magnitude = (new Vector2(touch.DeltaX, touch.DeltaY)).magnitude;
                if(magnitude < 0.01f && touch.TouchPhase != TouchPhase.Moved)
                {   
                }
            }
            centerObject.transform.Rotate(new Vector3(gesture.DeltaY, -gesture.DeltaX, 0), Space.World);
        }
        else
        {
            if (cubeParent != null && scroll && !isMoving)
            {
                Vector3 pos = cubeParent.transform.position;
                if (pos.y > -17 && gesture.DeltaY > 0)
                {
                    return;
                }
                if (pos.y < -35 && gesture.DeltaY < 0)
                {
                    return;
                }
                cubeParent.transform.Translate(new Vector3(0, gesture.DeltaY * 0.5f * Time.deltaTime));
            }
    
        }
     
    
    }

    public bool isMoving = false;
    bool fingerOnScreen = false;
    public bool count = false;
    Vector2 lastPos;

    void Update()
    {
        if (isMoving || audioSource.isPlaying)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) || (Input.touches.Length == 1 && Input.touches[0].phase == UnityEngine.TouchPhase.Began))
        {    
            fingerOnScreen = true;     
            if (Input.touches.Length == 0)
            {
                lastPos = Input.mousePosition;
            }
            else
            {
                lastPos = Input.touches[0].position;
            }
        }
        if (inCenter)
        {
            if (Input.GetMouseButtonUp(0) || (Input.touches.Length == 1 && Input.touches[0].phase == UnityEngine.TouchPhase.Ended))
            {
                float magn;
                if (Input.touches.Length == 0)
                {
                    magn = (lastPos - new Vector2(Input.mousePosition.x, Input.mousePosition.y)).magnitude;
                }
                else
                {
                    magn = (lastPos - Input.touches[0].position).magnitude;
                }
                bool flag = true;
                Ray ray;
                if(Input.touches.Length == 0)
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                }
                else
                {
                    ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                }
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider.transform.parent.name == inCenterCube)
                    {
                        flag = false;
                    }
                }
                int playingGlobal = PlayerPrefs.GetInt("playing");
                if (magn < 4 && inCenter && flag && !isMoving && playingGlobal == 0)
                {
                    centerObject.GetComponent<cubeScript>().ToInitial();
                    centerObject.transform.localScale = new Vector3(3,3,3);
                    StartCoroutine(wait());

                }
                fingerOnScreen = false;
                Debug.Log("finger off screen");
            }

            if (Input.GetMouseButtonDown(0) ||(Input.touches.Length == 1 && Input.touches[0].phase == UnityEngine.TouchPhase.Began))
            { // if left button pressed...
                Ray ray;
                if (Input.touches.Length == 0)
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                }
                else
                {
                    ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                }
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.transform.parent.name == inCenterCube)
                    {
                        Debug.Log(hit.collider.name);
                        Debug.Log(hit.collider.transform.parent.name);
                        hit.collider.GetComponent<playAudioCube>().Play();
                        count = false;
                    }
                }
            }
        }
        else
        {
            var go = PlayerPrefs.GetInt("playing");
            if (Input.GetMouseButtonDown(0) || ( Input.touches.Length == 1 &&  Input.touches[0].phase == UnityEngine.TouchPhase.Began) && !isMoving && !inCenter)
            { // if left button pressed...
                Ray ray;
                if(Input.touches.Length == 0)
                {
                    ray  = Camera.main.ScreenPointToRay(Input.mousePosition);
                }
                else
                {
                    ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                }
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (!count && !audioSource.isPlaying && !isMoving  && go == 0)
                    {
                        count = true;
                        hit.collider.GetComponent<playAudioCube>().Play();
                        inCenterCube = hit.collider.transform.parent.name;
                        StartCoroutine(waitPress(1f));
                        centerObject = hit.collider.transform.parent.gameObject;
                        return;
                    }
                    if (count && inCenterCube.Equals(hit.collider.transform.parent.name) && !centerObject.GetComponent<AudioSource>().isPlaying && go == 0)
                    {
                        
                        inCenterCube = hit.collider.transform.parent.name;
                        hit.collider.GetComponentInParent<cubeScript>().ToCenter();
                        centerObject = hit.collider.transform.parent.gameObject;
                        Debug.Log(hit.collider.transform.parent.name);
                        hit.collider.transform.parent.transform.localScale = new Vector3(3, 3, 3);
                        StartCoroutine(wait(3f));
                        count = false;

                    }
                    else 
                    {
                        if(!centerObject.GetComponent<AudioSource>().isPlaying && !isMoving && go == 0)
                        {
                            count = true;
                            hit.collider.GetComponent<playAudioCube>().Play();
                            StartCoroutine(waitPress(1f));
                            inCenterCube = hit.collider.transform.parent.name;
                            return;
                        }                 
                    }                    
                }
            }
        }

    }
    IEnumerator waitPress(float seconds = 2.0f)
    {
        PlayerPrefs.SetInt("playing", 1);
        isMoving = true;
        yield return new WaitForSeconds(seconds);
        isMoving = false;
        PlayerPrefs.SetInt("playing", 0);

    }


    IEnumerator wait(float seconds = 2.0f)
    {
        isMoving = true;
        yield return new WaitForSeconds(seconds);
        isMoving = false;
        inCenter = !inCenter;

    }
}
