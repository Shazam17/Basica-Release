using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Random = UnityEngine.Random;

public class paintPro : MonoBehaviour, IPointerClickHandler
{
    public Text text;
    Texture2D texture;
    public Texture2D Tload;
    private Texture2D t;

    Thread sub;
    float width;
    float height;
    Color col;

    int[ , ] imageMap;

    public void PlayIntro()
    {
        string path = Hooks.GetVoicePath();
        AudioClip clip = Resources.Load<AudioClip>(path + "Цвета/Уровень 3/давай порисуем");
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
    }

    void Start()
    {
        try
        {
            PlayIntro();
            var rect = GetComponent<RectTransform>();


            Texture2D[] txts = Resources.LoadAll<Texture2D>("расскраски/");
            Texture2D selected = txts[Random.Range(0, txts.Length)];
            t = new Texture2D(selected.width, selected.height, TextureFormat.RGB24, 1, false);

            rect.sizeDelta = new Vector2(selected.width * 2, selected.height * 2);

            width = rect.rect.width;
            height = rect.rect.height;
            Graphics.CopyTexture(selected, t);


            GetComponent<Image>().sprite = Sprite.Create(t, new Rect(0.0f, 0.0f, t.width, t.height), new Vector2(0.5f, 0.5f), 100.0f);
            //GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

        }catch(Exception e)
        {
            text.text = e.Message;
        }
    }




    public void OnPointerClick(PointerEventData eventData)
    {
        try
        {
            Vector2 localCursor;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localCursor))
                return;


            var cord = new Vector2((int)localCursor.x + width / 2, (int)localCursor.y + height / 2) / new Vector2(width, height);


            colorPicker picker = GameObject.Find("colorPicker").GetComponent<colorPicker>();
            col = picker.selectedColor;

            var pos = new Vector2Int((int)(cord.x * t.width), (int)(cord.y * t.height));
            if (t.GetPixel(pos.x, pos.y).grayscale > 0.05f)
            {
                FloodFillPro(t, pos.x, pos.y, col);
                t.Apply();
            }
        }catch(Exception e)
        {
            text.text = e.Message;
        }
      

        
       
    }


    int rep = 2;
    private void FloodFill(Vector2Int pt, Color targetColor, Color replacementColor)
    {
        Texture2D tex = new Texture2D(Tload.width, Tload.height, TextureFormat.PVRTC_RGBA4, false);
        Stack<Vector2Int> pixels = new Stack<Vector2Int>();
        var tColor = imageMap[pt.x,pt.y];
        pixels.Push(pt);
       
        while (pixels.Count > 0)
        {
        
            Vector2Int a = pixels.Pop();
            if (a.x < t.width && a.x > 0 &&
                    a.y < t.height && a.y > 0)//make sure we stay within bounds
            {

                if (imageMap[a.x,a.y] == tColor)
                {
                    imageMap[a.x, a.y] = rep;


                    pixels.Push(new Vector2Int(a.x - 1, a.y));
                    pixels.Push(new Vector2Int(a.x + 1, a.y));
                    pixels.Push(new Vector2Int(a.x, a.y - 1));
                    pixels.Push(new Vector2Int(a.x, a.y + 1));
                }
            }
        }
        byte[] l = new byte[t.height * t.width];
        for(int i = 0; i < Tload.width; i++)
        {
            for(int j= 0;j < Tload.height; j++)
            {
                if(imageMap[i , j] == 0)
                {
                    l[i * Tload.height +  j] = (0x00); 
                }
                if (imageMap[i, j] == 1)
                {
                    l[i * Tload.height + j] = (0x0ff);
                }
                if (imageMap[i, j] == 2)
                {
                    l[i * Tload.height + j] = (0xf);
                }
            }
        }
        tex.LoadRawTextureData(l);
        tex.Apply();
        GetComponent<Image>().sprite = Sprite.Create(t, new Rect(0.0f, 0.0f, t.width, t.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    private static int TransformToLeftTop_y(int y, int height)
    {
        return y;
    }

    /// <summary>
    /// Transforms a point in the texture plane so that 0,0 points at left-top corner.</summary>
    private static int TransformToLeftTop_y(float y, int height)
    {
        return height - (int)y;
    }
    public void FloodFillPro(Texture2D texture, int startX, int startY, Color newColor)
    {
        Point start = new Point(startX, TransformToLeftTop_y(startY, texture.height));

        Flat2DArray copyBmp = new Flat2DArray(texture.height, texture.width, texture.GetPixels());

        Color originalColor = texture.GetPixel(start.X, start.Y);
        int width = texture.width;
        int height = texture.height;


        if (originalColor == newColor)
        {
            return;
        }

        copyBmp[start.X, start.Y] = newColor;

        Queue<Point> openNodes = new Queue<Point>();
        openNodes.Enqueue(start);

        int i = 0;

        // TODO: remove this
        // emergency switch so it doesn't hang if something goes wrong
        int emergency = width * height;

        while (openNodes.Count > 0)
        {
            i++;

            if (i > emergency)
            {
                return;
            }

            Point current = openNodes.Dequeue();
            int x = current.X;
            int y = current.Y;

            if (x > 0)
            {
                if (copyBmp[x - 1, y] == originalColor)
                {
                    copyBmp[x - 1, y] = newColor;
                    openNodes.Enqueue(new Point(x - 1, y));
                }
            }
            if (x < width - 1)
            {
                if (copyBmp[x + 1, y] == originalColor)
                {
                    copyBmp[x + 1, y] = newColor;
                    openNodes.Enqueue(new Point(x + 1, y));
                }
            }
            if (y > 0)
            {
                if (copyBmp[x, y - 1] == originalColor)
                {
                    copyBmp[x, y - 1] = newColor;
                    openNodes.Enqueue(new Point(x, y - 1));
                }
            }
            if (y < height - 1)
            {
                if (copyBmp[x, y + 1] == originalColor)
                {
                    copyBmp[x, y + 1] = newColor;
                    openNodes.Enqueue(new Point(x, y + 1));
                }
            }
        }

        texture.SetPixels(copyBmp.data);
    }

    // Could be its own file
    private class Flat2DArray
    {
        public Color[] data;
        private readonly int height;
        private readonly int width;

        public Flat2DArray(int height, int width, Color[] data)
        {
            this.height = height;
            this.width = width;

            this.data = data;
        }

        public Color this[int x, int y]
        {
            get
            {
                return data[x + y * width];
            }
            set
            {
                data[x + y * width] = value;
            }
        }
    }

    private struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

}
