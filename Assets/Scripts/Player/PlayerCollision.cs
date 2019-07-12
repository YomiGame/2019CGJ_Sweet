using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerController con;
    PlayerAbility abi;
    // Start is called before the first frame update
    void Start()
    {
        con = GetComponent<PlayerController>();
        abi = GetComponent<PlayerAbility>();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        OnMonster(other);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnItem(other);
    }

    /// <summary>
    /// 碰撞到怪物
    /// </summary>
    /// <param name="other"></param>
    private void OnMonster(Collision2D other)
    {
        if (other.transform.tag == "Triangle" || other.transform.tag == "Square" || other.transform.tag == "Heart")
        {
            EventManager.Instance.DispatchEvent("OnHit", null);
        }
    }
    /// <summary>
    /// 碰撞到道具
    /// </summary>
    private void OnItem(Collider2D other)
    {
        Debug.Log("发生碰撞："+ other.gameObject.name);
        int itemNo=0;
        if (other.transform.tag == "Item" && abi.ItemList.Count<=4)
        {
            Item item=other.GetComponent<Item>();
            switch (abi.playerState)
            {
                case PlayerAbility.PlayerState.Triangle:
                    if(item.type!= PlayerAbility.Item.Triangle) { return; }
                    itemNo = (int)PlayerAbility.Item.Triangle;
                    Effect(other.transform);
                    Destroy(other.gameObject);
                    break;
                case PlayerAbility.PlayerState.HeartShaped:
                    if (item.type != PlayerAbility.Item.HeartShaped) { return; }
                    itemNo = (int)PlayerAbility.Item.HeartShaped;
                    Effect(other.transform);
                    Destroy(other.gameObject);
                    break;
                case PlayerAbility.PlayerState.Square:
                    if (item.type != PlayerAbility.Item.Square) { return; }
                    itemNo = (int)PlayerAbility.Item.Square;
                    Effect(other.transform);
                    Destroy(other.gameObject);
                    break;
                default:
                    return;
            }
            EventManager.Instance.DispatchEvent("OnGainItem", itemNo);
        }
    }

    public void Effect(Transform tager)
    {
        GameObject effect = ObjectPool.Instance.Spawn("YellowStarEffect");
        effect.transform.position = tager.position;
    }
}
