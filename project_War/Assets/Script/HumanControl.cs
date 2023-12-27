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

    public int bloodBar = 10;//��Ϊmax_blood
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
        //�����ƶ�
        Moving();
        //���Ʒ���
        Turning();
        //���ƹ���
        Attack();
        //�����Χ����
        CheckObject();
        //�������λ��
        Check_Position();
    }

    private void CheckObject()
    {
        ray = new Ray(transform.position, transform.forward);
        //�Ƿ��ڵ���
        isGround = Physics.Raycast(transform.position, Vector3.down, 2.3f, LayerMask.GetMask("StaticSpace"));
        //�����Χ��ز����� ��ȡhit
        Physics.SphereCast(ray,1.1f,out hit,1.1f,layerMask);

        //��⵽ħ����
        CheckMagicBall();
        //��⵽����
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
        //�����޸ĺ��Ѫ��������ֵ������0�����ֵ֮��
        current_blood = Mathf.Clamp(current_blood+change,0,bloodBar);
        //��blood�����п����޸���ֵ
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
        //ǰ���ƶ�
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
        //�����ƶ�
        if (Input.GetKey(KeyCode.A))
        {
            cc.Move(transform.right * (-moveSpeed) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            cc.Move(transform.right * moveSpeed * Time.deltaTime);
        }

        //���� �ո���Ծ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround) {
                isJump = true;
            }
        }
        if (isJump)
        {
            //��ʱ
            jumpTime += Time.deltaTime;
            //����
            if (jumpTime<jumpMaxTime) jumpNow = jumpSpeed;
            //�½�
            else if (jumpTime >= jumpMaxTime)
            {
                jumpNow = -10;
                //�������
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
        //��ȡ�����ˮƽ�������ƶ��ľ���
        float h = Input.GetAxis("Mouse X")*rotateSpeed;
        //��ȡ����ڴ�ֱ�������ƶ��ľ���
        float v = Input.GetAxis("Mouse Y") * rotateSpeed;
        //����������Ϊ�����ᣬ��תh��v��С�ĽǶ�
        transform.Rotate(Vector3.up,h,Space.Self);
        headRotation.x = Mathf.Clamp(headRotation.x-v,-45f,45f);
        head.transform.localEulerAngles = headRotation;

    }

    /*
    //���̿����ƶ��������Ʒ���(����֡�������ٶȹ������״�ǽ)
    private void Moving()
    {
        //ǰ���ƶ�
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * (-moveSpeed) * Time.deltaTime);
        }
        //�����ƶ�
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * (-rotateSpeed) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
        //���� �ո���Ծ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                isJump = true;
            }
        }
        if (isJump)
        {
            //��ʱ
            jumpTime += Time.deltaTime;
            //����
            if (jumpTime < jumpMaxTime) jumpNow = jumpSpeed;
            //�½�
            else if (jumpTime >= jumpMaxTime)
            {
                jumpNow = -10;
                //�������
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
