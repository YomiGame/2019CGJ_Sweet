using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerController con;
    // Start is called before the first frame update
    void Start()
    {
        con = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Triangle" || other.transform.tag == "Square" || other.transform.tag == "Heart")
        {
            EventManager.Instance.DispatchEvent("OnHit", null);
        }
    }


}
