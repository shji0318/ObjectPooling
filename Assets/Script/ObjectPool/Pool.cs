using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool 
{
    GameObject _original;
    Transform _root;
    Stack<Poolable> _stack = new Stack<Poolable>();
    public GameObject Original { get => _original; set => _original = value; }
    public Transform Root { get => _root; set => _root = value; }
    public Stack<Poolable> Stack { get => _stack; }

    public void Init(GameObject go , int count =5)
    {
        Original = go;
        Root = new GameObject().transform;
        Root.name = $"{go.name}_Root";        

        for(int i = 0; i <count; i++)
        {
            Push(Create());
        }

    }

    public Poolable Create()
    {
        GameObject go = Object.Instantiate<GameObject>(Original);
        go.name = Original.name;
        return go.GetComponent<Poolable>();
    }

    public void Push (Poolable poolable)
    {
        if (poolable == null)
            return;

        poolable.transform.parent = Root;
        poolable.gameObject.SetActive(false);

        Stack.Push(poolable);
    }

    public Poolable Pop (Transform parent=null)
    {
        Poolable poolable;
        if (Stack.Count > 0)
            poolable = Stack.Pop();
        else
            poolable = Create();

        poolable.gameObject.SetActive(true);
        if (parent == null)
            poolable.transform.parent = GameObject.FindObjectOfType<Camera>().transform;
        poolable.transform.parent = parent;

        return poolable;
    }
}
