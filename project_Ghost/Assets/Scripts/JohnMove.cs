/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMove : MonoBehaviour
{
    public EndLogic end;

    //人物旋转速度
    public float turnSpeed = 20f;
    //定义游戏人物上的组件
    Animator m_Animator;
    Rigidbody m_rigidbody;
    //游戏人物的矢量
    Vector3 m_Movement;
    //游戏人物旋转角度
    Quaternion m_Quaternion = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        //获取游戏人物上的刚体，动画控制机组件
        m_Animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    //FixedUpdate()以固定帧长的大小进行
    private void FixedUpdate()
    {


        //获取水平、竖直是否有键值输入
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //设置人物移动的方向
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        //定义游戏人物是否移动
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVercticalInput = !Mathf.Approximately(vertical, 0f);

        bool isWalking = hasHorizontalInput || hasVercticalInput;
        m_Animator.SetBool("isWalk", isWalking);

        //旋转的过渡
        Vector3 desirForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Quaternion = Quaternion.LookRotation(desirForward);
    }

    private void OnAnimatorMove()
    {
        m_rigidbody.MovePosition(m_rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_rigidbody.MoveRotation(m_Quaternion);
    }

    public float rollSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;

    Vector3 m_Movement;
    Quaternion m_Quaternion = Quaternion.identity;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdata()
    {
        //获得按键信息
        float horizontal = Input.GetAxis("Horizontal");
        float vectical = Input.GetAxis("Vectical");

        //获得方向
        m_Movement.Set(vectical, 0f, horizontal);
        m_Movement.Normalize();

        //判断是否移动
        bool isWalking = !Mathf.Approximately(horizontal, 0f) || !Mathf.Approximately(vectical, 0f);
        m_Animator.SetBool("isWalk", isWalking);

        //旋转过渡
        Vector3 desirForward = Vector3.RotateTowards(transform.forward, m_Movement, rollSpeed * Time.deltaTime, 0f);
        m_Quaternion = Quaternion.LookRotation(desirForward);
    }
    
    private void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Quaternion);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FinishLine")
        {
            end.isExit_win = true;
        }
        if (other.gameObject.name == "Ghost" || other.gameObject.name == "Ghost_s")
        {
            //先这样，后期变成血条减少
            end.isExit_fail = true;
        }
    }
}*/


using UnityEngine;

public class JohnMove : MonoBehaviour
{
    //结束脚本
    public EndLogic end;

    //摄像机
    public GameObject playerView;

    //速度：每秒移动5个单位长度
    public float moveSpeed = 6;
    //角速度：每秒旋转135度
    public float angularSpeed = 135;
    //跳跃参数
    public float jumpForce = 200f;

    //水平视角灵敏度
    public float horizontalRotateSensitivity = 10;
    //垂直视角灵敏度
    public float verticalRotateSensitivity = 5;

    //最大俯角
    public float maxDepressionAngle = 90;

    //最大仰角
    public float maxElevationAngle = 25;

    //角色的刚体
    private Rigidbody rigidbody;

    //获取动画机
    Animator m_Animator;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
        View();
        Jump();
    }

    void Move()
    {
        //通过键盘获取竖直、水平轴的值，范围在-1到1
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        //按照矢量移动一段距离
        transform.Translate(Vector3.forward * v * Time.deltaTime * moveSpeed);
        transform.Translate(Vector3.right * h * Time.deltaTime * moveSpeed);

        //判断是否移动
        bool isWalking =Input.GetKeyDown(KeyCode.W);
        m_Animator.SetBool("isWalk", isWalking);
    }

    void View()
    {
        //锁定鼠标到屏幕中心
        SetCursorToCentre();

        //当前垂直角度
        double VerticalAngle = playerView.transform.eulerAngles.x;

        //通过鼠标获取竖直、水平轴的值，范围在-1到1
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y") * -1;

        //角色水平旋转
        transform.Rotate(Vector3.up * h * Time.deltaTime * angularSpeed * horizontalRotateSensitivity);

        //计算本次旋转后，竖直方向上的欧拉角
        double targetAngle = VerticalAngle + v * Time.deltaTime * angularSpeed * verticalRotateSensitivity;

        //竖直方向视角限制
        if (targetAngle > maxDepressionAngle && targetAngle < 360 - maxElevationAngle) return;

        //摄像机竖直方向上旋转
        playerView.transform.Rotate(Vector3.right * v * Time.deltaTime * angularSpeed * verticalRotateSensitivity);
    }

    void SetCursorToCentre()
    {
        //锁定鼠标后再解锁，鼠标将自动回到屏幕中心
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
        //隐藏鼠标
        Cursor.visible = false;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(Vector3.up * jumpForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FinishLine")
        {
            end.isExit_win = true;
        }
        if (other.gameObject.name == "Ghost" || other.gameObject.name == "Ghost_s")
        {
            //先这样，后期变成血条减少
            end.isExit_fail = true;
        }
    }
}