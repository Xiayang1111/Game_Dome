using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    //是否是天空中掉落的
    private bool isFall;
    //掉落目标点
    private float target;
    //掉落速度
    private float speed=50;

    //点击后飞向位置所在物体
    private GameObject sunNum;

    //初始化太阳出现的位置和目标位置
    public void InitSun(float x,float y,float targetY)
    {
        isFall = true;
        transform.position = new Vector3(x,y,0);
        target = targetY;
    }

    //太阳移动到目标位置
    public void SunMove()
    {
        if (isFall)//是掉落太阳
        {
            if (transform.position.y>target) {//未到目标点
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
            else//到后销毁
            {
                Destroy(this.gameObject,3f);
            }
        }
    }

    //点击事件
    public void OnMouseDown()
    {
        //更新阳光数量
        GameMgr.Instance.ChangeSunNum(50);
        //屏幕坐标转换为世界坐标
        Vector3 worldVector3 = Camera.main.ScreenToWorldPoint(sunNum.transform.position);
        worldVector3.z = 0;
        //进行协程方法，飞向目标位置
        StartCoroutine(FlyTo(worldVector3));
    }

    //协程方法
    private IEnumerator FlyTo(Vector3 target)
    {
        //获得单位向量
        //向目标方向移动
        while (Vector3.Distance(target,transform.position)>2f)
        {

            Vector3 dir = (target-transform.position).normalized;
            //等待0.01秒(必须要有)
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
