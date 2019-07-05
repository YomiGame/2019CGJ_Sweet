using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 可重用对象
 * */
public abstract class ReusableObject : MonoBehaviour, IReusable

{

    public abstract void Spawn();

    public abstract void UnSpawn();

}
