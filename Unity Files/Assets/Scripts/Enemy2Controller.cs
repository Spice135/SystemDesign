using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2Controller : MonoBehaviour
{
    public int health;
    private Text myText;
    private Text attackText;
    private GameObject player;
    private PlayerController pScript;
    private bool flag;
    private Text damageText;

    public GameObject healthObj;
    public GameObject damageObj;
    public GameObject attackObj;

    // Start is called before the first frame update
    void Start()
    {
        flag = false;
        player = GameObject.FindGameObjectWithTag("Player");
        damageText = damageObj.GetComponent<Text>();
        attackText = attackObj.GetComponent<Text>();
        pScript = player.GetComponent<PlayerController>();
        myText = healthObj.GetComponent<Text>();
        health = 100;
        
        player.GetComponent<FileWriter>().NewEncounter(); // Start new encounter
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = "Health: " + health.ToString();
        if (!flag && pScript.disable == false)
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

    // Attack once every 5 seconds
    IEnumerator AttackPlayer()
    {
        flag = true;
        yield return new WaitForSeconds(Random.Range(3, 8));
        pScript.DamagePlayer(4);
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
        attackText.text = "HOOK";
        yield return new WaitForSeconds(0.5f);
        attackText.text = "";
    }
}
