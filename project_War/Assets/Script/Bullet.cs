using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f;//生存时间
    public float moveSpeed = 10f;//移动速度
    float time = 1;
    float acceleration = 0.01f;//加速度
    public int damage=1;//伤害量
    bool isCollider = false;//是否碰撞到目标

    RaycastHit hit;
    public GameObject target;//射击目标

    public GameObject hitEffect;//伤害特效

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletMove();
        Physics.Raycast(transform.position,transform.forward, out hit, 1);
        if (hit.collider&&hit.transform.gameObject.layer==target.layer)
        {
            isCollider = true;
            HaveCollider();
        }
        time += Time.deltaTime;
        if (time >= lifeTime||isCollider==true)
        {
            Destroy(transform.gameObject);
        }
    }

    void HaveCollider()
    {
        if (hit.transform.gameObject.layer == 7)
        {
            TankControl tankControl = hit.transform.GetComponent<TankControl>();
            tankControl.Change_HP(-damage);
        }
        else if (hit.transform.gameObject.layer == 8)
        {
            HumanControl humanControl = hit.transform.GetComponent<HumanControl>();
            humanControl.Change_HP(-damage);
        }
        Instantiate(hitEffect,transform.position,transform.rotation);
    }

    void BulletMove()
    {
        //向前
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        //向下坠落
        transform.Translate(Vector3.up*(0.5f)*(-acceleration)*time*time);
    }

    private void OnDestroy()
    {

    }
}
