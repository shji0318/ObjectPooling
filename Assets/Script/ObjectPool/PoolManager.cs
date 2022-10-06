using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{   
    public static PoolManager _instance;

    Dictionary<string, Pool> _poolDic = new Dictionary<string, Pool>();
    Transform _poolRoot;

    public Dictionary<string, Pool> PoolDic { get => _poolDic; set => _poolDic = value; }
    public Transform PoolRoot { get => _poolRoot; set => _poolRoot = value; }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            PoolRoot = this.transform;
            DontDestroyOnLoad(this);
        }            
        else
            Destroy(this);
    }

    public GameObject GetOriginal(string name)
    {
        if (!PoolDic.ContainsKey(name))
            return null;
        return PoolDic[name].Original;
    }

    public void CreatePool(GameObject original, int count = 5) // 새로운 Pool 만들기
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = PoolRoot;

        PoolDic.Add(original.name, pool);
    }

    public void Push(Poolable poolable) // 사용한 Pool Object 다시 집어넣기
    {
        if (!PoolDic.ContainsKey(poolable.name))
        {
            GameObject.Destroy(poolable);
            return;
        }

        PoolDic[poolable.name].Push(poolable);
    }

    public Poolable Pop (GameObject original, Transform parent =null) // Pool 꺼내오기
    {
        if (!PoolDic.ContainsKey(original.name))
            CreatePool(original);

        return PoolDic[original.name].Pop(parent);
    }
}
