using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
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

    public void Set_MaxBullet(int max_Value)
    {
        slider.maxValue = max_Value;
    }

    public void Set_CurrentBullet(int current_Value)
    {
        slider.value = current_Value;
    }
}
