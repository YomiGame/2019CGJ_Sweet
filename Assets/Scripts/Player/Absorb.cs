using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D box;
    public bool RayOnOff = false;
    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        EventManager.Instance.AddListener("OnAbsorbState", On);
        EventManager.Instance.AddListener("UnAbsorbState", Off);
    }
    // Update is called once per frame
    void Update()
    {
        Absord();
    }
    void On(string eventName, object data) { box.enabled = true; RayOnOff = true; }
    void Off(string eventName, object data) { box.enabled = false; RayOnOff = false; }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Triangle":
                EventManager.Instance.DispatchEvent("OnGainAbility", 1);
                Effect(other.gameObject);
                break;
            case "Square":
                EventManager.Instance.DispatchEvent("OnGainAbility", 3);
                Effect(other.gameObject);
                break;
            case "Heart":
                EventManager.Instance.DispatchEvent("OnGainAbility", 2);
                Effect(other.gameObject);
                break;
            default:
                break;
        }

    }

    private void Effect(GameObject other)
    {
        GameObject obj = ObjectPool.Instance.Spawn("YellowStarEffect");
        obj.transform.position = other.transform.position;
        other.SetActive(false);
        Destroy(other);
    }

    private void Absord()
    {
        if (!RayOnOff) { return; }
        RaycastHit2D hit;
        hit = Physics2D.Raycast(this.transform.position, Vector3.right, 3,
            (1 << 11) | (1 << 32));
        if (hit.transform == null) { return; }
        Debug.Log(hit.transform.name);
        if (hit.transform.tag == "Triangle" || hit.transform.tag == "Square" || hit.transform.tag == "Heart")
        {
            Vector3 tager = this.transform.position;
            hit.transform.Translate(new Vector3(tager.x * Time.deltaTime * 0.2f, 0));
        }
    }

    
}
