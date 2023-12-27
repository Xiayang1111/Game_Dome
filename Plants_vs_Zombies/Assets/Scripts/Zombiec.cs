using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombiec : MonoBehaviour
{
    //移动速度
    private float speed;
    //吃植物的最大间隔时间
    private float maxEatTime;
    //记录吃植物的时间
    private float eatTimer;
    //造成伤害
    private float damageFlood;
    //僵尸血量
    private float flood;
    //僵尸头掉时的血量
    private float lostHeadFlood;
    //僵尸的头
    private GameObject head;

    //所挂载的动画
    private Animator anim;

    //状态
    private bool isWalk;
    private bool isEat;
    private bool isLostHead;
    private bool isDie;

    // Start is called before the first frame update
    void Start()
    {
        speed = 30f;
        maxEatTime = 2f;
        eatTimer = 0;
        damageFlood = 20f;
        isWalk=true;
        isEat=false;
        isLostHead=false;
        isDie=false;
        flood = 200f;
        lostHeadFlood=100;
        anim = GetComponent<Animator>();
        head = transform.Find("Head").gameObject;
        head.SetActive(false);
    }

    //销毁僵尸
    public void DestroyZombies()
    {
        Debug.Log("僵尸你应该死了");
        anim.enabled = false;
        GameObject.Destroy(gameObject);
    }

    //僵尸血量改变
    public void ChangeFlood(float num)
    {

        if (flood + num < 0)//僵尸死亡
        {
            flood = 0;
            isDie = true;
            //播放僵尸死亡动画
            anim.SetTrigger("Die");   
            return;
        }
        flood += num;

        if (isDie == false&&!isLostHead)
        {
            //僵尸掉头
            if (flood<=lostHeadFlood)
            {
                isLostHead = true;
                head.SetActive(true);
                anim.SetBool("isLostHead",isLostHead);
            }
        }
        
    }

    //僵尸移动
    public void Move()
    {
        transform.Translate(-speed*Time.deltaTime,0,0);
    }

    //僵尸吃植物
    public void Eat(GameObject plant)
    {
        //植物扣血
        plant.GetComponent<Peashooter>().ChangeFlood(-damageFlood);
        if (plant.GetComponent<Peashooter>().GetFlood()==0&&isEat)
        {
            isEat = false;
            isWalk = true;
            anim.SetBool("isEat",isEat);
            anim.SetBool("isWalk",isWalk);
        }
    }

    //碰撞检测
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //碰到植物
        if (collision.gameObject.tag == "plant")
        {
            isWalk = false;
            isEat = true;
        }
        anim.SetBool("isWalk", isWalk);
        anim.SetBool("isEat", isEat);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "plant")
        {
            //隔一段时间吃一口植物
            eatTimer += Time.deltaTime;
            if (eatTimer >= maxEatTime)
            {
                Eat(collision.gameObject);
                eatTimer = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalk) {
            Move();
        }
    }
}
