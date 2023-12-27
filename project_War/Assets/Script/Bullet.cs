using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f;//����ʱ��
    public float moveSpeed = 10f;//�ƶ��ٶ�
    float time = 1;
    float acceleration = 0.01f;//���ٶ�
    public int damage=1;//�˺���
    bool isCollider = false;//�Ƿ���ײ��Ŀ��

    RaycastHit hit;
    public GameObject target;//���Ŀ��

    public GameObject hitEffect;//�˺���Ч

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
        //��ǰ
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        //����׹��
        transform.Translate(Vector3.up*(0.5f)*(-acceleration)*time*time);
    }

    private void OnDestroy()
    {

    }
}
