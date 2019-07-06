using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /**变量**/
    public float speed = 20f;             //速率
    public float jumpForce = 600f;            //跳跃力
    public float Hp = 100f;
    public float range = 20f;           //攻击范围
    public AnimatorOverrideController[] animatorList;
    [HideInInspector]
    public bool onGround;           //触地

    /**组件**/
    private Rigidbody2D rig2D;
    private Animator animator;
    /**动作锁**/

    [HideInInspector]
    public bool lockJump = false;

    [HideInInspector]
    public bool lockAttack = false;

    [HideInInspector]
    public bool lockMove = false;
    // Start is called before the first frame update
    void Awake()
    {
        rig2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        EventManager.Instance.AddListener("OnHorizontal", Run);
        EventManager.Instance.AddListener("OnJump", Jump);
        EventManager.Instance.AddListener("OnAttack", Attack);
        EventManager.Instance.AddListener("OnAbsorb", Absorb);
        EventManager.Instance.AddListener("UnAbsorb", UnAbsorb);
        EventManager.Instance.AddListener("OnHPAttenuation", HPAttenuation);
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        OnGround();
        NaturalAttenuation();
        Death();
        Anima();
    }

    private void Run(string eventName, object data)
    {
        if (lockMove)
            return;
        
        float axAxis = (float)data;
        Vector2 v = this.rig2D.velocity;
        v.x = axAxis * speed;
        this.rig2D.velocity = v;

        
    }

    private void Jump(string eventName, object data)
    {
        if (!lockJump && onGround)
            animator.SetTrigger("OnJump"); 
    }
    public void GoJump()
    {
        this.rig2D.AddForce(Vector2.up * jumpForce);
    }
    /// <summary>
    /// 攻击
    /// </summary>
    private void Attack(string eventName, object data)
    {
        if (lockAttack || !onGround) { return; }
        lockAttack = true;
        lockJump = true;
        lockMove = true;
        animator.SetTrigger("Throw");

        //test：
        animator.runtimeAnimatorController = animatorList[1];
    }
    /// <summary>
    /// 吸收
    /// </summary>
    bool fly = false;
    private void Absorb(string eventName, object data)
    {
        EventManager.Instance.DispatchEvent("OnHPAttenuation", Time.deltaTime*2);
        lockJump = true;
        lockMove = true;
        animator.SetBool("OnAbsorb", true);
        rig2D.Sleep();
        if (!fly)
        {
            this.transform.Translate(new Vector3(0, 0.2f, 0));
            fly = true;
        }
        
    }

    private void UnAbsorb(string eventName, object data)
    {
        rig2D.WakeUp();
        lockJump = false;
        lockMove = false;
        animator.SetBool("OnAbsorb", false);
        fly = false;
    }
    /// <summary>
    /// 生命消减
    /// </summary>
    private void HPAttenuation(string eventName, object data)
    {
        Hp -= (float)data;
    }
    /// <summary>
    /// 转向
    /// </summary>
    public void Turn()
    {
        if (this.rig2D.velocity.x > 0.5)
            this.transform.eulerAngles = Vector3.zero;
        else if (this.rig2D.velocity.x < -0.5)
            this.transform.eulerAngles = Vector3.up * 180;
    }
    /// <summary>
    /// 触地判定
    /// </summary>
    private void OnGround()
    {
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(this.transform.position, 0.5f, Vector2.down, 0.1f, LayerMask.GetMask("Support", "Floor"));
        if (hit)
        {
            //Debug.Log(hit.collider.name);
            this.onGround = true;
        }
        else
        {
            this.onGround = false;
        }
            
    }
    /// <summary>
    /// 生命自然衰减
    /// </summary>
    private void NaturalAttenuation()
    {
        Hp -= 1*Time.deltaTime/5;
    }
    /// <summary>
    /// 角色死亡
    /// </summary>
    private void Death()
    {
        if (Hp < 0)
            EventManager.Instance.DispatchEvent("OnPlayerDead",null);
    }

    private void Anima()
    {
        animator.SetBool("OnGround", onGround);
        animator.SetFloat("UpSpeed", rig2D.velocity.y);
        animator.SetBool("Run", rig2D.velocity.x>0.5|| rig2D.velocity.x<-0.5);
    }
}
