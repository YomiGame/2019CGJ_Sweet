using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartStatic : MonoBehaviour {
    private Transform HeartTransform;
    private Vector2 BullBeginPosition;//子弹发射位置
    private Vector2 BullEndPosition;//子弹结束位置
    public GameObject Bullect;//子弹，需要在Resources/Prefabs创建一个名为Bullectd的子弹物体
    private Transform player;
    private bool LeftRight;//左真右假
    private float BeginMoveOffest;//开始偏移量
    private float EndMoveOffest;//结束偏移量
    void Start () {
        HeartTransform = transform;
        player = GameObject.Find("player").transform;
        GameObject ReBullect = Resources.Load("Prefabs/bullect", typeof(GameObject)) as GameObject;
        Bullect = Instantiate(ReBullect, new Vector2(-100, -100), Quaternion.identity) as GameObject;
    }
    /// <summary>
    /// 判断左右玩家在心形怪左还是右边
    /// </summary>
    private void PlayerLeftRight()
    {
        if (player.position.x < HeartTransform.position.x)
        {
            LeftRight = false;
        }
        else
        {
            LeftRight = true;
        }
    }
    /// <summary>
    /// 循环发射子弹
    /// </summary>
    private void AiFire()
    {
        BullBeginPosition = new Vector2(HeartTransform.position.x + BeginMoveOffest, HeartTransform.position.y);
        BullEndPosition = new Vector2(HeartTransform.position.x + EndMoveOffest, HeartTransform.position.y);

        if (LeftRight == false)
        {
            BeginMoveOffest = -0.1f;
            EndMoveOffest = -6f;
            if (Bullect.transform.position.x <= BullEndPosition.x - 1)
            {
                Bullect.transform.position = BullBeginPosition;

            }
            else if (Bullect.transform.position.x > BullEndPosition.x)
            {
                Bullect.transform.Translate(Vector2.left * Time.deltaTime * 0.5f);
            }
        }
        else
        {
            BeginMoveOffest = 0.1f;
            EndMoveOffest = 6f;
            if (Bullect.transform.position.x >= BullEndPosition.x-1)
            {
                Bullect.transform.position = BullBeginPosition;
                
            }
            else if (Bullect.transform.position.x < BullEndPosition.x)
            {
                Bullect.transform.Translate(Vector2.right*Time.deltaTime*0.5f); 
            }
            
        }
    }
    //transform.position=Vector3.MoveTowards(start.position,end.position,speed*Time.deltaTime);
    void Update () {
        AiFire();
        PlayerLeftRight();
        Debug.Log(BullBeginPosition);
        Debug.Log(BullEndPosition);
    }
}
