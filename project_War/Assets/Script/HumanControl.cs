using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    private CharacterController cc;
    private Animator animator;

    public float jumpSpeed = 20f;
    public float jumpMaxTime = 1f;
    float jumpTime = 0;
    float jumpNow = -10;
    bool isJump = false;
    public int jumpNum = 1;

    public int bloodBar = 10;//即为max_blood
    private int current_blood;
    public Blood blood;

    public bool isAttack = false;
    private Attack attack;

    private GameObject head;
    private Vector3 headRotation;
    private bool isGround = true;
    private int layerMask;

    private int maxBullet = 100;
    private int current_Bullet = 0;
    public Mana mana;

    private RaycastHit hit;
    private Ray ray;

    public bool isdead = false;

    public float hitTime = 1f;
    private float time=0;

    private GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        attack = GetComponent<Attack>();
        animator = GetComponent<Animator>();
        plane = GameObject.Find("StaticSpace").transform.Find("Plane").gameObject;

        current_blood = bloodBar;
        blood.Set_MaxHP(bloodBar);
        mana.Set_MaxBullet(maxBullet);
        head = transform.Find("head").gameObject;
        headRotation = head.transform.localEulerAngles;

        layerMask = LayerMask.GetMask("MagicBall");
        layerMask += LayerMask.GetMask("Tank");
    }

    // Update is called once per frame
    void Update()
    {
        //控制移动
        Moving();
        //控制方向
        Turning();
        //控制攻击
        Attack();
        //检测周围物体
        CheckObject();
        //检测人物位置
        Check_Position();
    }

    private void CheckObject()
    {
        ray = new Ray(transform.position, transform.forward);
        //是否在地上
        isGround = Physics.Raycast(transform.position, Vector3.down, 2.3f, LayerMask.GetMask("StaticSpace"));
        //检测周围相关层物体 获取hit
        Physics.SphereCast(ray,1.1f,out hit,1.1f,layerMask);

        //检测到魔法球
        CheckMagicBall();
        //检测到敌人
        CheckTank();
        
    }

    private void CheckTank()
    {
        time += Time.deltaTime;
        if (time >= hitTime)
        {
            if (hit.collider && hit.transform.gameObject.layer == LayerMask.NameToLayer("Tank"))
            {
                Change_HP(-1);
                time = 0;
            }
        }
    }

    private void CheckMagicBall()
    {
        if (hit.collider && hit.transform.gameObject.layer == LayerMask.NameToLayer("MagicBall"))
        {
            MagicBall magicBall = hit.transform.GetComponent<MagicBall>();
            Change_Bullet(magicBall.addBulletNum);
            Change_HP(magicBall.addBloodNum);
            Destroy(hit.transform.gameObject);
        }
        
    }

    public void Check_Position()
    {
        if (transform.position.y < plane.transform.position.y - 10f)
        {
            Change_HP(-999);
        }
    }

    public void Change_HP(int change)
    {
        //返回修改后的血量，并将值控制在0到最大值之间
        current_blood = Mathf.Clamp(current_blood+change,0,bloodBar);
        //在blood对象中控制修改数值
        blood.Set_CurrentHP(current_blood);
        if (current_blood<=0)
        {

            //Destroy(gameObject);
            isdead = true;
        }
    }

    public void Change_Bullet(int change)
    {
        current_Bullet = Mathf.Clamp(current_Bullet+change,0,maxBullet);
        mana.Set_CurrentBullet(current_Bullet);
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && current_Bullet > 0)
        {
            attack.Attacking();
            Change_Bullet(-1);
        }
        else isAttack = false;
        attack.isAttack=isAttack;
    }
    
    void Moving()
    {
        //前后移动
        if (Input.GetKey(KeyCode.W))
        {
            cc.Move(transform.forward * moveSpeed * Time.deltaTime);
            animator.SetBool("isMove",true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            cc.Move(transform.forward * (-moveSpeed) * Time.deltaTime);
        }
        if (!Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isMove", false);
        }
        //左右移动
        if (Input.GetKey(KeyCode.A))
        {
            cc.Move(transform.right * (-moveSpeed) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            cc.Move(transform.right * moveSpeed * Time.deltaTime);
        }

        //上下 空格跳跃
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround) {
                isJump = true;
            }
        }
        if (isJump)
        {
            //计时
            jumpTime += Time.deltaTime;
            //上升
            if (jumpTime<jumpMaxTime) jumpNow = jumpSpeed;
            //下降
            else if (jumpTime >= jumpMaxTime)
            {
                jumpNow = -10;
                //到达地面
                if (isGround)
                {
                    jumpTime = 0;
                    isJump = false;
                }
            }
        }
        Jump(jumpNow);
    }

    void Jump(float jumpNow)
    {
        cc.Move(Vector3.up * jumpNow * Time.deltaTime);
    }

    void Turning()
    {
        //获取鼠标在水平方向上移动的距离
        float h = Input.GetAxis("Mouse X")*rotateSpeed;
        //获取鼠标在垂直方向上移动的距离
        float v = Input.GetAxis("Mouse Y") * rotateSpeed;
        //以自身向上为坐标轴，旋转h和v大小的角度
        transform.Rotate(Vector3.up,h,Space.Self);
        headRotation.x = Mathf.Clamp(headRotation.x-v,-45f,45f);
        head.transform.localEulerAngles = headRotation;

    }

    /*
    //键盘控制移动，鼠标控制方向(按照帧数来，速度过快容易穿墙)
    private void Moving()
    {
        //前后移动
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * (-moveSpeed) * Time.deltaTime);
        }
        //左右移动
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * (-rotateSpeed) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
        //上下 空格跳跃
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                isJump = true;
            }
        }
        if (isJump)
        {
            //计时
            jumpTime += Time.deltaTime;
            //上升
            if (jumpTime < jumpMaxTime) jumpNow = jumpSpeed;
            //下降
            else if (jumpTime >= jumpMaxTime)
            {
                jumpNow = -10;
                //到达地面
                if (isGround)
                {
                    jumpTime = 0;
                    isJump = false;
                }
            }
        }
        Jump(jumpNow);
    }
    */
}
