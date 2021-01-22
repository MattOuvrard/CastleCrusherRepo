using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance { get; set; }
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            instance = _instance;
        }
        else
        {
            Destroy(this);
        }
    }


    int level;
    public int levelMax;
    int nbTotemTomber;
    int nbTotem;
    GameObject LevelActuel;
    bool Brouillard;
    [SerializeField]
    Text_Manager m_text; 
    [SerializeField]
    Transform centerLevel;
    public float vitesseBrouillard;
    public GameObject Halo;
    public BallManager m_ballManag;
    public GameObject Cache;

    bool YaDesBall = true;
    float minuteur = 0;
    int Rebours = 0;


    BoundaryBal m_boundary;
    MaterialManager m_MM;




    // Start is called before the first frame update
    void Start()
    {
        m_MM = GetComponent<MaterialManager>();
        nbTotemTomber = 0;
        level = -1;
        m_boundary = GetComponentInChildren<BoundaryBal>();
        StartCoroutine("ChangerLevel");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Brouillard)
        {
            if( RenderSettings.fogEndDistance > 20)
            {

                // reduit le brouillard
                RenderSettings.fogEndDistance -= vitesseBrouillard;
            }
            else
            {
                Cache.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        else if (!Brouillard && RenderSettings.fogEndDistance < 200)
        {
            //on augmente le brouillard
            RenderSettings.fogEndDistance += vitesseBrouillard *2;
            if(RenderSettings.fogEndDistance > 185)
            {
                RenderSettings.skybox = Resources.Load<Material>("SkyBox/Skybox");
                changerNombreAAbattre();
                Cache.GetComponent<MeshRenderer>().enabled = false;
                //m_boundary.actif = true;
                m_boundary.Activation(true);
            }                
        }*/

        




        if (Input.GetKeyDown(KeyCode.N))
        {
            //level++;
            //StartCoroutine("ChangerLevel");
        }

        // timer si jamais y a plus de ball
        if (!YaDesBall)
        {
            minuteur += Time.deltaTime;
            if(minuteur > 15)
            {
                //gameover
                if (Rebours != -1)
                {
                    m_text.ChangeText(" GameOver");
                }
                Rebours = -1;
            }
            else if(minuteur > 13)
            {
                //0
                if (Rebours != 0)
                {
                    m_text.ChangeText(" 0 ");
                }
                Rebours = 0;
            }
            else if(minuteur > 11)
            {
                //1
                if(Rebours != 1)
                {
                    m_text.ChangeText(" 1 ");
                }
                Rebours = 1;

            }
            else if(minuteur > 8)
            {
                //2
                if(Rebours != 2)
                {
                    m_text.ChangeText(" 2 ");
                }
                Rebours = 2;
            }
            else if(minuteur > 5)
            {
                //3
                if (Rebours != 3)
                {
                    m_text.ChangeText(" 3 ");
                }
                Rebours = 3;
            }

        }
    }

    IEnumerator ChangerLevel()
    {
        nbTotemTomber = 0;
        nbTotem = 0;
        level++;

        YaDesBall = true;
        m_boundary.actif = false;
        m_boundary.Activation(false);
        minuteur = 0;

        m_MM.Lancement(LevelActuel, false);

        if(level >= levelMax)
        {
            m_text.ChangeText("You Win");
            yield break;
        }

        // ici on fait apparaitre le brouillard
        Brouillard = true;
        /*RenderSettings.skybox = Resources.Load<Material>("SkyBox/HDRMatt");*/
        m_text.ChangeText("Wait");
        yield return new WaitForSeconds(3);

        // on supprime l'ancien level
        //Destroy(LevelActuel);
        //m_MM.Lancement(LevelActuel, false);
        LevelActuel = null;
        m_ballManag.CleanBall();


        // puis on charge nouveau level
        var temp = Instantiate(Resources.Load<GameObject>("Level/niv_" + level + "/Level" + level + Random.Range(0, 2)), centerLevel);
        LevelActuel = temp; //Resources.Load<GameObject>("Level/niv_" + level + "Level"+ level + Random.Range(0, 3));
        if (LevelActuel != null)
        {
            foreach (Transform t in LevelActuel.transform)// on defini le nombre de totem a faire tomber
            {
                if (t.gameObject.tag == "Totem")
                {
                    nbTotem++;
                }
            }
            if (LevelActuel.GetComponent<LevelManager>())
            {
                m_ballManag.CreateBall(LevelActuel.GetComponent<LevelManager>().config);
            }
            m_MM.Lancement(LevelActuel, true);// ici on lance le changement de texture
        }



        yield return new WaitForSeconds(2);



        // puis on change a nouveau la brume
        Brouillard = false;
        changerNombreAAbattre();
        m_boundary.Activation(true);
    }

    public void EstCequeLeDernierTotemEstTomber()
    {
       
            if(nbTotemTomber < nbTotem)
            {
                changerNombreAAbattre();
                return; // y en a toujours qui son en lice
            }

        StartCoroutine("ChangerLevel");
    }

    public void TotemTomber(Vector3 pos)
    {
        nbTotemTomber++;        
        Instantiate(Halo, new Vector3(pos.x, Halo.transform.position.y, pos.z), Quaternion.identity, m_ballManag.transform);
        EstCequeLeDernierTotemEstTomber();
    }


    public void changerNombreAAbattre()
    {
        if(nbTotem - nbTotemTomber == 1)
        {
            m_text.ChangeText("Shoot It");
        }
        else
        {
            int t = nbTotem - nbTotemTomber;
            m_text.ChangeText("Shoot " + t);
        }

    }

    void bloquerNiveau()
    {
        foreach(Transform t in LevelActuel.transform)
        {
            if (t.GetComponent<Rigidbody>())
            {
                t.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }




    public void YAPlusDeBall()
    {
        m_text.ChangeText("No more ball");
        YaDesBall = false;
    }

}
