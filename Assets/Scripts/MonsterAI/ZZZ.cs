using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZZ : MonoBehaviour {
    public SquareAi ParentScript;
    private GameObject Bulle;
    private Animator BulleAnim;
	void Start () {
        Bulle = gameObject;
        ParentScript = gameObject.GetComponentInParent<SquareAi>();
        BulleAnim = gameObject.GetComponent<Animator>();
	}
	
	void Update () {
        if (ParentScript.Z_rea == true)
        {
            BulleAnim.SetBool("bulleFire", true);
        } else if (ParentScript.Z_rea == false)
        {
            BulleAnim.SetBool("bulleFire", false);
        }
	}
}
