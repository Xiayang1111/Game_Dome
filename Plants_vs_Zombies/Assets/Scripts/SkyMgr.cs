using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMgr : MonoBehaviour
{
    //����һ��������SkyMgr���ڷ���
    public static SkyMgr instance;

    //���䷶Χ
    private float minX =-410f;
    private float minY =-240f;
    private float maxX =250f;
    private float maxY =170f;

    //̫��Ԥ����
    private GameObject sunPre;

    //����̫��
    public void CreateSun()
    {
        GameObject sunObj = GameObject.Instantiate(sunPre);//�Զ��������������
        Sun sun = sunObj.GetComponent<Sun>();//�������л�ȡ����ű�
        float x = Random.Range(minX,maxX);
        float y = Random.Range(minY,maxY);

        sun.InitSun(x,transform.position.y,y);
        sun.transform.parent = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //��ȡ̫��Ԥ����
        sunPre = Resources.Load<GameObject>("Prefabs/Sun");
        //ÿ��һ��ʱ������һ��̫��
        InvokeRepeating("CreateSun",2,5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
