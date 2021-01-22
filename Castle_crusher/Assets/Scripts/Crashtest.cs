using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crashtest : MonoBehaviour
{
    public GameObject m_o;
    Material m_m;
    Shader actuel;
    public Shader dissolve;

    // Start is called before the first frame update
    void Start()
    {
        m_m = m_o.GetComponent<MeshRenderer>().material;
        actuel = m_m.shader;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            m_m.shader = dissolve;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            m_m.shader = actuel;
        }
    }
}
