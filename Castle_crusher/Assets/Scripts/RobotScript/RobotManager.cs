using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour
{
    public EcranMaManager m_ecran;

    public float temptransition;
    float compteurtransition;
    bool enTransition;

    public float tempsRecover;
    float compteurRecover;



    bool _isAngry = true;

    private void Awake()
    {
        foreach(Transform t in transform)
        {
            if (t.GetComponent<RobotCOlission>())
            {
                t.GetComponent<RobotCOlission>().m_mang = this;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Contact();
        }
    }


    public void Contact()
    {
        AudioManager.instance.jouer("CoupMetal");
        Chocker();
    }



    void Chocker()
    {

        m_ecran.ChangeGlitch(true);
        compteurtransition = 0;
        enTransition = true;
        _isAngry = false;
        compteurRecover = 0;
    }
     void retablie()
    {
        m_ecran.ChangeGlitch(true);
        compteurtransition = 0;
        enTransition = true;
        _isAngry = true;
        compteurRecover = 0;
    }


    private void Update()
    {
        if (enTransition)
        {
            compteurtransition += Time.deltaTime;
            if(compteurtransition > temptransition)
            {
                m_ecran.changeAngry(_isAngry);
                m_ecran.ChangeGlitch(false);
                enTransition = false;
            }
        }

        if (!_isAngry)
        {
            compteurRecover += Time.deltaTime;
            if(compteurRecover > tempsRecover)
            {
                retablie();
            }
        }

    }
}
