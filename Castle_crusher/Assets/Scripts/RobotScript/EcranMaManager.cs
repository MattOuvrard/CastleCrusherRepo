using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcranMaManager : MonoBehaviour
{

    Material m_ecran;
    public bool isAngry;
    public bool isGlitch;
    // Start is called before the first frame update
    void Start()
    {
        isAngry = true;
        isGlitch = false;
        m_ecran = GetComponent<MeshRenderer>().material;
    }

    public void changeAngry(bool b)
    {
        m_ecran.SetFloat("Boolean_248A4311", b ? 1f : 0f);
    }

    public void ChangeGlitch(bool b)
    {
        m_ecran.SetFloat("Boolean_F6495F8F", b ? 1f : 0f);
    }
}
