﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public PlayerState playerState = PlayerState.Nothing;
    /**状态**/
    public enum PlayerState
    {
        Nothing = 0,
        Triangle = 1,
        HeartShaped = 2,
        Square = 3
    }
    public enum Item
    {
        Nothing = 0,
        Triangle = 1,
        HeartShaped = 2,
        Square = 3
    }
    /// <summary>
    /// 能力队列
    /// </summary>
    public Queue<PlayerState> AbilityList = new Queue<PlayerState>();
    /// <summary>
    /// 道具队列
    /// </summary>
    public Queue<Item> ItemList = new Queue<Item>();
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.AddListener("OnAbandon", Abandon);
        EventManager.Instance.AddListener("OnSwitchAbility", SwitchAbility);
        EventManager.Instance.AddListener("OnSwitchItem", SwitchItem);
        EventManager.Instance.AddListener("OnGainAbility", GainAbility);
        EventManager.Instance.AddListener("OnGainItem", GainItem);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 抛弃能力
    /// </summary>
    private void Abandon(string eventName, object data)
    {
        playerState = PlayerState.Nothing;
    }
    /// <summary>
    /// 切换能力
    /// </summary>
    private void SwitchAbility(string eventName, object data)
    {
        PlayerState previous = AbilityList.Dequeue();
        AbilityList.Enqueue(previous);
    }
    /// <summary>
    /// 切换道具
    /// </summary>
    private void SwitchItem(string eventName, object data)
    {
        Item previous = ItemList.Dequeue();
        ItemList.Enqueue(previous);
    }/// <summary>
    /// 获得能力
    /// </summary>
    private void GainAbility(string eventName, object data)
    {
        int ability = (int)data;
        AbilityList.Enqueue((PlayerState)ability);

    }/// <summary>
    /// 获得道具
    /// </summary>
    private void GainItem(string eventName, object data)
    {
        int item = (int)data;
        ItemList.Enqueue((Item)item);
    }

}
