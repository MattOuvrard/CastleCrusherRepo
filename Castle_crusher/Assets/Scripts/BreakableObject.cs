using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField]
    protected GameObject my_breakable;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            SwitchBreak();
        }
    }


    public void SwitchBreak()
    {
        GetComponent<Collider>().isTrigger = true;
        GameObject temp =  Instantiate(my_breakable, transform.position, transform.rotation, transform.parent);
        temp.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        Destroy(this.gameObject);
    }

}
