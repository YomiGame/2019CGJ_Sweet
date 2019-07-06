using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 传送门
/// </summary>
public class Teleport : MonoBehaviour
{
    public Transform wayOut;
        // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            Debug.Log(wayOut.position);
            other.gameObject.transform.position=wayOut.position;
        }
    }
}
