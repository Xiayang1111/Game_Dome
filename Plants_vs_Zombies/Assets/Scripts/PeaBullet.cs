using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    //�ӵ��ٶ�
    public float speed;
    //�ӵ������ʱ��
    private float maxTime;
    private float timer;
    //�Ƿ��뽩ʬ��ײ
    private bool isBoom;
    //����˺�
    private float damageFlood;
    //�ӵ�����ֲ��
    private GameObject parent;
    //ֲ������
    private string parentName;

    //�ӵ��ƶ�
    public void bulletMove()
    {
        if(!isBoom&&timer<maxTime)
        {
            transform.Translate(speed*Time.deltaTime,0,0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.tag=="zombiec") {
            //�Խ�ʬ����˺�
            other.GetComponent<Zombiec>().ChangeFlood(-damageFlood);
            //�ӵ�����
            if (gameObject!=null)
            {
                Destroy(gameObject);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        maxTime = 8f;
        timer = 0;
        isBoom = false;
        damageFlood = 50;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        bulletMove();
    }
}
