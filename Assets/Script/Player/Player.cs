using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float _speed;
    [SerializeField]
    GameObject BulletPos;
    bool shotDelay=true;
   
    void Update()
    {
        PlayerMove();
        Shot();
    }

    public void PlayerMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(Vector3.forward * _speed * Time.deltaTime);            
        }
        else if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(Vector3.back * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
    }
    public void Shot()
    {
        if (!shotDelay)
            return;

        if (Input.GetMouseButton(0))
        {
            shotDelay = false;            
            GameObject go = Util.Instantiate("Bullet");
            go.transform.position = BulletPos.transform.position;
            StartCoroutine("Delay");
        }
    }

    IEnumerator Delay ()
    {        
        yield return new WaitForSeconds(0.2f);
        shotDelay = true;
    }

}
