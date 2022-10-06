using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rigid;

    private void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Fire();
    }

    void Fire()
    {
        rigid.velocity = Vector3.zero;
        rigid.AddForce(Vector3.forward * 50, ForceMode.Impulse);
        StartCoroutine("DestroyBullet");
    }

    IEnumerator DestroyBullet ()
    {
        Debug.Log("불릿삭제 실행");
        yield return new WaitForSeconds(2f);
        Util.Destroy(gameObject);
        Debug.Log("불릿삭제 종료");
    }
}
