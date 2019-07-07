using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Beginmanger : MonoBehaviour {
    public  Transform BG1;
    public Transform BG2;
    public Transform BG3;
    public Transform BG4;
    private float timer;
    public int TimeImage;
    void Start () {
        BG1 = transform.Find("Begin1");
        BG2 = transform.Find("Begin2");
        BG3 = transform.Find("Begin3");
        BG4 = transform.Find("Begin4");
        timer = 0;


    }

    void Update () {

        timer+= Time.time;
        if (timer > 400)
        {
            timer = 0;
            TimeImage++;
        }

        if (TimeImage == 0)
        {
            BG1.transform.position = new Vector2(0, 0);
            BG2.transform.position = new Vector2(-200, -200);
            BG3.transform.position = new Vector2(-200, -200);
            BG4.transform.position = new Vector2(-200, -200);
        }
        if (TimeImage == 1)
        {
            BG1.transform.position = new Vector2(-200, -200);
            BG2.transform.position = new Vector2(0, 0);
            BG3.transform.position = new Vector2(-200, -200);
            BG4.transform.position = new Vector2(-200, -200);
        }
        if (TimeImage == 2)
        {
            BG1.transform.position = new Vector2(-200, -200);
            BG2.transform.position = new Vector2(-200, -200);
            BG3.transform.position = new Vector2(0, 0);
            BG4.transform.position = new Vector2(-200, -200);
        }
        if (TimeImage == 3)
        {
            BG1.transform.position = new Vector2(-200, -200);
            BG2.transform.position = new Vector2(-200, -200);
            BG3.transform.position = new Vector2(-200, -200);
            BG4.transform.position = new Vector2(0, 0);
        } else if(TimeImage == 4)
        {
            TimeImage = 0;
            SceneManager.LoadScene(1);
        }
    }
}
