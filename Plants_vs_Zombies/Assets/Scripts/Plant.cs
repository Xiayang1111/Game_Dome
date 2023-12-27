using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    //是否被种植
    public bool isOnGround;
    //需要阳光数
    public float sunNum;
    //植物血量
    public float flood;
    //动画
    public Animator anim;
    //预制体
    public GameObject Pre;

    //获得植物当前血量
    public float GetFlood()
    {
        return flood;
    }

    //植物改变血量
    public void ChangeFlood(float num)
    {
        if (flood + num < 0)//植物死亡
        {
            flood = 0;
            if (gameObject != null)
            {
                GameObject.Destroy(gameObject);
                return;
            }
        }
        flood += num;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
