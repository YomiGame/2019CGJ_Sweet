using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 子池
 * */
public class SubPool
{
    List<GameObject> subPool = new List<GameObject>();      //子池列表
    GameObject m_Pref;                                      //资源对象（预制体）
    /**
     * 获取子池名称
     * */
    public string Name
    {
        get
        {
            return m_Pref.name;
        }
    }

    public SubPool(GameObject pref)
    {
        m_Pref = pref;
    }
    /**
     * 生成对象
     * */
    public GameObject Spawn()
    {
        GameObject obj = null;      //池中对象
        foreach (GameObject o in subPool)
        {//判断是否激活：
            if (o.activeSelf == false)
            {
                obj = o;
            }
        }

        if (obj == null)
        {  //第一次和池子中没有的时候
            obj = GameObject.Instantiate(m_Pref);       //创建新对象
            subPool.Add(obj);       //入池
        }

        obj.SetActive(true);        //激活对象

        //通过接口重用：
        IReusable ir = obj.GetComponent<IReusable>();
        if (ir != null)
        {
            ir.Spawn();
        }
        return obj;
    }
    /**
     * 收回对象
     * */
    public void UnSpawn(GameObject obj)
    {
        //需要收回的对象存在于池中：
        if (subPool.Contains(obj))
        {//通过接口重用：
            IReusable ir = obj.GetComponent<IReusable>();
            if (ir != null)
            {
                ir.UnSpawn();
            }
            obj.SetActive(false);
        }
    }
    /**
     * 收回全部
     * */
    public void UnSpawnAllObject()
    {
        foreach (GameObject o in subPool)
        {
            if (o.activeSelf == true)
            {
                UnSpawn(o);
            }
        }
    }

    /**
     * 对象是否在子池中
     * */
    public bool ContainObjectInSubPool(GameObject obj)
    {
        return subPool.Contains(obj);
    }

}
