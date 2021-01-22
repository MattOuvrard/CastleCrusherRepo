using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    // ce manager va servir pour changer les materiaux des objets et leur donné un nouveau material a l'apparition a a la disparition
    public List<MeshRenderer> lesOBjets;
    public List<Material> lesMaterial;
    GameObject leBloc;

    public Material dissolveMat;
    bool disparition;
    bool activation = false;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        lesOBjets = new List<MeshRenderer>();
        lesMaterial = new List<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activation)
        {
            if (disparition)
            {
                dissolveMat.SetFloat("Vector1_B3360ECD",
                    dissolveMat.GetFloat("Vector1_B3360ECD") + Time.deltaTime * speed);
                Debug.Log(dissolveMat.GetFloat("Vector1_B3360ECD"));
                if (dissolveMat.GetFloat("Vector1_B3360ECD") >= 1)
                {
                    fin(true);
                }
            }
            else
            {
                //Debug.Log(dissolveMat.GetFloat("Vector1_B3360ECD"));
                dissolveMat.SetFloat("Vector1_B3360ECD",
                    dissolveMat.GetFloat("Vector1_B3360ECD") - Time.deltaTime * speed);              
                if (dissolveMat.GetFloat("Vector1_B3360ECD") <= 0  )
                {
                    dissolveMat.SetFloat("Vector1_B3360ECD", -0.15f);
                    fin(false);
                }
            }
        }
    }



    public void Lancement(GameObject monlevel,bool apparition)
    {
        float amount;
        if (apparition)
            amount = 1;
        else
            amount = -0.15f;


        if (monlevel != null)
        {
            leBloc = monlevel;
            var allchildren = monlevel.GetComponentsInChildren<Transform>();
            lesMaterial.Clear(); lesOBjets.Clear();
            foreach (Transform t in allchildren)
            {
                if (t.gameObject.GetComponent<MeshRenderer>())
                {
                    lesOBjets.Add(t.gameObject.GetComponent<MeshRenderer>());
                    lesMaterial.Add(t.gameObject.GetComponent<MeshRenderer>().materials[0]);
                    if (apparition)
                    {
                        t.gameObject.GetComponent<MeshRenderer>().material = dissolveMat;
                        //dissolveMat.SetFloat("Vector1_B3360ECD", amount);
                    }
                    else
                    {
                        int tailleMa =  t.gameObject.GetComponent<MeshRenderer>().materials.Length;
                        Material[] mesTests = new Material[tailleMa];
                        t.gameObject.GetComponent<MeshRenderer>().material = dissolveMat;
                        for (int i =0; i < tailleMa; i++)
                        {
                            //t.gameObject.GetComponent<MeshRenderer>().materials[i] = dissolveMat;
                            mesTests[i] = dissolveMat;
                        }
                        //dissolveMat.SetFloat("Vector1_B3360ECD", amount);   
                        t.gameObject.GetComponent<MeshRenderer>().materials = mesTests;
                    }
                }
            }
        }
        dissolveMat.SetFloat("Vector1_B3360ECD", amount);


        if (monlevel != null)
           activation = true;

        if (apparition)
            disparition = false;
        else
            disparition = true; ;

    }


    public void fin(bool isdisparition)
    {


        if (isdisparition)
        {

            lesMaterial.Clear();
            lesOBjets.Clear();
            Destroy(leBloc);
            leBloc = null;
        }
        else // on remet les material en place
        {
            for(int i = 0; i < lesMaterial.Count; i++)
            {
                lesOBjets[i].material = lesMaterial[i];
                lesOBjets[i].materials[0] = lesMaterial[i];
                
            }
        }
        activation = false;
    }

}
