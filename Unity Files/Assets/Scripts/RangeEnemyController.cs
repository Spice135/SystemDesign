using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeEnemyController : MonoBehaviour
{
    public int health;
    private Text myText;
    private Text attackText;
    private GameObject player;
    private PlayerController pScript;
    private bool flag;
    private Text damageText;

    public GameObject healthObj;
    public GameObject attackObj;
    public GameObject damageObj;
    public GameObject spawner;

    void Start()
    {
        flag = false;
        player = GameObject.FindGameObjectWithTag("Player");
        damageText = damageObj.GetComponent<Text>();
        attackText = attackObj.GetComponent<Text>();
        pScript = player.GetComponent<PlayerController>();
        myText = healthObj.GetComponent<Text>();
        health = 100;
        spawner = GameObject.Find("Spawner");

        player.GetComponent<FileWriter>().NewEncounter(); // Start new encounter

        if(spawner.GetComponent<EnemySpawner>().openerDone == false)
            StartCoroutine(Opener());
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = "Health: " + health.ToString();
        if (!flag)
            StartCoroutine(AttackPlayer());
    }

    public int GetHealth()
    {
        return health;
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        StartCoroutine(DisplayDamage(damage));
    }

    IEnumerator Opener()
    {
        pScript.disable = true;
        pScript.DamagePlayer(2);
        StartCoroutine(DisplayAttack());
        yield return new WaitForSeconds(Random.Range(2, 5));
        pScript.disable = false;
        spawner.GetComponent<EnemySpawner>().openerDone = true;
    }

    // Attack once every 8 seconds
    IEnumerator AttackPlayer()
    {
        flag = true;
        yield return new WaitForSeconds(Random.Range(1, 9));
        pScript.DamagePlayer(2);
        StartCoroutine(DisplayAttack());
        flag = false;
    }

    IEnumerator DisplayDamage(int damage)
    {
        damageText.text = damage.ToString();
        yield return new WaitForSeconds(0.5f);
        damageText.text = "";
    }

    IEnumerator DisplayAttack()
    {
        attackText.text = "ARROW";
        yield return new WaitForSeconds(0.5f);
        attackText.text = "";
    }
}
