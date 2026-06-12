using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour
{

    public int difficulty = 1;

    public GameObject enemytype;

    public GameObject fastenemytype;

    public GameObject starenemytype;

    public GameObject bubbleenemytype;

    public GameObject snipeenemytype;

    public GameObject spawnerenemytype;

    public GameObject meteortype;

    public GameObject shopMenu;

    public ParticleSystem current;

    public GameObject rotateparent;

    public ShopUI UI;

    List<GameObject> spawnpoints;


    public bool enemies = true;

    // Start is called before the first frame update
    void Start()
    {
        spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList();

        StartCoroutine(spawnEnemies());
        StartCoroutine(spawnRandomMeteors());
        StartCoroutine(spawnOceanCurrent());

        ExitScreen.instance = GameObject.FindGameObjectWithTag("Exit");
    }

    // Update is called once per frame
    void Update()
    {


        StaticVars.wave = difficulty;





        List<EnemyBehavior> l = new List<EnemyBehavior>();
        l.AddRange(UnityEngine.Object.FindObjectsOfType<EnemyBehavior>());
        if (l.Count > 1) enemies = true;
        else enemies = false;



    }

    private IEnumerator spawnRandomMeteors()
    {

        while (CharacterController2D.Instance != null)
        {

            yield return new WaitForSeconds(UnityEngine.Random.Range(30, 90));

            int e = UnityEngine.Random.Range(1, 2);

            while (e > 0)
            {
                GameObject Sp = spawnpoints.GetRandomItem();

                yield return Sp.GetComponent<SpawnPoint>().DisplayWarn();

                GameObject enemy = Instantiate(meteortype);
                MeteorBehavior b = enemy.GetComponent<MeteorBehavior>();
                b.speed *= (float)UnityEngine.Random.Range(5000, 15000) / 10000;
                enemy.transform.localScale *= (float)UnityEngine.Random.Range(2000, 10000) / 10000;

                enemy.transform.position = Sp.transform.position;
                e--;
            }

        }

    }

    private IEnumerator spawnOceanCurrent()
    {

        while (CharacterController2D.Instance != null)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(UnityEngine.Random.Range(50, 240));

            print("enabling current");
            Quaternion dir = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));



            yield return new WaitForSeconds(3f);

            float speed = 0.02f;

            rotateparent.transform.rotation = dir;


            current.enableEmission = true;

            dir *= Quaternion.Euler(0, 0, -90f);

                CharacterController2D.Instance.passivevelocity.x += (rotateparent.transform.right * speed).x;

                CharacterController2D.Instance.passivevelocity.y += (rotateparent.transform.right * speed).y;

            yield return new WaitForSeconds(UnityEngine.Random.Range(27, 43));


            current.enableEmission = false;


            //ToDo: Interpolate passive velocity back to 0
            CharacterController2D.Instance.passivevelocity = new Vector2();
        }

    }

    private IEnumerator spawnEnemies()
    {

    while (CharacterController2D.Instance != null) {


            yield return new WaitForSeconds(1f);

            if (enemies == false)
            {

                yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 6f));

                //how many basic enemies to spawn
                int e = 1 + UnityEngine.Random.Range(0, (int)(difficulty * 0.2));

                //how many special enemies to spawn
                int s = 1 + UnityEngine.Random.Range(0, (int)(difficulty * 0.15));

                int sn = 0;

                //how many gimmick
                int g = 0;

                int h = 0;

                //how many big guys
                int sp = 0;

                if (difficulty > 4)
                {
                    if (UnityEngine.Random.Range(0, 10) > 5) g = 1 + UnityEngine.Random.Range(0, (int)(difficulty * 0.24));
                    else h = 1 + UnityEngine.Random.Range(0, (int)(difficulty * 0.24));
                }

                if (difficulty > 9)
                {
                    if (UnityEngine.Random.Range(0, 10) > 5)
                    {
                        s = 0;
                        sn = 1 + UnityEngine.Random.Range(0, (int)(difficulty * 0.1));

                    }

                    if (UnityEngine.Random.Range(0, 10) > 5)
                    {
                        sp = 1 + UnityEngine.Random.Range(0, (int)(difficulty * 0.08));

                    }

                }


                //SpawnDefaultEnemies
                while (e > 0)
                {
                    GameObject Sp = spawnpoints.GetRandomItem();

                    yield return Sp.GetComponent<SpawnPoint>().DisplayWarn();


                    GameObject enemy = Instantiate(enemytype);
                    EnemyBehavior b = enemy.GetComponent<EnemyBehavior>();
                    b.speed *= (float)UnityEngine.Random.Range(8000, 15000) / 10000;
                    b.turnspeed *= (float)UnityEngine.Random.Range(5000, 15000) / 10000;
                    enemy.transform.localScale *= (float)UnityEngine.Random.Range(7500, 12500) / 10000;

                    enemy.transform.position = Sp.transform.position;
                    e--;
                }


                //SpawnFastEnemies
                while (s > 0)
                {
                    GameObject Sp = spawnpoints.GetRandomItem();

                    yield return Sp.GetComponent<SpawnPoint>().DisplayWarn();

                    GameObject enemy = Instantiate(fastenemytype);
                    FastEnemyBehavior b = enemy.GetComponent<FastEnemyBehavior>();
                    b.speed *= (float)UnityEngine.Random.Range(8000, 12000) / 10000;
                    b.turnspeed *= (float)UnityEngine.Random.Range(7500, 12500) / 10000;
                    enemy.transform.localScale *= (float)UnityEngine.Random.Range(7500, 12500) / 10000;

                    enemy.transform.position = Sp.transform.position;
                    s--;
                }

                //SpawnSnipeEnemies
                while (sn > 0)
                {
                    GameObject Sp = spawnpoints.GetRandomItem();

                    yield return Sp.GetComponent<SpawnPoint>().DisplayWarn();

                    GameObject enemy = Instantiate(snipeenemytype);
                    SnipeEnemyBehaviour b = enemy.GetComponent<SnipeEnemyBehaviour>();
                    enemy.transform.localScale *= (float)UnityEngine.Random.Range(9000, 11000) / 10000;

                    enemy.transform.position = Sp.transform.position;
                    sn--;
                }


                //SpawnSpawner
                while (sp > 0)
                {
                    GameObject Sp = spawnpoints.GetRandomItem();

                    yield return Sp.GetComponent<SpawnPoint>().DisplayWarn();

                    GameObject enemy = Instantiate(spawnerenemytype);
                    SpawnEnemyBehaviour b = enemy.GetComponent<SpawnEnemyBehaviour>();
                    enemy.transform.localScale *= (float)UnityEngine.Random.Range(9000, 11000) / 10000;

                    enemy.transform.position = Sp.transform.position;
                    sp--;
                }


                //SpawnStarEnemies
                while (g > 0)
                {

                    GameObject Sp = spawnpoints.GetRandomItem();

                    yield return Sp.GetComponent<SpawnPoint>().DisplayWarn();

                    GameObject enemy = Instantiate(starenemytype);
                    BounceEnemyBehavior b = enemy.GetComponent<BounceEnemyBehavior>();
                    b.speed *= (float)UnityEngine.Random.Range(8000, 12000) / 10000;

                    enemy.transform.position = Sp.transform.position;
                    g--;
                }

                //SpawnDefaultEnemies
                while (h > 0)
                {
                    GameObject Sp = spawnpoints.GetRandomItem();

                    yield return Sp.GetComponent<SpawnPoint>().DisplayWarn();

                    GameObject enemy = Instantiate(bubbleenemytype);
                    BubbleEnemyBehaviour b = enemy.GetComponent<BubbleEnemyBehaviour>();
                    b.speed *= (float)UnityEngine.Random.Range(5000, 15000) / 10000;
                    b.turnspeed *= (float)UnityEngine.Random.Range(5000, 15000) / 10000;
                    enemy.transform.localScale *= (float)UnityEngine.Random.Range(7500, 12500) / 10000;

                    enemy.transform.position = Sp.transform.position;
                    h--;
                }

                difficulty++;
                StaticVars.highscore += 5 * difficulty;


                if (difficulty % 5 == 0)
                {
                    shopMenu.SetActive(true);
                    UI.UpdateShop(difficulty);
                    Time.timeScale = 0;
                }

            }

        }

    }

}
