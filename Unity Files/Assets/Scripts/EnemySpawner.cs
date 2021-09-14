using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemySpawner : MonoBehaviour
{
    public int numEnemy;
    public int numEnemyRanged;
    public bool openerDone;
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;

    public GameObject enemy2Prefab;
    public GameObject enemy2Prefab2;
    public GameObject enemy2Prefab3;

    public GameObject enemy3Prefab;
    public GameObject enemy3Prefab2;
    public GameObject enemy3Prefab3;

    public GameObject rangePrefab;
    public GameObject rangePrefab2;
    public GameObject rangePrefab3;

    public GameObject range2Prefab;
    public GameObject range2Prefab2;
    public GameObject range2Prefab3;

    public GameObject range3Prefab;
    public GameObject range3Prefab2;
    public GameObject range3Prefab3;

    //public GameObject camera;
    private Text myText;
    private Text myTextRange;
    public Text destroyText;
    public Text rangedestroyText;

    // Start is called before the first frame update
    void Start()
    {
        openerDone = false;
        numEnemy = 3;
        numEnemyRanged = 3;
        myText = GameObject.Find("EHealth").GetComponent<Text>();
        myTextRange = GameObject.Find("EHealth_Range").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemyRange = GameObject.Find("EnemyRange");
        if (enemyRange)
        {
            if (enemyRange.tag == "EnemyRange")
            {
                RangeEnemyController controller = enemyRange.GetComponent<RangeEnemyController>();

                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemyRanged--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemyRange);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myTextRange.text = "0";
                    rangedestroyText.text = "";
                }
            }
            else if (enemyRange.tag == "EnemyRange2")
            {
                RangeEnemy2Controller controller = enemyRange.GetComponent<RangeEnemy2Controller>();

                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemyRanged--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemyRange);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myTextRange.text = "0";
                    rangedestroyText.text = "";
                }
            }
            else if (enemyRange.tag == "EnemyRange3")
            {
                RangeEnemy3Controller controller = enemyRange.GetComponent<RangeEnemy3Controller>();

                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemyRanged--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemyRange);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myTextRange.text = "0";
                    rangedestroyText.text = "";
                }
            }
        }

        GameObject enemyRange1 = GameObject.Find("EnemyRange1");
        if (enemyRange1)
        {
            if (enemyRange1.tag == "EnemyRange")
            {
                RangeEnemyController controller = enemyRange1.GetComponent<RangeEnemyController>();
                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemyRanged--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemyRange1);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myTextRange.text = "0";
                    rangedestroyText.text = "";
                }
            }
            else if (enemyRange1.tag == "EnemyRange2")
            {
                RangeEnemy2Controller controller = enemyRange1.GetComponent<RangeEnemy2Controller>();
                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemyRanged--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemyRange1);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myTextRange.text = "0";
                    rangedestroyText.text = "";
                }
            }
            if (enemyRange1.tag == "EnemyRange3")
            {
                RangeEnemy3Controller controller = enemyRange1.GetComponent<RangeEnemy3Controller>();
                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemyRanged--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemyRange1);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myTextRange.text = "0";
                    rangedestroyText.text = "";
                }
            }
        }

        GameObject enemyRange2 = GameObject.Find("EnemyRange2");
        if (enemyRange2)
        {
            if (enemyRange2.tag == "EnemyRange")
            {
                RangeEnemyController controller = enemyRange2.GetComponent<RangeEnemyController>();
                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemyRanged--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemyRange2);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myTextRange.text = "0";
                    rangedestroyText.text = "";
                }
            }
            else if (enemyRange2.tag == "EnemyRange2")
            {
                RangeEnemy2Controller controller = enemyRange2.GetComponent<RangeEnemy2Controller>();
                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemyRanged--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemyRange2);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myTextRange.text = "0";
                    rangedestroyText.text = "";
                }
            }
            else if (enemyRange2.tag == "EnemyRange3")
            {
                RangeEnemy3Controller controller = enemyRange2.GetComponent<RangeEnemy3Controller>();
                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemyRanged--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemyRange2);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myTextRange.text = "0";
                    rangedestroyText.text = "";
                }
            }
        }

        GameObject enemy = GameObject.Find("Enemy");
        if (enemy)
        {
            if (enemy.tag == "Enemy")
            {
                EnemyController controller = enemy.GetComponent<EnemyController>();
                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemy--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemy);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myText.text = "0";
                    destroyText.text = "";
                }
            }
            else if (enemy.tag == "Enemy2")
            {
                Enemy2Controller controller = enemy.GetComponent<Enemy2Controller>();
                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemy--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemy);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myText.text = "0";
                    destroyText.text = "";
                }
            }
            else if (enemy.tag == "Enemy3")
            {
                Enemy3Controller controller = enemy.GetComponent<Enemy3Controller>();
                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemy--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemy);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myText.text = "0";
                    destroyText.text = "";
                }
            }
        }

        GameObject enemy1 = GameObject.Find("Enemy1");
        if (enemy1)
        {
            if (enemy1.tag == "Enemy")
            {
                EnemyController controller = enemy1.GetComponent<EnemyController>();
                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemy--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemy1);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myText.text = "0";
                    destroyText.text = "";
                }
            }
            else if (enemy1.tag == "Enemy2")
            {
                Enemy2Controller controller = enemy1.GetComponent<Enemy2Controller>();
                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemy--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemy1);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myText.text = "0";
                    destroyText.text = "";
                }
            }
            else if (enemy1.tag == "Enemy3")
            {
                Enemy3Controller controller = enemy1.GetComponent<Enemy3Controller>();
                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemy--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemy1);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myText.text = "0";
                    destroyText.text = "";
                }
            }
        }

        GameObject enemy2 = GameObject.Find("Enemy2");
        if (enemy2)
        {
            if (enemy2.tag == "Enemy")
            {
                EnemyController controller = enemy2.GetComponent<EnemyController>();
                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemy--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemy2);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myText.text = "0";
                    destroyText.text = "";
                }
            }
            else if (enemy2.tag == "Enemy2")
            {
                Enemy2Controller controller = enemy2.GetComponent<Enemy2Controller>();
                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemy--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemy2);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myText.text = "0";
                    destroyText.text = "";
                }
            }
            else if (enemy2.tag == "Enemy3")
            {
                Enemy3Controller controller = enemy2.GetComponent<Enemy3Controller>();
                if (controller.GetHealth() <= 0)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<FileWriter>().EndEncounter();
                    numEnemy--;
                    player.GetComponent<PlayerController>().score++;
                    Destroy(enemy2);
                    //camera.GetComponent<CameraShake>().ShakeThisCamera();
                    myText.text = "0";
                    destroyText.text = "";
                }
            }
        }

        if (numEnemy < 3)
        {
            StartCoroutine(SpawnEnemy());
            numEnemy++;
        }

        if(numEnemyRanged < 3)
        {
            StartCoroutine(SpawnRange());
            numEnemyRanged++;
        }
    }

    IEnumerator SpawnRange()
    {
        // Creating prefab as child
        yield return new WaitForSeconds(Random.Range(3, 10));

        if (!GameObject.Find("EnemyRange"))
        {
            int rand = Random.Range(1, 4); // Return int between 1 and 3
            GameObject toSpawn;

            if (rand == 1)
                toSpawn = rangePrefab;
            else if (rand == 2)
                toSpawn = range2Prefab;
            else
                toSpawn = range3Prefab;

            GameObject go = Instantiate(toSpawn, new Vector3(8.5f, 2, 0), Quaternion.identity) as GameObject;
            go.name = "EnemyRange";
            go.transform.parent = GameObject.Find("Spawner").transform;
        }
        else if (!GameObject.Find("EnemyRange1"))
        {
            int rand = Random.Range(1, 4); // Return int between 1 and 3
            GameObject toSpawn;

            if (rand == 1)
                toSpawn = rangePrefab2;
            else if (rand == 2)
                toSpawn = range2Prefab2;
            else
                toSpawn = range3Prefab2;

            GameObject go = Instantiate(toSpawn, new Vector3(8.5f, -1, 0), Quaternion.identity) as GameObject;
            go.name = "EnemyRange1";
            go.transform.parent = GameObject.Find("Spawner").transform;
        }
        else if (!GameObject.Find("EnemyRange2"))
        {
            int rand = Random.Range(1, 4); // Return int between 1 and 3
            GameObject toSpawn;

            if (rand == 1)
                toSpawn = rangePrefab3;
            else if (rand == 2)
                toSpawn = range2Prefab3;
            else
                toSpawn = range3Prefab3;

            GameObject go = Instantiate(toSpawn, new Vector3(8.5f, -3.75f, 0), Quaternion.identity) as GameObject;
            go.name = "EnemyRange2";
            go.transform.parent = GameObject.Find("Spawner").transform;
        }
    }
    IEnumerator SpawnEnemy()
    {
        // Creating prefab as child
        yield return new WaitForSeconds(Random.Range(3, 10));

        if (!GameObject.Find("Enemy"))
        {
            int rand = Random.Range(1, 4); // Return int between 1 and 3
            GameObject toSpawn;

            if (rand == 1)
                toSpawn = enemyPrefab;
            else if (rand == 2)
                toSpawn = enemy2Prefab;
            else
                toSpawn = enemy3Prefab;

            GameObject go = Instantiate(toSpawn, new Vector3(6.25f, 0, 0), Quaternion.identity) as GameObject;
            go.name = "Enemy";
            go.transform.parent = GameObject.Find("Spawner").transform;
        }
        else if (!GameObject.Find("Enemy1"))
        {
            int rand = Random.Range(1, 4); // Return int between 1 and 3
            GameObject toSpawn;

            if (rand == 1)
                toSpawn = enemyPrefab2;
            else if (rand == 2)
                toSpawn = enemy2Prefab2;
            else
                toSpawn = enemy3Prefab2;

            GameObject go = Instantiate(toSpawn, new Vector3(7, -2.75f, 0), Quaternion.identity) as GameObject;
            go.name = "Enemy1";
            go.transform.parent = GameObject.Find("Spawner").transform;
        }
        else if (!GameObject.Find("Enemy2"))
        {
            int rand = Random.Range(1, 4); // Return int between 1 and 3
            GameObject toSpawn;

            if (rand == 1)
                toSpawn = enemyPrefab3;
            else if (rand == 2)
                toSpawn = enemy2Prefab3;
            else
                toSpawn = enemy3Prefab3;

            GameObject go = Instantiate(toSpawn, new Vector3(7, 3, 0), Quaternion.identity) as GameObject;
            go.name = "Enemy2";
            go.transform.parent = GameObject.Find("Spawner").transform;
        }
    }
}
