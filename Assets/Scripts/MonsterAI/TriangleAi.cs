using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleAi : MonoBehaviour {
    public GameObject Triangle;//挂载位置的怪物方块
    public Rigidbody2D TriangleRigidbody;//怪物方块的刚体
    private Vector2 LowTouch;
    private Vector2 HighTouch;
    private int RandNumber;//移动方向1左2右边
    private Vector2 StaticVector2;
    public float SqaceSpeed;//移动速度
    private bool StatusOfAttacked;
    public GameObject player;//玩家，没获取下面要获取
    private Animator T_Animator;
    void Start()
    {
        Triangle = this.gameObject;
        TriangleRigidbody = gameObject.GetComponent<Rigidbody2D>();
        LowTouch = new Vector2(Triangle.transform.position.x, Triangle.transform.position.y - 3);//最下
        HighTouch = new Vector2( Triangle.transform.position.x, Triangle.transform.position.y + 3);//最上
        StaticVector2 = this.gameObject.transform.position;//移动起始原点
        StartCoroutine(Timer());
        StatusOfAttacked = true;
        SqaceSpeed = 1.2f;
        player = GameObject.FindWithTag("Player");
        TriangleRigidbody.bodyType = RigidbodyType2D.Kinematic;
        Triangle.tag = "Triangle";
        T_Animator = Triangle.GetComponent<Animator>();

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monster"), LayerMask.NameToLayer("Floor"));
    }
    /// <summary>
    /// 怪物巡逻与超范围停止
    /// </summary>
    private void AIRun()
    {
        if (RandNumber == 1)
        {
            TriangleRigidbody.velocity = new Vector2(TriangleRigidbody.velocity.x, SqaceSpeed);
        }
        else if (RandNumber == 2)
        {
            TriangleRigidbody.velocity = new Vector2(TriangleRigidbody.velocity.x, -SqaceSpeed);
        }
        if (Triangle.transform.position.y >= HighTouch.y)
        {
            RandNumber = 2;
            //Debug.Log("3");
        }
        else if (Triangle.transform.position.y <= LowTouch.y)
        {
            RandNumber = 1;
            //Debug.Log("4");
        }

    }
    /// <summary>
    /// 随机状态
    /// </summary>
    private void RandomStatus()
    {
        float i = Random.value;
        if (i > 0.5f && Triangle.transform.position.y >= LowTouch.y && Triangle.transform.position.y <= HighTouch.y)//向右走
        {
            RandNumber = 1;
        }
        else if (i <= 0.5f && Triangle.transform.position.y <= HighTouch.y && Triangle.transform.position.y >= LowTouch.y)//向左走
        {
            RandNumber = 2;
        }
    }
    /// <summary>
    /// 人接近时怪物的反应
    /// </summary>
    public void AITrianglereaction()
    {
        if ((Triangle.transform.position.x - player.transform.position.x) < 3 && (Triangle.transform.position.x - player.transform.position.x) > -3)
        {
            Debug.Log("1");
            //RandNumber = 3;
            SqaceSpeed = 3.0f;
            AIGoQuickly();
            T_Animator.SetBool("Near", true);
        }
        else
        {
            SqaceSpeed = 1.0f;
            //LowTouch = new Vector2(Triangle.transform.position.x - 3, Triangle.transform.position.y);//左边最远重新定义
            //HighTouch = new Vector2(Triangle.transform.position.x + 3, Triangle.transform.position.y);//右边最远重新定义
            T_Animator.SetBool("Near", false);
        }

    }
    /// <summary>
    /// 方块速度跑
    /// </summary>
    private void AIGoQuickly()//重写一下
    {
        
    }
    /// <summary>
    /// 方块怪被打
    /// </summary>
    private void AIBehurt()
    {

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
        AITrianglereaction();
    }
}

