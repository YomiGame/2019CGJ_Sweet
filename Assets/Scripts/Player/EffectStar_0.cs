using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectStar_0 : MonoBehaviour
{
    private SpriteRenderer renderer2d;
    public float h;
    public float s;
    public float v;
    // Start is called before the first frame update
    void Start()
    {
        renderer2d = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color.RGBToHSV(renderer2d.color, out h, out s, out v);
        h += 0.01f;
        renderer2d.color = Color.HSVToRGB(h,s,v);
    }
}
