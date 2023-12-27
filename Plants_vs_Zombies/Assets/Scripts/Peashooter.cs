using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : Plant
{
    //��������
    public GameObject bulletPre;
    //�����ӵ��ļ��ʱ��
    private float timer;

    //�����ӵ�
    public void CreateBullet()
    {
        GameObject newBullet = GameObject.Instantiate(bulletPre);
        newBullet.transform.parent = transform;
        newBullet.transform.position = transform.position;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        timer = 0;
    }

    //ֲ�﹥���������ӵ���ײ��

    // Update is called once per frame
    void Update()
    {
        if (isOnGround)
        {
            timer += Time.deltaTime;
            if (timer >= 3) {
                CreateBullet();
                timer = 0;
            }
        }
    }
}
