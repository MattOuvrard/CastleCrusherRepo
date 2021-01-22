using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePanneau : BreakableObject
{
    [SerializeField]
    GameObject BatimentLier;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            SwitchBreak();
        }
    }

    private void Update()
    {
        if(BatimentLier == null)
        {
            SwitchBreak();
        }
    }
}
