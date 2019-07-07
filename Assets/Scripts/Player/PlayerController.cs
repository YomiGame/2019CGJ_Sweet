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
    public Rigidbody2D rig2D;
    private Animator animator;
    private PlayerAbility ability;
    private SpriteRenderer sp;
    /**动作锁**/

    [HideInInspector]
    public bool lockJump = false;
    [HideInInspector]
    public bool lockAttack = false;
    [HideInInspector]
    public bool lockMove = false;
    [HideInInspector]
    public bool lockAbsorb = false;
    [HideInInspector]
    public bool lockTurn = false;
    // Start is called before the first frame update
    void Awake()
    {
        rig2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ability = GetComponent<PlayerAbility>();
        sp = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        EventManager.Instance.AddListener("OnHorizontal", Run);
        EventManager.Instance.AddListener("OnJump", Jump);
        EventManager.Instance.AddListener("OnAttack", Attack);
        EventManager.Instance.AddListener("OnAbsorb", Absorb);
        EventManager.Instance.AddListener("UnAbsorb", UnAbsorb);
        EventManager.Instance.AddListener("OnHPAttenuation", HPAttenuation);
        EventManager.Instance.AddListener("OnHit", Hit);
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        OnGround();
        NaturalAttenuation();
        Death();
        Anima();
        AnimatorSwitch();
        if (ability.playerState==PlayerAbility.PlayerState.Triangle)
        {
            jumpForce = 750;
        }
        else
        {
            jumpForce = 600;
        }
        
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
        if (lockAttack || !onGround || 
            ability.playerState==PlayerAbility.PlayerState.Nothing ||
            ability.ItemList.Count==0) { return; }
        lockAttack = true;
        lockJump = true;
        lockMove = true;
        StopMove();
        animator.SetTrigger("Throw");
    }
    /// <summary>
    /// 吸收
    /// </summary>
    bool fly = false;
    private void Absorb(string eventName, object data)
    {
        if (ability.playerState != PlayerAbility.PlayerState.Nothing || lockAbsorb || ability.AbilityList.Count>=3) { return; }
        EventManager.Instance.DispatchEvent("OnAbsorbState",null);
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
        EventManager.Instance.DispatchEvent("UnAbsorbState", null);
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
    /// 受到攻击
    /// </summary>
    private void Hit(string eventName, object data)
    {
        EventManager.Instance.DispatchEvent("OnHPAttenuation", 10f);
        EventManager.Instance.RemoveListener("OnHit", Hit);
        StartCoroutine(Filcker());
        Invoke("UnInvincible", 1);
    }
    private void UnInvincible()
    {
        EventManager.Instance.AddListener("OnHit", Hit);
        StopCoroutine(Filcker());
    }

    IEnumerator Filcker()
    {
        Color cc = sp.color;
        for(int i=0; i<3 ;i++){
            cc.a = 0.5f;
            sp.color = cc;
            yield return new WaitForSeconds(0.2f);
            cc.a = 1f;
            sp.color = cc;
            yield return new WaitForSeconds(0.2f);
        }
        
    }
    /// <summary>
    /// 转向
    /// </summary>
    public void Turn()
    {
        if (lockTurn) { return; }
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
        hit = Physics2D.CircleCast(this.transform.position, 0.2f, Vector2.down, 0.1f, LayerMask.GetMask("Support", "Floor"));
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
    public void StopMove()
    {
        Vector3 v = rig2D.velocity;
        v.x = 0;
        rig2D.velocity = v;
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

    private void AnimatorSwitch()
    {
        switch (ability.playerState)
        {
            case PlayerAbility.PlayerState.Nothing:
                animator.runtimeAnimatorController = animatorList[0];
                break;
            case PlayerAbility.PlayerState.Triangle:
                animator.runtimeAnimatorController = animatorList[1];
                break;
            case PlayerAbility.PlayerState.HeartShaped:
                animator.runtimeAnimatorController = animatorList[2];
                break;
            case PlayerAbility.PlayerState.Square:
                animator.runtimeAnimatorController = animatorList[3];
                break;
        }
    }
}
