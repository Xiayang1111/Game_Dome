using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    //子弹速度
    public float speed;
    //子弹发射最长时间
    private float maxTime;
    private float timer;
    //是否与僵尸碰撞
    private bool isBoom;
    //造成伤害
    private float damageFlood;
    //子弹所属植物
    private GameObject parent;
    //植物名称
    private string parentName;

    //子弹移动
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
            //对僵尸造成伤害
            other.GetComponent<Zombiec>().ChangeFlood(-damageFlood);
            //子弹销毁
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
