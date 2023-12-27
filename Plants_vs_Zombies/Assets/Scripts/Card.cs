using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    //冷却时间
    private float coolTime = 4;
    //时间
    private float timer;
    //冷却图片
    private GameObject progress;
    //bg图片
    private GameObject bg;

    //植物预制体
    private GameObject plantPre;
    //生成的植物
    private GameObject plant;
    //植物需要的阳光数
    private float num;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        progress = transform.Find("progress").gameObject;
        num=100;
        bg = GameObject.Find("bg");
    }

    //冷却加载
    private void updateProgress()
    {
        Image image = progress.GetComponent<Image>();
        float per = Mathf.Clamp(timer/coolTime,0,1);
        if (GameMgr.Instance.GetSunNum()>=num)//有钱
        {
            image.fillAmount = 1 - per;
            if (image.fillAmount == 0)//冷却时间到
            {
                progress.SetActive(false);//不激活
                return;
            }
        }
        else//没钱
        {
            image.fillAmount = 1;
        }
        progress.SetActive(true);//激活
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        updateProgress();
    }

    //将屏幕坐标转化为世界坐标
    public Vector3 ScreenToWorld(Vector3 eventData)
    {
        Vector3 vector = Camera.main.ScreenToWorldPoint(eventData);
        Vector3 finl = new Vector3(vector.x,vector.y,0);
        return finl;
    }

    //通过名字生成植物
    public GameObject CreatePlant(string name)
    {
        string[] str_plant = name.Split("_");
        string path = "Prefabs/" + str_plant[1];
        plantPre = Resources.Load<GameObject>(path);
        return GameObject.Instantiate(plantPre);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("开始拖拽了");
        //如果在冷却期内则不执行该方法
        //Debug.Log(progress.activeSelf);
        if (progress.activeSelf) return; 
        //通过名字生成植物
        plant = CreatePlant(name);
        //将屏幕坐标转化为世界坐标
        plant.transform.position = ScreenToWorld(eventData.position);
        //更新阳光数量
        GameMgr.Instance.ChangeSunNum(-num);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("正在拖拽");
        //让植物跟随鼠标移动
        if (plant!=null)
        {
            plant.transform.position = ScreenToWorld(eventData.position);
            //解决子弹在移动过程中发射的问题（将植物的某属性更改）
            plant.GetComponent<Plant>().isOnGround = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (plant != null)
        {
            //植物的种植
            //获取鼠标接触到的所有物体数组
            Collider2D[] colliders = Physics2D.OverlapPointAll(ScreenToWorld(eventData.position));
            for (int i = 0; i < colliders.Length; i++)
            {
                // Debug.Log(colliders[i].name+" "+ colliders[i].tag+" "+ colliders[i].transform.childCount);
                //判断是不是格子 格子上有没有植物
                //是 有植物
                if (colliders[i].tag == "grad" && colliders[i].transform.childCount == 0)
                {
                    plant.transform.position = colliders[i].transform.position;
                    plant.transform.SetParent(colliders[i].transform);
                    //结束植物拖拽状态
                    plant.GetComponent<Plant>().isOnGround = true;
                    plant = null;
                    //卡片重新进入冷却期
                    timer = 0;
                    progress.SetActive(true);
                    return;
                }
            }
            if (plant!=null)
            {
                //撤销消耗阳光数量
                GameMgr.Instance.ChangeSunNum(num);
                Destroy(plant);
                plant = null;
            }
        }
        return;
    }
}
