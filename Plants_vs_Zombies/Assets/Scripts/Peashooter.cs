using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : Plant
{
    //发射物体
    public GameObject bulletPre;
    //创造子弹的间断时间
    private float timer;

    //创造子弹
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

    //植物攻击（设置子弹碰撞）

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
