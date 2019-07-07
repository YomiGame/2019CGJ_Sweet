using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 摄像机控制类
/// 请挂载到摄像机上
/// </summary>
public class CameraController : MonoBehaviour {
    public Transform target;        //跟随目标
    public BoxCollider2D[] limit;   //界限
    public int level = 1;
    private Camera myCamera;
	// Use this for initialization
	void Start () {
        myCamera = Camera.main;

    }

    // Update is called once per frame
    Vector3 tempPosition;
	void Update () {
        if (target)
        {
            tempPosition = this.transform.position;
            tempPosition.x = target.transform.position.x;
            tempPosition.y = target.transform.position.y;
            this.transform.position = tempPosition;
            //Follow();
        }
	}

    private void Follow()
    {
        //limit[level].bounds.size.x= myCamera.
        float offsetY = -transform.position.z / Mathf.Pow(3f, 0.5f);
        float offsetX = offsetY * ((Screen.width + 0f) / (Screen.height + 0f));
        Vector3 min = limit[level-1].bounds.min;
        Vector3 max = limit[level-1].bounds.max;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(target.position.x, min.x + offsetX, max.x - offsetX);
        pos.y = Mathf.Clamp(target.position.y, min.y + offsetY, max.y - offsetY);
        transform.position = pos;
    }


}
