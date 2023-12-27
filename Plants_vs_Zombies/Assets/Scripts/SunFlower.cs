using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant
{
    //����ʱ��
    private float lightTime;

    //��������
    public void CreateSun()
    {
        GameObject newSun = GameObject.Instantiate(Pre);
        Vector3 dir = new Vector3(Random.Range(-50,50),Random.Range(-50,50), 0);
        newSun.transform.position =transform.position+dir;
        newSun.transform.parent =transform;
        
    }

    //�Ź�����¼�
    public void FinalyFunction()
    {
        CreateSun();
        lightTime = 0;
        anim.SetBool("isLight",false);
    }

    // Start is called before the first frame update
    public override void Start()
    {
        isOnGround = false;
        sunNum = 150;
        anim = GetComponent<Animator>();
        Pre = Resources.Load<GameObject>("Prefabs/Sun");
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnGround) {
            lightTime += Time.deltaTime;
            if (lightTime >= 4)
            {
                //���� �����ı�
                anim.SetBool("isLight", true);
            }
        }
    }
}
