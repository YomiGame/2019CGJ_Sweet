using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public PlayerAbility.Item type;
    public List<Sprite> material;

    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = this.transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Chartlet();
    }
    /// <summary>
    /// 更换贴图
    /// </summary>
    public void Chartlet()
    {
        switch (type)
        {
            case PlayerAbility.Item.Triangle:
                sprite.sprite = material[0];
                break;
            case PlayerAbility.Item.HeartShaped:
                sprite.sprite = material[1];
                break;
            case PlayerAbility.Item.Square:
                sprite.sprite = material[2];
                break;

        }
    }
}
