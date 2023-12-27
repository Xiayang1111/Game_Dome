using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blood : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�������ֵ�ķ�Χ��������ǰֵҲ����Ϊ���ֵ
    public void Set_MaxHP(int max_vlaues)
    {
        slider.maxValue = max_vlaues;
        slider.value = max_vlaues;
    }

    //�޸���ֵ��ʹvalue���ֺ͵�ǰ����ֵһ��
    public void Set_CurrentHP(int current_value)
    {
        slider.value = current_value;
    }
    
}
