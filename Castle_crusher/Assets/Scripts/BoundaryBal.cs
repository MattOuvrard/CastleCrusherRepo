using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryBal : MonoBehaviour
{
    public List<GameObject> in_mine;
    public bool actif = false;
    public bool avertit = false;
    // Start is called before the first frame update
    void Start()
    {
        in_mine = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ThereAreBall() && actif && !avertit)
        {
            GameManager.instance.YAPlusDeBall();
            avertit = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!ExistInMine(other.gameObject))
        {
            in_mine.Add(other.gameObject);
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (!ExistInMine(collision.gameObject))
    //    {
    //        in_mine.Add(collision.gameObject);
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (ExistInMine(other.gameObject))
        {
            in_mine.Remove(other.gameObject);
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (ExistInMine(collision.gameObject))
    //    {
    //        in_mine.Remove(collision.gameObject);
    //    }
    //}

    bool ExistInMine(GameObject m_check)
    {
        foreach(GameObject go  in in_mine)
        {
            if(m_check == go)
            {
                return true;
            }
        }
        return false;
    }

    bool ThereAreBall()
    {
        foreach(GameObject go in in_mine)
        {
            if(go == null)
            {
                in_mine.Remove(go);
            }
            else if(go.tag == "Ball")
            {
                return true;
            }
        }
        return false;
    }

    public void Activation(bool verite)
    {
        actif = verite;
        avertit = !verite;
        //in_mine.RemoveAll();
    }
}
