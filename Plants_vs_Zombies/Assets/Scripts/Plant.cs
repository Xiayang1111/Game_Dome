using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    //�Ƿ���ֲ
    public bool isOnGround;
    //��Ҫ������
    public float sunNum;
    //ֲ��Ѫ��
    public float flood;
    //����
    public Animator anim;
    //Ԥ����
    public GameObject Pre;

    //���ֲ�ﵱǰѪ��
    public float GetFlood()
    {
        return flood;
    }

    //ֲ��ı�Ѫ��
    public void ChangeFlood(float num)
    {
        if (flood + num < 0)//ֲ������
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
