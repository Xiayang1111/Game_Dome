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

    //设置最大值的范围，并将当前值也设置为最大值
    public void Set_MaxHP(int max_vlaues)
    {
        slider.maxValue = max_vlaues;
        slider.value = max_vlaues;
    }

    //修改数值，使value保持和当前生命值一致
    public void Set_CurrentHP(int current_value)
    {
        slider.value = current_value;
    }
    
}
