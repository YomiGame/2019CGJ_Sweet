using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareAi : MonoBehaviour
{
    public GameObject Monster;//挂载位置的怪物方块
    public Rigidbody2D MonsterRigidbody;//怪物方块的刚体
    private Vector2 LeftTouch;
    private Vector2 RightTouch;
    private int RandNumber;//移动方向1左2右边
    private Vector2 StaticVector2;
    private float SqaceSpeed;//移动速度
    private bool StatusOfAttacked;
    private GameObject player;//玩家，没获取下面要获取
    private SpriteRenderer MonsterRender;//怪物的render
    private Animator MonsterAnim;//怪物的动画状态
    private bool z_rea;
    public bool Z_rea
    {
        get { return z_rea; }
        set { Z_rea = z_rea; }
    }
    void Start()
    {
        Monster = this.gameObject;
        MonsterRigidbody = gameObject.GetComponent<Rigidbody2D>();
        LeftTouch = new Vector2(Monster.transform.position.x - 3, Monster.transform.position.y);//左边最远
        RightTouch = new Vector2(Monster.transform.position.x + 3, Monster.transform.position.y);//右边最远
        StaticVector2 = this.gameObject.transform.position;//移动起始原点
        StartCoroutine(Timer());
        StatusOfAttacked = true;
        SqaceSpeed = 1.2f;
        player = GameObject.FindWithTag("Player");
        Monster.tag = "Square";
        MonsterRender = gameObject.GetComponent<SpriteRenderer>();
        MonsterAnim = gameObject.GetComponent<Animator>();
        z_rea = false;
    }
    /// <summary>
    /// 怪物巡逻与超范围停止
    /// </summary>
    private void AIRun()
    {
        if (RandNumber == 1)
        {
            MonsterRigidbody.velocity = new Vector2(1f, 0);
        }
        else if (RandNumber == 2)
        {
            MonsterRigidbody.velocity = new Vector2(-1f, 0);
        }
        if (Monster.transform.position.x >= RightTouch.x)
        {
            RandNumber = 2;
            Debug.Log("3");
        }
        else if (Monster.transform.position.x <= LeftTouch.x)
        {
            RandNumber = 1;
            Debug.Log("4");
        }

    }
    /// <summary>
    /// 随机状态
    /// </summary>
    private void RandomStatus()
    {
        float i = Random.value;
        if (i > 0.5f && Monster.transform.position.x >= LeftTouch.x && Monster.transform.position.x <= RightTouch.x)//向右走
        {
            RandNumber = 1;
        }
        else if (i <= 0.5f && Monster.transform.position.x <= RightTouch.x && Monster.transform.position.x >= LeftTouch.x)//向左走
        {
            RandNumber = 2;
        }
    }
    /// <summary>
    /// 人接近时怪物的反应
    /// 吸收时调用
    /// </summary>
    public void AIreaction()
    {
        if (Monster.transform.position.y-player.transform.position.y < 5f)//修改判定距离
        {
            if ((Monster.transform.position - player.transform.position).sqrMagnitude < 2)
            {
                Debug.Log("1");
                StatusOfAttacked = false;//全部停止
                RandNumber = 3;//全部停止
                SqaceSpeed = 2.0f;//全部停止
                AIGoQuickly();
                MonsterAnim.SetBool("SFire", true);
                z_rea = true;
            }
            else
            {
                MonsterAnim.SetBool("SFire", false);
                StatusOfAttacked = true;
                SqaceSpeed = 1.2f;
                z_rea = false;
                //LeftTouch = new Vector2(Monster.transform.position.x - 3, Monster.transform.position.y);//左边最远重新定义
                //RightTouch = new Vector2(Monster.transform.position.x + 3, Monster.transform.position.y);//右边最远重新定义

            }
        }
    }
    /// <summary>
    /// 方块速度跑
    /// </summary>
    private void AIGoQuickly()
    {
        if (player.transform.position.x - Monster.transform.position.x < 0)
        {
            MonsterRigidbody.velocity = new Vector2(-SqaceSpeed, 0);//人在左边，向左冲
            MonsterRender.flipX = false;
        }
        else
        {
            MonsterRigidbody.velocity = new Vector2(SqaceSpeed, 0);//人在右边，向右冲
            MonsterRender.flipX = true;
        }
    }
    /// <summary>
    /// 方块怪被吸
    /// 吸收时调用赋值
    /// </summary>
    public void AIBeSuck(bool keep)
    {
        if (keep == true)
        {
            StatusOfAttacked = false;//全部停止
            RandNumber = 3;//全部停止
            SqaceSpeed = 2.0f;//全部停止
        }
        else
        {
            StatusOfAttacked = true;
            SqaceSpeed = 1.2f;

        }
    }
    /// <summary>
    /// 计时携程
    /// </summary>
    /// <returns></returns>
    IEnumerator Timer()
    {
        while (true)
        {
            if (StatusOfAttacked == true)
            {
                RandomStatus();
            }
            yield return new WaitForSeconds(1.2f);
        }
    }
    void Update()
    {
        AIRun();
        AIreaction();
    }
}
