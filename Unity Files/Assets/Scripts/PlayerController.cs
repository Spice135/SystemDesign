using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject camera;
    public bool disable;
    public KeyCode restart;
    public KeyCode hit1;
    public KeyCode hit2;
    public KeyCode hit3;
    public KeyCode hit4;
    public float Stamina;
    public int Health;
    public Text healthText;
    public Text staminaText;
    public Text attackText;
    public Text blockText;
    public Text damageText;
    public GameObject scoreObj;
    public Text scoreText;
    public Text cooldown1;
    public Text cooldown2;
    public Text cooldown3;
    public bool isBlocked;
    public Text rewardText;
    public GameObject playerSprite;
    private float FireRate1 = 1f;
    private float FireRate2 = 3f;
    private float FireRate3 = 1f;
    private float NextFire1 = 0f;
    private float NextFire2 = 0f;
    private float NextFire3 = 0f;
    private int stamNeeded = 50;
    private Automation auto;
    public int score;

    private bool min1;
    private bool min2;
    private bool min3;
    private bool max1;
    private bool max2;
    private bool max3;
    private int damageModifier = 0;
    private float staminaModifier = 0;
    private float staminaCap = 100;
    private float cooldownModifier = 1;

    // Start is called before the first frame update
    void Start()
    {
        //disable = false;
        isBlocked = false;
        Health = 100; // Initialize player's health to 100
        Stamina = 100; // Inititalize player's stamina to 100
        auto = GetComponent<Automation>();

        scoreObj = GameObject.Find("PScore");
        scoreText = scoreObj.GetComponent<Text>();
        score = 0;

        min1 = false;
        min2 = false;
        min3 = false;
        max1 = false;
        max2 = false;
        max3 = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRewards();
        scoreText.text = "SCORE: " + score.ToString(); // Print score

        // Check if restart key was pressed
        if (Input.GetKeyDown(restart))
        {
            // Automation persists when restarting
            //GameManager manage = GetComponent<GameManager>();
            GameManager.automation = auto.GetAutomate();
            if (!GameManager.automation)
                GameManager.autoNumber = -1; // Set to none
            else
                GameManager.autoNumber = auto.GetNum();
            // Restart scene
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        if (Health < 1)
        {
            GameManager.automation = auto.GetAutomate();
            // Automation persists when restarting
            if (!GameManager.automation)
                GameManager.autoNumber = -1; // Set to none
            else
            {
                GameManager.autoNumber = auto.GetNum();
                GetComponent<FileWriter>().PlayerDeath();
            }

            StartCoroutine(Death());
            return;
        }

        int stamInt = (int)Stamina;
        healthText.text = "Health: " + Health.ToString();
        staminaText.text = "Stamina: " + stamInt.ToString();

        // Check if the player is attacking
        if(!auto.GetAutomate())
            CheckAttack();

        CheckCooldown();

        // Regen Stamina
        StaminaRegen();

    }

    IEnumerator Death()
    {
        Destroy(playerSprite); // Remove the player sprite
        healthText.text = "Health: 0";
        camera.GetComponent<CameraShake>().ShakeThisCamera();
        yield return new WaitForSeconds(3); // Shake camera
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex); // Restart scene
    }
       
    void StaminaRegen()
    {
        if(Stamina < staminaCap) // Cap stamina at 100
        {
            Stamina += (0.5f + staminaModifier);
        }
    }

    void CheckCooldown()
    {
        if (Time.time >= NextFire1 * cooldownModifier)
            cooldown1.text = "";
        else
            cooldown1.text = "COOLDOWN";

        if (Time.time >= NextFire2 * cooldownModifier)
            cooldown2.text = "";
        else
            cooldown2.text = "COOLDOWN";

        if (Time.time >= NextFire3 * cooldownModifier)
            cooldown3.text = "";
        else
            cooldown3.text = "COOLDOWN";
    }

    void CheckAttack()
    {
        if (disable)
            return;

        //GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        //GameObject enemy2 = GameObject.FindGameObjectWithTag("EnemyRange");

        GameObject[] enemies = new GameObject[3];
        enemies[0] = GameObject.Find("Enemy");
        enemies[1] = GameObject.Find("Enemy1");
        enemies[2] = GameObject.Find("Enemy2");
        //enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Get all enemies

        int health1 = 1000;
        int health2 = 1000;
        int health3 = 1000;

        if (enemies[0] != null || enemies[1] != null || enemies[2] != null)
        {
            if (enemies[0] != null)
            {
                if(enemies[0].tag == "Enemy")   
                    health1 = enemies[0].GetComponent<EnemyController>().health;
                else if (enemies[0].tag == "Enemy2")
                    health1 = enemies[0].GetComponent<Enemy2Controller>().health;
                else if (enemies[0].tag == "Enemy3")
                    health1 = enemies[0].GetComponent<Enemy3Controller>().health;
            }

            if (enemies[1] != null)
            {
                //health2 = enemies[1].GetComponent<EnemyController>().health;
                if (enemies[1].tag == "Enemy")
                    health2 = enemies[1].GetComponent<EnemyController>().health;
                else if (enemies[1].tag == "Enemy2")
                    health2 = enemies[1].GetComponent<Enemy2Controller>().health;
                else if (enemies[1].tag == "Enemy3")
                    health2 = enemies[1].GetComponent<Enemy3Controller>().health;
            }

            if (enemies[2] != null)
            {
                //health3 = enemies[2].GetComponent<EnemyController>().health;
                if (enemies[2].tag == "Enemy")
                    health3 = enemies[2].GetComponent<EnemyController>().health;
                else if (enemies[2].tag == "Enemy2")
                    health3 = enemies[2].GetComponent<Enemy2Controller>().health;
                else if (enemies[2].tag == "Enemy3")
                    health3 = enemies[2].GetComponent<Enemy3Controller>().health;
            }

            EnemyController controller = null;
            Enemy2Controller controller2 = null;
            Enemy3Controller controller3 = null;

            if (health1 < health2 && health1 < health3)
            {
                if (enemies[0].tag == "Enemy")
                    controller = enemies[0].GetComponent<EnemyController>();
                else if (enemies[0].tag == "Enemy2")
                    controller2 = enemies[0].GetComponent<Enemy2Controller>();
                else if (enemies[0].tag == "Enemy3")
                    controller3 = enemies[0].GetComponent<Enemy3Controller>();
            }
            else if (health2 < health1 && health2 < health3)
            {
                //controller = enemies[1].GetComponent<EnemyController>();
                if (enemies[1].tag == "Enemy")
                    controller = enemies[1].GetComponent<EnemyController>();
                else if (enemies[1].tag == "Enemy2")
                    controller2 = enemies[1].GetComponent<Enemy2Controller>();
                else if (enemies[1].tag == "Enemy3")
                    controller3 = enemies[1].GetComponent<Enemy3Controller>();
            }
            else if (health3 < health2 && health3 < health1)
            {
                //controller = enemies[2].GetComponent<EnemyController>();
                if (enemies[2].tag == "Enemy")
                    controller = enemies[2].GetComponent<EnemyController>();
                else if (enemies[2].tag == "Enemy2")
                    controller2 = enemies[2].GetComponent<Enemy2Controller>();
                else if (enemies[2].tag == "Enemy3")
                    controller3 = enemies[2].GetComponent<Enemy3Controller>();
            }
            else if (enemies[0] != null)
            {
                //controller = enemies[0].GetComponent<EnemyController>();
                if (enemies[0].tag == "Enemy")
                    controller = enemies[0].GetComponent<EnemyController>();
                else if (enemies[0].tag == "Enemy2")
                    controller2 = enemies[0].GetComponent<Enemy2Controller>();
                else if (enemies[0].tag == "Enemy3")
                    controller3 = enemies[0].GetComponent<Enemy3Controller>();
            }
            else if (enemies[1] != null)
            {
                //controller = enemies[1].GetComponent<EnemyController>();
                if (enemies[1].tag == "Enemy")
                    controller = enemies[1].GetComponent<EnemyController>();
                else if (enemies[1].tag == "Enemy2")
                    controller2 = enemies[1].GetComponent<Enemy2Controller>();
                else if (enemies[1].tag == "Enemy3")
                    controller3 = enemies[1].GetComponent<Enemy3Controller>();
            }
            else
            {
                //controller = enemies[2].GetComponent<EnemyController>();
                if (enemies[2].tag == "Enemy")
                    controller = enemies[2].GetComponent<EnemyController>();
                else if (enemies[2].tag == "Enemy2")
                    controller2 = enemies[2].GetComponent<Enemy2Controller>();
                else if (enemies[2].tag == "Enemy3")
                    controller3 = enemies[2].GetComponent<Enemy3Controller>();
            }

            if ((Input.GetKeyDown(hit1) && Stamina >= (stamNeeded / 10) && !isBlocked && Time.time >= NextFire1 * cooldownModifier))
            {
                Stamina -= (stamNeeded / 10);
                if (controller != null)
                    controller.DamageEnemy(5 + damageModifier);
                if (controller2 != null)
                    controller2.DamageEnemy(5 + damageModifier);
                if (controller3 != null)
                    controller3.DamageEnemy(5 + damageModifier);
                StartCoroutine(DisplayAttack("JAB"));
                NextFire1 = FireRate1 + Time.time;
                //Health -= 50;
            }

            if ((Input.GetKeyDown(hit2) && Stamina >= (stamNeeded / 2) && !isBlocked && Time.time >= NextFire2 * cooldownModifier))
            {
                Stamina -= (stamNeeded / 2);
                if (controller != null)
                    controller.DamageEnemy(15 + damageModifier);
                if (controller2 != null)
                    controller2.DamageEnemy(15 + damageModifier);
                if (controller3 != null)
                    controller3.DamageEnemy(15 + damageModifier);
                StartCoroutine(DisplayAttack("CROSS"));
                NextFire2 = FireRate2 + Time.time;
                //Health -= 50;
            }

            /*if ((Input.GetKeyDown(hit3) && Stamina >= stamNeeded && !isBlocked && Time.time >= NextFire3))
            {
                Stamina -= stamNeeded;
                if (controller != null)
                    controller.DamageEnemy(30);
                StartCoroutine(DisplayAttack("UPPERCUT"));
                NextFire3 = FireRate3 + Time.time;
                //Health -= 50;
            } */

            if (Input.GetKey(hit4))
            {
                isBlocked = true;
                blockText.text = "BLOCKED";
            }
            else
            {
                isBlocked = false;
                blockText.text = "";
            }
        }


        GameObject[] range_enemies = new GameObject[3];

        range_enemies[0] = GameObject.Find("EnemyRange");
        range_enemies[1] = GameObject.Find("EnemyRange1");
        range_enemies[2] = GameObject.Find("EnemyRange2");
        //range_enemies = GameObject.FindGameObjectsWithTag("EnemyRange"); // Get all ranged enemies

        int healthR1 = 1000;
        int healthR2 = 1000;
        int healthR3 = 1000;

        if (range_enemies[0] != null || range_enemies[1] != null || range_enemies[2] != null)
        {
            // If no enemies exist, Just return lol

            if (range_enemies[0] != null)
            {
                //healthR1 = range_enemies[0].GetComponent<RangeEnemyController>().health;
                if (range_enemies[0].tag == "EnemyRange")
                    healthR1 = range_enemies[0].GetComponent<RangeEnemyController>().health;
                else if (range_enemies[0].tag == "EnemyRange2")
                    healthR1 = range_enemies[0].GetComponent<RangeEnemy2Controller>().health;
                else if (range_enemies[0].tag == "EnemyRange3")
                    healthR1 = range_enemies[0].GetComponent<RangeEnemy3Controller>().health;
            }

            if (range_enemies[1] != null)
            {
                //healthR2 = range_enemies[1].GetComponent<RangeEnemyController>().health;
                if (range_enemies[1].tag == "EnemyRange")
                    healthR2 = range_enemies[1].GetComponent<RangeEnemyController>().health;
                else if (range_enemies[1].tag == "EnemyRange2")
                    healthR2 = range_enemies[1].GetComponent<RangeEnemy2Controller>().health;
                else if (range_enemies[1].tag == "EnemyRange3")
                    healthR2 = range_enemies[1].GetComponent<RangeEnemy3Controller>().health;
            }

            if (range_enemies[2] != null)
            {
                //healthR3 = range_enemies[2].GetComponent<RangeEnemyController>().health;
                if (range_enemies[2].tag == "EnemyRange")
                    healthR3 = range_enemies[2].GetComponent<RangeEnemyController>().health;
                else if (range_enemies[2].tag == "EnemyRange2")
                    healthR3 = range_enemies[2].GetComponent<RangeEnemy2Controller>().health;
                else if (range_enemies[2].tag == "EnemyRange3")
                    healthR3 = range_enemies[2].GetComponent<RangeEnemy3Controller>().health;
            }

            RangeEnemyController controllerR = null;
            RangeEnemy2Controller controllerR2 = null;
            RangeEnemy3Controller controllerR3 = null;

            if (healthR1 < healthR2 && healthR1 < healthR3)
            {
                if (range_enemies[0].tag == "EnemyRange")
                    controllerR = range_enemies[0].GetComponent<RangeEnemyController>();
                else if (range_enemies[0].tag == "EnemyRange2")
                    controllerR2 = range_enemies[0].GetComponent<RangeEnemy2Controller>();
                else if (range_enemies[0].tag == "EnemyRange3")
                    controllerR3 = range_enemies[0].GetComponent<RangeEnemy3Controller>();
            }
            else if (healthR2 < healthR1 && healthR2 < healthR3)
            {
                //controllerR = range_enemies[1].GetComponent<RangeEnemyController>();
                if (range_enemies[1].tag == "EnemyRange")
                    controllerR = range_enemies[1].GetComponent<RangeEnemyController>();
                else if (range_enemies[1].tag == "EnemyRange2")
                    controllerR2 = range_enemies[1].GetComponent<RangeEnemy2Controller>();
                else if (range_enemies[1].tag == "EnemyRange3")
                    controllerR3 = range_enemies[1].GetComponent<RangeEnemy3Controller>();
            }
            else if (healthR3 < healthR2 && healthR3 < healthR1)
            {
                //controllerR = range_enemies[2].GetComponent<RangeEnemyController>();
                if (range_enemies[2].tag == "EnemyRange")
                    controllerR = range_enemies[2].GetComponent<RangeEnemyController>();
                else if (range_enemies[2].tag == "EnemyRange2")
                    controllerR2 = range_enemies[2].GetComponent<RangeEnemy2Controller>();
                else if (range_enemies[2].tag == "EnemyRange3")
                    controllerR3 = range_enemies[2].GetComponent<RangeEnemy3Controller>();
            }
            else if (range_enemies[0] != null)
            {
                //controllerR = range_enemies[0].GetComponent<RangeEnemyController>();
                if (range_enemies[0].tag == "EnemyRange")
                    controllerR = range_enemies[0].GetComponent<RangeEnemyController>();
                else if (range_enemies[0].tag == "EnemyRange2")
                    controllerR2 = range_enemies[0].GetComponent<RangeEnemy2Controller>();
                else if (range_enemies[0].tag == "EnemyRange3")
                    controllerR3 = range_enemies[0].GetComponent<RangeEnemy3Controller>();
            }
            else if (range_enemies[1] != null)
            {
                //controllerR = range_enemies[1].GetComponent<RangeEnemyController>();
                if (range_enemies[1].tag == "EnemyRange")
                    controllerR = range_enemies[1].GetComponent<RangeEnemyController>();
                else if (range_enemies[1].tag == "EnemyRange2")
                    controllerR2 = range_enemies[1].GetComponent<RangeEnemy2Controller>();
                else if (range_enemies[1].tag == "EnemyRange3")
                    controllerR3 = range_enemies[1].GetComponent<RangeEnemy3Controller>();
            }
            else
            {
                //controllerR = range_enemies[2].GetComponent<RangeEnemyController>();
                if (range_enemies[2].tag == "EnemyRange")
                    controllerR = range_enemies[2].GetComponent<RangeEnemyController>();
                else if (range_enemies[2].tag == "EnemyRange2")
                    controllerR2 = range_enemies[2].GetComponent<RangeEnemy2Controller>();
                else if (range_enemies[2].tag == "EnemyRange3")
                    controllerR3 = range_enemies[2].GetComponent<RangeEnemy3Controller>();
            }

            if ((Input.GetKeyDown(hit3) && Stamina >= stamNeeded / 2 && !isBlocked && Time.time >= NextFire3 * cooldownModifier))
            {
                Stamina -= stamNeeded / 2;
                if(controllerR != null)
                    controllerR.DamageEnemy(30 + damageModifier);
                if (controllerR2 != null)
                    controllerR2.DamageEnemy(30 + damageModifier);
                if (controllerR3 != null)
                    controllerR3.DamageEnemy(30 + damageModifier);
                StartCoroutine(DisplayAttack("ARROW"));
                NextFire3 = FireRate3 + Time.time;
                //Health -= 50;
            }

            //RangeEnemyController controller = enemy2.GetComponent<RangeEnemyController>();

            /* if ((Input.GetKeyDown(hit1) && Stamina >= (stamNeeded / 10) && !isBlocked && Time.time >= NextFire1))
            {
                Stamina -= (stamNeeded / 10);
                controllerR.DamageEnemy(5);
                StartCoroutine(DisplayAttack("JAB"));
                NextFire1 = FireRate1 + Time.time;
                //Health -= 50;
            }

            if ((Input.GetKeyDown(hit2) && Stamina >= (stamNeeded / 2) && !isBlocked && Time.time >= NextFire2))
            {
                Stamina -= (stamNeeded / 2);
                controllerR.DamageEnemy(15);
                StartCoroutine(DisplayAttack("CROSS"));
                NextFire2 = FireRate2 + Time.time;
                //Health -= 50;
            } */



            if (Input.GetKey(hit4))
            {
                isBlocked = true;
                blockText.text = "BLOCKED";
            }
            else
            {
                isBlocked = false;
                blockText.text = "";
            }
            
        }
    }

    public void Attack1()
    {
        GameObject[] enemies = new GameObject[3];
        enemies[0] = GameObject.Find("Enemy");
        enemies[1] = GameObject.Find("Enemy1");
        enemies[2] = GameObject.Find("Enemy2");
        //enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Get all enemies

        int health1 = 1000;
        int health2 = 1000;
        int health3 = 1000;

        if (enemies[0] != null || enemies[1] != null || enemies[2] != null)
        {
            if (enemies[0] != null)
            {
                if (enemies[0].tag == "Enemy")
                    health1 = enemies[0].GetComponent<EnemyController>().health;
                else if (enemies[0].tag == "Enemy2")
                    health1 = enemies[0].GetComponent<Enemy2Controller>().health;
                else if (enemies[0].tag == "Enemy3")
                    health1 = enemies[0].GetComponent<Enemy3Controller>().health;
            }

            if (enemies[1] != null)
            {
                //health2 = enemies[1].GetComponent<EnemyController>().health;
                if (enemies[1].tag == "Enemy")
                    health2 = enemies[1].GetComponent<EnemyController>().health;
                else if (enemies[1].tag == "Enemy2")
                    health2 = enemies[1].GetComponent<Enemy2Controller>().health;
                else if (enemies[1].tag == "Enemy3")
                    health2 = enemies[1].GetComponent<Enemy3Controller>().health;
            }

            if (enemies[2] != null)
            {
                //health3 = enemies[2].GetComponent<EnemyController>().health;
                if (enemies[2].tag == "Enemy")
                    health3 = enemies[2].GetComponent<EnemyController>().health;
                else if (enemies[2].tag == "Enemy2")
                    health3 = enemies[2].GetComponent<Enemy2Controller>().health;
                else if (enemies[2].tag == "Enemy3")
                    health3 = enemies[2].GetComponent<Enemy3Controller>().health;
            }

            EnemyController controller = null;
            Enemy2Controller controller2 = null;
            Enemy3Controller controller3 = null;

            if (health1 < health2 && health1 < health3)
            {
                if (enemies[0].tag == "Enemy")
                    controller = enemies[0].GetComponent<EnemyController>();
                else if (enemies[0].tag == "Enemy2")
                    controller2 = enemies[0].GetComponent<Enemy2Controller>();
                else if (enemies[0].tag == "Enemy3")
                    controller3 = enemies[0].GetComponent<Enemy3Controller>();
            }
            else if (health2 < health1 && health2 < health3)
            {
                //controller = enemies[1].GetComponent<EnemyController>();
                if (enemies[1].tag == "Enemy")
                    controller = enemies[1].GetComponent<EnemyController>();
                else if (enemies[1].tag == "Enemy2")
                    controller2 = enemies[1].GetComponent<Enemy2Controller>();
                else if (enemies[1].tag == "Enemy3")
                    controller3 = enemies[1].GetComponent<Enemy3Controller>();
            }
            else if (health3 < health2 && health3 < health1)
            {
                //controller = enemies[2].GetComponent<EnemyController>();
                if (enemies[2].tag == "Enemy")
                    controller = enemies[2].GetComponent<EnemyController>();
                else if (enemies[2].tag == "Enemy2")
                    controller2 = enemies[2].GetComponent<Enemy2Controller>();
                else if (enemies[2].tag == "Enemy3")
                    controller3 = enemies[2].GetComponent<Enemy3Controller>();
            }
            else if (enemies[0] != null)
            {
                //controller = enemies[0].GetComponent<EnemyController>();
                if (enemies[0].tag == "Enemy")
                    controller = enemies[0].GetComponent<EnemyController>();
                else if (enemies[0].tag == "Enemy2")
                    controller2 = enemies[0].GetComponent<Enemy2Controller>();
                else if (enemies[0].tag == "Enemy3")
                    controller3 = enemies[0].GetComponent<Enemy3Controller>();
            }
            else if (enemies[1] != null)
            {
                //controller = enemies[1].GetComponent<EnemyController>();
                if (enemies[1].tag == "Enemy")
                    controller = enemies[1].GetComponent<EnemyController>();
                else if (enemies[1].tag == "Enemy2")
                    controller2 = enemies[1].GetComponent<Enemy2Controller>();
                else if (enemies[1].tag == "Enemy3")
                    controller3 = enemies[1].GetComponent<Enemy3Controller>();
            }
            else
            {
                //controller = enemies[2].GetComponent<EnemyController>();
                if (enemies[2].tag == "Enemy")
                    controller = enemies[2].GetComponent<EnemyController>();
                else if (enemies[2].tag == "Enemy2")
                    controller2 = enemies[2].GetComponent<Enemy2Controller>();
                else if (enemies[2].tag == "Enemy3")
                    controller3 = enemies[2].GetComponent<Enemy3Controller>();
            }

            if ((Stamina >= (stamNeeded / 10) && !isBlocked && Time.time >= NextFire1 * cooldownModifier))
            {
                Stamina -= (stamNeeded / 10);
                if (controller != null)
                    controller.DamageEnemy(5 + damageModifier);
                if (controller2 != null)
                    controller2.DamageEnemy(5 + damageModifier);
                if (controller3 != null)
                    controller3.DamageEnemy(5 + damageModifier);
                StartCoroutine(DisplayAttack("JAB"));
                NextFire1 = FireRate1 + Time.time;
                //Health -= 50;
            }
        }
    }

    public void Attack2()
    {
        GameObject[] enemies = new GameObject[3];
        enemies[0] = GameObject.Find("Enemy");
        enemies[1] = GameObject.Find("Enemy1");
        enemies[2] = GameObject.Find("Enemy2");
        //enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Get all enemies

        int health1 = 1000;
        int health2 = 1000;
        int health3 = 1000;

        if (enemies[0] != null || enemies[1] != null || enemies[2] != null)
        {
            if (enemies[0] != null)
            {
                if (enemies[0].tag == "Enemy")
                    health1 = enemies[0].GetComponent<EnemyController>().health;
                else if (enemies[0].tag == "Enemy2")
                    health1 = enemies[0].GetComponent<Enemy2Controller>().health;
                else if (enemies[0].tag == "Enemy3")
                    health1 = enemies[0].GetComponent<Enemy3Controller>().health;
            }

            if (enemies[1] != null)
            {
                //health2 = enemies[1].GetComponent<EnemyController>().health;
                if (enemies[1].tag == "Enemy")
                    health2 = enemies[1].GetComponent<EnemyController>().health;
                else if (enemies[1].tag == "Enemy2")
                    health2 = enemies[1].GetComponent<Enemy2Controller>().health;
                else if (enemies[1].tag == "Enemy3")
                    health2 = enemies[1].GetComponent<Enemy3Controller>().health;
            }

            if (enemies[2] != null)
            {
                //health3 = enemies[2].GetComponent<EnemyController>().health;
                if (enemies[2].tag == "Enemy")
                    health3 = enemies[2].GetComponent<EnemyController>().health;
                else if (enemies[2].tag == "Enemy2")
                    health3 = enemies[2].GetComponent<Enemy2Controller>().health;
                else if (enemies[2].tag == "Enemy3")
                    health3 = enemies[2].GetComponent<Enemy3Controller>().health;
            }

            EnemyController controller = null;
            Enemy2Controller controller2 = null;
            Enemy3Controller controller3 = null;

            if (health1 < health2 && health1 < health3)
            {
                if (enemies[0].tag == "Enemy")
                    controller = enemies[0].GetComponent<EnemyController>();
                else if (enemies[0].tag == "Enemy2")
                    controller2 = enemies[0].GetComponent<Enemy2Controller>();
                else if (enemies[0].tag == "Enemy3")
                    controller3 = enemies[0].GetComponent<Enemy3Controller>();
            }
            else if (health2 < health1 && health2 < health3)
            {
                //controller = enemies[1].GetComponent<EnemyController>();
                if (enemies[1].tag == "Enemy")
                    controller = enemies[1].GetComponent<EnemyController>();
                else if (enemies[1].tag == "Enemy2")
                    controller2 = enemies[1].GetComponent<Enemy2Controller>();
                else if (enemies[1].tag == "Enemy3")
                    controller3 = enemies[1].GetComponent<Enemy3Controller>();
            }
            else if (health3 < health2 && health3 < health1)
            {
                //controller = enemies[2].GetComponent<EnemyController>();
                if (enemies[2].tag == "Enemy")
                    controller = enemies[2].GetComponent<EnemyController>();
                else if (enemies[2].tag == "Enemy2")
                    controller2 = enemies[2].GetComponent<Enemy2Controller>();
                else if (enemies[2].tag == "Enemy3")
                    controller3 = enemies[2].GetComponent<Enemy3Controller>();
            }
            else if (enemies[0] != null)
            {
                //controller = enemies[0].GetComponent<EnemyController>();
                if (enemies[0].tag == "Enemy")
                    controller = enemies[0].GetComponent<EnemyController>();
                else if (enemies[0].tag == "Enemy2")
                    controller2 = enemies[0].GetComponent<Enemy2Controller>();
                else if (enemies[0].tag == "Enemy3")
                    controller3 = enemies[0].GetComponent<Enemy3Controller>();
            }
            else if (enemies[1] != null)
            {
                //controller = enemies[1].GetComponent<EnemyController>();
                if (enemies[1].tag == "Enemy")
                    controller = enemies[1].GetComponent<EnemyController>();
                else if (enemies[1].tag == "Enemy2")
                    controller2 = enemies[1].GetComponent<Enemy2Controller>();
                else if (enemies[1].tag == "Enemy3")
                    controller3 = enemies[1].GetComponent<Enemy3Controller>();
            }
            else
            {
                //controller = enemies[2].GetComponent<EnemyController>();
                if (enemies[2].tag == "Enemy")
                    controller = enemies[2].GetComponent<EnemyController>();
                else if (enemies[2].tag == "Enemy2")
                    controller2 = enemies[2].GetComponent<Enemy2Controller>();
                else if (enemies[2].tag == "Enemy3")
                    controller3 = enemies[2].GetComponent<Enemy3Controller>();
            }

            if ((Stamina >= (stamNeeded / 2) && !isBlocked && Time.time >= NextFire2 * cooldownModifier))
            {
                Stamina -= (stamNeeded / 2);
                if (controller != null)
                    controller.DamageEnemy(15 + damageModifier);
                if (controller2 != null)
                    controller2.DamageEnemy(15 + damageModifier);
                if (controller3 != null)
                    controller3.DamageEnemy(15 + damageModifier);
                StartCoroutine(DisplayAttack("CROSS"));
                NextFire2 = FireRate2 + Time.time;
                //Health -= 50;
            }
        }
    }

    public void Attack3()
    {
        GameObject[] range_enemies = new GameObject[3];

        range_enemies[0] = GameObject.Find("EnemyRange");
        range_enemies[1] = GameObject.Find("EnemyRange1");
        range_enemies[2] = GameObject.Find("EnemyRange2");
        //range_enemies = GameObject.FindGameObjectsWithTag("EnemyRange"); // Get all ranged enemies

        int healthR1 = 1000;
        int healthR2 = 1000;
        int healthR3 = 1000;

        if (range_enemies[0] != null || range_enemies[1] != null || range_enemies[2] != null)
        {
            // If no enemies exist, Just return lol

            if (range_enemies[0] != null)
            {
                //healthR1 = range_enemies[0].GetComponent<RangeEnemyController>().health;
                if (range_enemies[0].tag == "EnemyRange")
                    healthR1 = range_enemies[0].GetComponent<RangeEnemyController>().health;
                else if (range_enemies[0].tag == "EnemyRange2")
                    healthR1 = range_enemies[0].GetComponent<RangeEnemy2Controller>().health;
                else if (range_enemies[0].tag == "EnemyRange3")
                    healthR1 = range_enemies[0].GetComponent<RangeEnemy3Controller>().health;
            }

            if (range_enemies[1] != null)
            {
                //healthR2 = range_enemies[1].GetComponent<RangeEnemyController>().health;
                if (range_enemies[1].tag == "EnemyRange")
                    healthR2 = range_enemies[1].GetComponent<RangeEnemyController>().health;
                else if (range_enemies[1].tag == "EnemyRange2")
                    healthR2 = range_enemies[1].GetComponent<RangeEnemy2Controller>().health;
                else if (range_enemies[1].tag == "EnemyRange3")
                    healthR2 = range_enemies[1].GetComponent<RangeEnemy3Controller>().health;
            }

            if (range_enemies[2] != null)
            {
                //healthR3 = range_enemies[2].GetComponent<RangeEnemyController>().health;
                if (range_enemies[2].tag == "EnemyRange")
                    healthR3 = range_enemies[2].GetComponent<RangeEnemyController>().health;
                else if (range_enemies[2].tag == "EnemyRange2")
                    healthR3 = range_enemies[2].GetComponent<RangeEnemy2Controller>().health;
                else if (range_enemies[2].tag == "EnemyRange3")
                    healthR3 = range_enemies[2].GetComponent<RangeEnemy3Controller>().health;
            }

            RangeEnemyController controllerR = null;
            RangeEnemy2Controller controllerR2 = null;
            RangeEnemy3Controller controllerR3 = null;

            if (healthR1 < healthR2 && healthR1 < healthR3)
            {
                if (range_enemies[0].tag == "EnemyRange")
                    controllerR = range_enemies[0].GetComponent<RangeEnemyController>();
                else if (range_enemies[0].tag == "EnemyRange2")
                    controllerR2 = range_enemies[0].GetComponent<RangeEnemy2Controller>();
                else if (range_enemies[0].tag == "EnemyRange3")
                    controllerR3 = range_enemies[0].GetComponent<RangeEnemy3Controller>();
            }
            else if (healthR2 < healthR1 && healthR2 < healthR3)
            {
                //controllerR = range_enemies[1].GetComponent<RangeEnemyController>();
                if (range_enemies[1].tag == "EnemyRange")
                    controllerR = range_enemies[1].GetComponent<RangeEnemyController>();
                else if (range_enemies[1].tag == "EnemyRange2")
                    controllerR2 = range_enemies[1].GetComponent<RangeEnemy2Controller>();
                else if (range_enemies[1].tag == "EnemyRange3")
                    controllerR3 = range_enemies[1].GetComponent<RangeEnemy3Controller>();
            }
            else if (healthR3 < healthR2 && healthR3 < healthR1)
            {
                //controllerR = range_enemies[2].GetComponent<RangeEnemyController>();
                if (range_enemies[2].tag == "EnemyRange")
                    controllerR = range_enemies[2].GetComponent<RangeEnemyController>();
                else if (range_enemies[2].tag == "EnemyRange2")
                    controllerR2 = range_enemies[2].GetComponent<RangeEnemy2Controller>();
                else if (range_enemies[2].tag == "EnemyRange3")
                    controllerR3 = range_enemies[2].GetComponent<RangeEnemy3Controller>();
            }
            else if (range_enemies[0] != null)
            {
                //controllerR = range_enemies[0].GetComponent<RangeEnemyController>();
                if (range_enemies[0].tag == "EnemyRange")
                    controllerR = range_enemies[0].GetComponent<RangeEnemyController>();
                else if (range_enemies[0].tag == "EnemyRange2")
                    controllerR2 = range_enemies[0].GetComponent<RangeEnemy2Controller>();
                else if (range_enemies[0].tag == "EnemyRange3")
                    controllerR3 = range_enemies[0].GetComponent<RangeEnemy3Controller>();
            }
            else if (range_enemies[1] != null)
            {
                //controllerR = range_enemies[1].GetComponent<RangeEnemyController>();
                if (range_enemies[1].tag == "EnemyRange")
                    controllerR = range_enemies[1].GetComponent<RangeEnemyController>();
                else if (range_enemies[1].tag == "EnemyRange2")
                    controllerR2 = range_enemies[1].GetComponent<RangeEnemy2Controller>();
                else if (range_enemies[1].tag == "EnemyRange3")
                    controllerR3 = range_enemies[1].GetComponent<RangeEnemy3Controller>();
            }
            else
            {
                //controllerR = range_enemies[2].GetComponent<RangeEnemyController>();
                if (range_enemies[2].tag == "EnemyRange")
                    controllerR = range_enemies[2].GetComponent<RangeEnemyController>();
                else if (range_enemies[2].tag == "EnemyRange2")
                    controllerR2 = range_enemies[2].GetComponent<RangeEnemy2Controller>();
                else if (range_enemies[2].tag == "EnemyRange3")
                    controllerR3 = range_enemies[2].GetComponent<RangeEnemy3Controller>();
            }

            if ((Stamina >= stamNeeded / 2 && !isBlocked && Time.time >= NextFire3 * cooldownModifier))
            {
                Stamina -= stamNeeded / 2;
                if (controllerR != null)
                    controllerR.DamageEnemy(30 + damageModifier);
                if (controllerR2 != null)
                    controllerR2.DamageEnemy(30 + damageModifier);
                if (controllerR3 != null)
                    controllerR3.DamageEnemy(30 + damageModifier);
                StartCoroutine(DisplayAttack("ARROW"));
                NextFire3 = FireRate3 + Time.time;
                //Health -= 50;
            }
        }
    }

    public void Attack4()
    {
        isBlocked = true;
        blockText.text = "BLOCKED";
    }

    public void Release4()
    {
        isBlocked = false;
        blockText.text = "";
    }

    public void DamagePlayer(int damage)
    {
        if (!isBlocked)
        {
            Health -= damage;
            StartCoroutine(DisplayDamage(damage));
        }
        else
        {
            Health -= (damage / 2);
            StartCoroutine(DisplayDamage(damage / 2));
        }

        if (Health < 0)
            Health = 0;
    }

    IEnumerator DisplayAttack(string label)
    {
        attackText.text = label;
        yield return new WaitForSeconds(0.5f);
        attackText.text = "";
    }

    IEnumerator DisplayDamage(int damage)
    {
        damageText.text = damage.ToString();
        yield return new WaitForSeconds(0.5f);
        damageText.text = "";
    }

    public bool Check1()
    {
        if (Stamina >= (stamNeeded / 10) && Time.time >= NextFire1 * cooldownModifier)
            return true;
        else
            return false;
    }

    public bool Check2()
    {
        if ((Stamina >= (stamNeeded / 2) && Time.time >= NextFire2 * cooldownModifier))
            return true;
        else
            return false;
    }

    public bool Check3()
    {
        if ((Stamina >= stamNeeded / 2 && Time.time >= NextFire3 * cooldownModifier))
            return true;
        else
            return false;
    }

    void CheckRewards()
    {
        if(score == 3 && !min1) // Minor reward 1
        {
            rewardText.text = "New Reward: Health +20";
            Health += 20;
            min1 = true;
        }

        if(score == 9 && !min2) // Minor reward 2
        {
            rewardText.text = "New Reward: Damage +5";
            damageModifier = 5;
            min2 = true;
        }

        if(score == 27 && !min3) // Minor reward 3
        {
            rewardText.text = "New Reward: Stamina Recover x2";
            min3 = true;
            staminaModifier = 0.5f;
        }

        if(score == 10 && !max1) // Major reward 1
        {
            rewardText.text = "New Reward: Stamina x2";
            staminaCap = 200;
            max1 = true;
        }

        if (score == 20 && !max2) // Major reward 2
        {
            rewardText.text = "New Reward: Cooldown 1/2";
            cooldownModifier = 0.5f;
            max2 = true;
        }

        if (score == 30 && !max3) // Major reward 3
        {
            rewardText.text = "New Reward: Damage +15";
            damageModifier = 15;
            max3 = true;
        }

    }
}
