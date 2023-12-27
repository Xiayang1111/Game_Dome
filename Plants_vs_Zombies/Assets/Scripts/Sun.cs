using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    //�Ƿ�������е����
    private bool isFall;
    //����Ŀ���
    private float target;
    //�����ٶ�
    private float speed=50;

    //��������λ����������
    private GameObject sunNum;

    //��ʼ��̫�����ֵ�λ�ú�Ŀ��λ��
    public void InitSun(float x,float y,float targetY)
    {
        isFall = true;
        transform.position = new Vector3(x,y,0);
        target = targetY;
    }

    //̫���ƶ���Ŀ��λ��
    public void SunMove()
    {
        if (isFall)//�ǵ���̫��
        {
            if (transform.position.y>target) {//δ��Ŀ���
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
            else//��������
            {
                Destroy(this.gameObject,3f);
            }
        }
    }

    //����¼�
    public void OnMouseDown()
    {
        //������������
        GameMgr.Instance.ChangeSunNum(50);
        //��Ļ����ת��Ϊ��������
        Vector3 worldVector3 = Camera.main.ScreenToWorldPoint(sunNum.transform.position);
        worldVector3.z = 0;
        //����Э�̷���������Ŀ��λ��
        StartCoroutine(FlyTo(worldVector3));
    }

    //Э�̷���
    private IEnumerator FlyTo(Vector3 target)
    {
        //��õ�λ����
        //��Ŀ�귽���ƶ�
        while (Vector3.Distance(target,transform.position)>2f)
        {

            Vector3 dir = (target-transform.position).normalized;
            //�ȴ�0.01��(����Ҫ��)
            yield return new WaitForSeconds(0.01f);
            transform.Translate(dir*4);
        }
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        sunNum = GameObject.Find("SunNum");
    }

    // Update is called once per frame
    void Update()
    {
        SunMove();
    }
}
