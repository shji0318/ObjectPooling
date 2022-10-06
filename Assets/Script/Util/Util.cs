using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util 
{
    public static T Load<T> (string path) where T : UnityEngine.Object
    {
        if(typeof(T)==typeof(GameObject))
        {
            // path 부분에서 주소 뒷부분에 object 네임만 추출
            string objName = path;
            int index = path.LastIndexOf('/');
            if (index >= 0)
                objName = path.Substring(index + 1);

            GameObject go = PoolManager._instance.GetOriginal(objName); // PoolManager에 등록되어 있는지 확인
            if (go != null)
                return go as T;            
        }

        return Resources.Load<T>(path);
    }
    public static GameObject Instantiate(string name, Transform parent=null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{name}");

        if (original == null)
            return null;

        if (original.GetComponent<Poolable>()) // Poolable이 등록되어 있으면 Instantiate가 아닌 Pop 과정을 통해 인스턴스화
            return PoolManager._instance.Pop(original, parent).gameObject;

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;

        return go;
    }

    public static void Destroy (GameObject go)
    {
        if (go == null)
            return;

        Poolable poolable = go.GetComponent<Poolable>();

        if(poolable != null) // Poolable이 등록되어 있으면 Destroy가 아닌 Push 과정을 통해 Pool이 생성되어 있다면 삭제하지않고 Pool로 이동하는 방식
        {
            PoolManager._instance.Push(poolable);
            return;
        }

        Object.Destroy(go);
    }
}
