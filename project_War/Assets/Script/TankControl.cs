using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour
{
    private Attack attack;
    private AutoNavigate navigate;

    public int bloodBar = 3;//即为max_blood
    private int current_blood;
    private Blood blood;

    public GameObject dieEffect;
    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<Attack>();
        navigate = GetComponent<AutoNavigate>();
        blood = transform.Find("Canvas/BloodSlider").GetComponent<Blood>();
        blood.Set_MaxHP(bloodBar);
        current_blood = bloodBar;
    }

    // Update is called once per frame
    void Update()
    {
        if (navigate.hits[1].collider&&navigate.hits[1].transform.name=="Human") {
            attack.isAttack = true;
        }
    }
    public void Change_HP(int change)
    {
        //返回修改后的血量，并将值控制在0到最大值之间
        current_blood = Mathf.Clamp(current_blood + change, 0, bloodBar);
        //在blood对象中控制修改数值
        blood.Set_CurrentHP(current_blood);
        if (current_blood <= 0)
        {
            Instantiate(dieEffect,transform.position,Quaternion.Euler(-90,0,0));
            Destroy(gameObject);
        }
    }

}
