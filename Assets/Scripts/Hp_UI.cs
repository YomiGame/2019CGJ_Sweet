using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_UI : MonoBehaviour
{
    public GameObject player;
    PlayerController con;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        con = player.GetComponent<PlayerController>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = con.Hp/100;
    }
}
