/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMove : MonoBehaviour
{
    public EndLogic end;

    //������ת�ٶ�
    public float turnSpeed = 20f;
    //������Ϸ�����ϵ����
    Animator m_Animator;
    Rigidbody m_rigidbody;
    //��Ϸ�����ʸ��
    Vector3 m_Movement;
    //��Ϸ������ת�Ƕ�
    Quaternion m_Quaternion = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        //��ȡ��Ϸ�����ϵĸ��壬�������ƻ����
        m_Animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    //FixedUpdate()�Թ̶�֡���Ĵ�С����
    private void FixedUpdate()
    {


        //��ȡˮƽ����ֱ�Ƿ��м�ֵ����
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //���������ƶ��ķ���
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        //������Ϸ�����Ƿ��ƶ�
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVercticalInput = !Mathf.Approximately(vertical, 0f);

        bool isWalking = hasHorizontalInput || hasVercticalInput;
        m_Animator.SetBool("isWalk", isWalking);

        //��ת�Ĺ���
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
        //��ð�����Ϣ
        float horizontal = Input.GetAxis("Horizontal");
        float vectical = Input.GetAxis("Vectical");

        //��÷���
        m_Movement.Set(vectical, 0f, horizontal);
        m_Movement.Normalize();

        //�ж��Ƿ��ƶ�
        bool isWalking = !Mathf.Approximately(horizontal, 0f) || !Mathf.Approximately(vectical, 0f);
        m_Animator.SetBool("isWalk", isWalking);

        //��ת����
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
            //�����������ڱ��Ѫ������
            end.isExit_fail = true;
        }
    }
}*/


using UnityEngine;

public class JohnMove : MonoBehaviour
{
    //�����ű�
    public EndLogic end;

    //�����
    public GameObject playerView;

    //�ٶȣ�ÿ���ƶ�5����λ����
    public float moveSpeed = 6;
    //���ٶȣ�ÿ����ת135��
    public float angularSpeed = 135;
    //��Ծ����
    public float jumpForce = 200f;

    //ˮƽ�ӽ�������
    public float horizontalRotateSensitivity = 10;
    //��ֱ�ӽ�������
    public float verticalRotateSensitivity = 5;

    //��󸩽�
    public float maxDepressionAngle = 90;

    //�������
    public float maxElevationAngle = 25;

    //��ɫ�ĸ���
    private Rigidbody rigidbody;

    //��ȡ������
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
        //ͨ�����̻�ȡ��ֱ��ˮƽ���ֵ����Χ��-1��1
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        //����ʸ���ƶ�һ�ξ���
        transform.Translate(Vector3.forward * v * Time.deltaTime * moveSpeed);
        transform.Translate(Vector3.right * h * Time.deltaTime * moveSpeed);

        //�ж��Ƿ��ƶ�
        bool isWalking =Input.GetKeyDown(KeyCode.W);
        m_Animator.SetBool("isWalk", isWalking);
    }

    void View()
    {
        //������굽��Ļ����
        SetCursorToCentre();

        //��ǰ��ֱ�Ƕ�
        double VerticalAngle = playerView.transform.eulerAngles.x;

        //ͨ������ȡ��ֱ��ˮƽ���ֵ����Χ��-1��1
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y") * -1;

        //��ɫˮƽ��ת
        transform.Rotate(Vector3.up * h * Time.deltaTime * angularSpeed * horizontalRotateSensitivity);

        //���㱾����ת����ֱ�����ϵ�ŷ����
        double targetAngle = VerticalAngle + v * Time.deltaTime * angularSpeed * verticalRotateSensitivity;

        //��ֱ�����ӽ�����
        if (targetAngle > maxDepressionAngle && targetAngle < 360 - maxElevationAngle) return;

        //�������ֱ��������ת
        playerView.transform.Rotate(Vector3.right * v * Time.deltaTime * angularSpeed * verticalRotateSensitivity);
    }

    void SetCursorToCentre()
    {
        //���������ٽ�������꽫�Զ��ص���Ļ����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
        //�������
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
            //�����������ڱ��Ѫ������
            end.isExit_fail = true;
        }
    }
}