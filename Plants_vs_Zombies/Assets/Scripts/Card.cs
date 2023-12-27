using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    //��ȴʱ��
    private float coolTime = 4;
    //ʱ��
    private float timer;
    //��ȴͼƬ
    private GameObject progress;
    //bgͼƬ
    private GameObject bg;

    //ֲ��Ԥ����
    private GameObject plantPre;
    //���ɵ�ֲ��
    private GameObject plant;
    //ֲ����Ҫ��������
    private float num;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        progress = transform.Find("progress").gameObject;
        num=100;
        bg = GameObject.Find("bg");
    }

    //��ȴ����
    private void updateProgress()
    {
        Image image = progress.GetComponent<Image>();
        float per = Mathf.Clamp(timer/coolTime,0,1);
        if (GameMgr.Instance.GetSunNum()>=num)//��Ǯ
        {
            image.fillAmount = 1 - per;
            if (image.fillAmount == 0)//��ȴʱ�䵽
            {
                progress.SetActive(false);//������
                return;
            }
        }
        else//ûǮ
        {
            image.fillAmount = 1;
        }
        progress.SetActive(true);//����
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        updateProgress();
    }

    //����Ļ����ת��Ϊ��������
    public Vector3 ScreenToWorld(Vector3 eventData)
    {
        Vector3 vector = Camera.main.ScreenToWorldPoint(eventData);
        Vector3 finl = new Vector3(vector.x,vector.y,0);
        return finl;
    }

    //ͨ����������ֲ��
    public GameObject CreatePlant(string name)
    {
        string[] str_plant = name.Split("_");
        string path = "Prefabs/" + str_plant[1];
        plantPre = Resources.Load<GameObject>(path);
        return GameObject.Instantiate(plantPre);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("��ʼ��ק��");
        //�������ȴ������ִ�и÷���
        //Debug.Log(progress.activeSelf);
        if (progress.activeSelf) return; 
        //ͨ����������ֲ��
        plant = CreatePlant(name);
        //����Ļ����ת��Ϊ��������
        plant.transform.position = ScreenToWorld(eventData.position);
        //������������
        GameMgr.Instance.ChangeSunNum(-num);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("������ק");
        //��ֲ���������ƶ�
        if (plant!=null)
        {
            plant.transform.position = ScreenToWorld(eventData.position);
            //����ӵ����ƶ������з�������⣨��ֲ���ĳ���Ը��ģ�
            plant.GetComponent<Plant>().isOnGround = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (plant != null)
        {
            //ֲ�����ֲ
            //��ȡ���Ӵ�����������������
            Collider2D[] colliders = Physics2D.OverlapPointAll(ScreenToWorld(eventData.position));
            for (int i = 0; i < colliders.Length; i++)
            {
                // Debug.Log(colliders[i].name+" "+ colliders[i].tag+" "+ colliders[i].transform.childCount);
                //�ж��ǲ��Ǹ��� ��������û��ֲ��
                //�� ��ֲ��
                if (colliders[i].tag == "grad" && colliders[i].transform.childCount == 0)
                {
                    plant.transform.position = colliders[i].transform.position;
                    plant.transform.SetParent(colliders[i].transform);
                    //����ֲ����ק״̬
                    plant.GetComponent<Plant>().isOnGround = true;
                    plant = null;
                    //��Ƭ���½�����ȴ��
                    timer = 0;
                    progress.SetActive(true);
                    return;
                }
            }
            if (plant!=null)
            {
                //����������������
                GameMgr.Instance.ChangeSunNum(num);
                Destroy(plant);
                plant = null;
            }
        }
        return;
    }
}
