using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 漂浮特效
/// </summary>
public class FloatEffect : MonoBehaviour
{
    public float height;

    private Vector3 pos;
    private float delta;
    // Start is called before the first frame update
    void Start()
    {
        pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Float();
    }

    int coeff=1;
    private void Float()
    {
        Vector3 tr = pos;
        tr.y=Mathf.Lerp(pos.y, pos.y + height,delta);
        this.transform.position = tr;

        delta += Time.deltaTime* coeff;
        if (delta >= 1)
        {
            coeff = -1;
        }
        else if(delta <= 0){
            coeff = 1;
        }
    }
}
