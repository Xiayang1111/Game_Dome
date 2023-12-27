using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollLogic : MonoBehaviour
{
    public float rollSpeed = 30f;
    public float rollEnd = 70f;

    int num = 1;
    float roll_y;

    // Start is called before the first frame update
    void Start()
    {
        roll_y = this.transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        Roll();
    }

    public void Roll()
    {
        if (num == 1)
        {
            if (roll_y < rollEnd) roll_y += rollSpeed * Time.deltaTime;
            else num = 0;
        }
        else
        {
            if (roll_y > -rollEnd) roll_y += -rollSpeed * Time.deltaTime;
            else num = 1;
        }
        this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, roll_y, this.transform.localEulerAngles.z);
    }
}
