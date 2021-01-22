using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCOlission : MonoBehaviour
{
    public RobotManager m_mang;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            m_mang.Contact();
        }
    }


}
