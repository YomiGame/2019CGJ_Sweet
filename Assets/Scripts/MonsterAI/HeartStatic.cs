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
    private SpriteRenderer HearetRender;
    private Animator HeartAnim;
    void Start () {
        HeartTransform = transform;
        player = GameObject.FindWithTag("Player").transform;
        GameObject ReBullect = Resources.Load("Prefabs/bullect", typeof(GameObject)) as GameObject;//获取子弹预制体
        Bullect = Instantiate(ReBullect, new Vector2(-100, -100), Quaternion.identity) as GameObject;
        HeartTransform.tag = "Heart";
        HearetRender = HeartTransform.GetComponent<SpriteRenderer>();
        HeartAnim = transform.GetComponent<Animator>();//获取状态机
    }
    /// <summary>
    /// 判断左右玩家在心形怪左还是右边
    /// </summary>
    private void PlayerLeftRight()
    {
        if (player.position.x < HeartTransform.position.x)
        {
            LeftRight = false;
            HearetRender.flipX = true;
        }
        else
        {
            LeftRight = true;
            HearetRender.flipX = false;
        }
    }
    /// <summary>
    /// 循环发射子弹
    /// </summary>
    private void AiFire()
    {
        BullBeginPosition = new Vector2(HeartTransform.position.x + BeginMoveOffest, HeartTransform.position.y);
        BullEndPosition = new Vector2(HeartTransform.position.x + EndMoveOffest, HeartTransform.position.y);

        ///判断玩家位置发射子弹
        if (LeftRight == false)
        {
            BeginMoveOffest = -0.1f;
            EndMoveOffest = -6f;
            if (Bullect.transform.position.x <= BullEndPosition.x )
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
            if (Bullect.transform.position.x >= BullEndPosition.x)
            {
                Bullect.transform.position = BullBeginPosition;
                
            }
            else if (Bullect.transform.position.x < BullEndPosition.x)
            {
                Bullect.transform.Translate(Vector2.right*Time.deltaTime*0.5f); 
            }
            
        }


    }

    /// <summary>
    /// 子弹到玩家位置时复位
    /// </summary>
    private void BulletVToPlayerV()
    {
        if ((Bullect.transform.position - player.transform.position).sqrMagnitude < 0.1f)
        {
            Bullect.transform.position = BullBeginPosition;
        }
    }
    /// <summary>
    /// 判断玩家靠近
    /// </summary>
    private void CheckPlayer()
    {
        if ((HeartTransform.position - player.transform.position).sqrMagnitude < 40f)
        {
            AiFire();
            PlayerLeftRight();
            BulletVToPlayerV();
            HeartAnim.SetBool("Heartdo", true);
            
        }
        else if ((HeartTransform.position - player.transform.position).sqrMagnitude >= 40f)
        {
            HeartAnim.SetBool("Heartdo", false);
            Bullect.transform.position = new Vector2(-100, -100);
        }
    }

    void Update () {
        CheckPlayer();
    }
}
