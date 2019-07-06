using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 输入管理
/// </summary>
public class InputManager : SingletonBase<InputManager>,IInitialize
{
    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ListeningDirectionAxis();
        ListeningJump();
        ListeningAbandon();
        ListeningSwitchAbility();
        ListeningSwitchItem();
        ListeningAttack();
    }
    /// <summary>
    /// 监听方向键
    /// </summary>
    private void ListeningDirectionAxis()
    {
        EventManager.Instance.DispatchEvent("OnHorizontal", Input.GetAxisRaw("Horizontal"));
    }
    /// <summary>
    /// 监听跳跃
    /// </summary>
    private void ListeningJump()
    {
        if(Input.GetKeyDown(KeyCode.W))
            EventManager.Instance.DispatchEvent("OnJump",null);
    }
    /// <summary>
    /// 抛弃能力
    /// </summary>
    private void ListeningAbandon()
    {
        if (Input.GetKeyDown(KeyCode.S))
            EventManager.Instance.DispatchEvent("OnAbandon", null);
    }
    /// <summary>
    /// 切换能力
    /// </summary>
    private void ListeningSwitchAbility()
    {
        if (Input.GetKeyDown(KeyCode.E))
            EventManager.Instance.DispatchEvent("OnSwitchAbility", null);
    }
    /// <summary>
    /// 切换道具
    /// </summary>
    private void ListeningSwitchItem()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            EventManager.Instance.DispatchEvent("OnSwitchItem", null);
    }
    /// <summary>
    /// 监听攻击
    /// 长按=吸收
    /// 短按=攻击
    /// </summary>
    public int keyFrameCriticality=15;
    private int keyFrame=0;
    private void ListeningAttack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            keyFrame++;
        }

        if(keyFrame < keyFrameCriticality && Input.GetKeyUp(KeyCode.Space)){
                EventManager.Instance.DispatchEvent("OnAttack", null);
                keyFrame = 0;
        }
        else if(keyFrame > keyFrameCriticality && !Input.GetKeyUp(KeyCode.Space))
        {
            EventManager.Instance.DispatchEvent("OnAbsorb", null);
        }else if(Input.GetKeyUp(KeyCode.Space))
        {
            EventManager.Instance.DispatchEvent("UnAbsorb", null);
            keyFrame = 0;
        }
    }
            
}
