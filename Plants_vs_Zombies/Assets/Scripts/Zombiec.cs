using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombiec : MonoBehaviour
{
    //�ƶ��ٶ�
    private float speed;
    //��ֲ��������ʱ��
    private float maxEatTime;
    //��¼��ֲ���ʱ��
    private float eatTimer;
    //����˺�
    private float damageFlood;
    //��ʬѪ��
    private float flood;
    //��ʬͷ��ʱ��Ѫ��
    private float lostHeadFlood;
    //��ʬ��ͷ
    private GameObject head;

    //�����صĶ���
    private Animator anim;

    //״̬
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

    //���ٽ�ʬ
    public void DestroyZombies()
    {
        Debug.Log("��ʬ��Ӧ������");
        anim.enabled = false;
        GameObject.Destroy(gameObject);
    }

    //��ʬѪ���ı�
    public void ChangeFlood(float num)
    {

        if (flood + num < 0)//��ʬ����
        {
            flood = 0;
            isDie = true;
            //���Ž�ʬ��������
            anim.SetTrigger("Die");   
            return;
        }
        flood += num;

        if (isDie == false&&!isLostHead)
        {
            //��ʬ��ͷ
            if (flood<=lostHeadFlood)
            {
                isLostHead = true;
                head.SetActive(true);
                anim.SetBool("isLostHead",isLostHead);
            }
        }
        
    }

    //��ʬ�ƶ�
    public void Move()
    {
        transform.Translate(-speed*Time.deltaTime,0,0);
    }

    //��ʬ��ֲ��
    public void Eat(GameObject plant)
    {
        //ֲ���Ѫ
        plant.GetComponent<Peashooter>().ChangeFlood(-damageFlood);
        if (plant.GetComponent<Peashooter>().GetFlood()==0&&isEat)
        {
            isEat = false;
            isWalk = true;
            anim.SetBool("isEat",isEat);
            anim.SetBool("isWalk",isWalk);
        }
    }

    //��ײ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //����ֲ��
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
            //��һ��ʱ���һ��ֲ��
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
