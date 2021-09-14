using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automation : MonoBehaviour
{
    private PlayerController playerScript;
    public bool automate;
    public bool showOff;
    public bool random1;
    public bool random2;
    public bool random3;
    public bool random4;
    public bool random5;
    public bool random6;
    public bool random7;

    // Start is called before the first frame update
    void Start()
    {
        automate = false;
        showOff = false;

        random1 = false;
        random2 = false;
        random3 = false;
        random4 = false;
        random5 = false;
        random6 = false;
        random7 = false;

        //GameManager manage = GetComponent<GameManager>();
        automate = GameManager.automation;
        int thing = GameManager.autoNumber;

        if (thing == 0)
            showOff = true;
        else if (thing == 1)
            random1 = true;
        else if (thing == 2)
            random2 = true;
        else if (thing == 3)
            random3 = true;
        else if (thing == 4)
            random4 = true;
        else if (thing == 5)
            random5 = true;
        else if (thing == 6)
            random6 = true;
        else if (thing == 7)
            random7 = true;

        playerScript = GetComponent<PlayerController>(); // Get the player controller script
        // One optimal mode
        // 3-5 random patterns (random, spam 1, spam 2, spam 3, spam 4)
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            automate = !automate;

        if (Input.GetKeyDown(KeyCode.W))
            showOff = !showOff;

        if (Input.GetKeyDown(KeyCode.A))
        {
            SetAllFalse();
            random1 = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetAllFalse();
            random2 = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetAllFalse();
            random3 = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            SetAllFalse();
            random4 = true;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            SetAllFalse();
            random5 = true;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            SetAllFalse();
            random6 = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            SetAllFalse();
            random7 = true;
        }

        if (automate)
        {
            if (showOff)
            {
                Optimal();
                Time.timeScale = 1; // Real time
            }
            else
            {
                Time.timeScale = 100; // 100x the time
                if (random1)
                    Spam1();
                else if (random2)
                    Spam2();
                else if (random3)
                    Spam3();
                else if (random4)
                    Spam4();
                else if (random5)
                    Spam5();
                else if (random6)
                    Spam6();
                else if (random7)
                    Spam7();
            }
        }
        else
            Time.timeScale = 1; // Real time
    }

    public int GetNum()
    {
        if (showOff)
            return 0;
        else if (random1)
            return 1;
        else if (random2)
            return 2;
        else if (random3)
            return 3;
        else if (random4)
            return 4;
        else if (random5)
            return 5;
        else if (random6)
            return 6;
        else if (random7)
            return 7;

        return 0;
    }

    void SetAllFalse()
    {
        random1 = false;
        random2 = false;
        random3 = false;
        random4 = false;
        random5 = false;
        random6 = false;
        random7 = false;
    }

    public bool GetAutomate()
    {
        return automate;
    }

    void Optimal()
    {
        playerScript.Attack4(); // Always block

        if(playerScript.Check3())
        {
            playerScript.Release4(); // Release block
            playerScript.Attack3(); // Heaviest attack first
            playerScript.Attack4(); // Block again
            return;
        }

        if (playerScript.Check2())
        {
            playerScript.Release4(); // Release block
            playerScript.Attack2(); // Second attack
            playerScript.Attack4(); // Block again
            return;
        }

        if (playerScript.Check1())
        {
            playerScript.Release4(); // Release block
            playerScript.Attack1(); // Lightest attack last
            playerScript.Attack4(); // Block again
            return;
        }
    }

    void Spam1()
    {
        playerScript.Attack1();
    }

    void Spam2()
    {
        playerScript.Attack2();
    }

    void Spam3()
    {
        playerScript.Attack3();
    }

    void Spam4() // Pattern 1
    {
        playerScript.Attack1(); // Attack 1, then 2, then 3
        playerScript.Attack2();
        playerScript.Attack3();
    }

    void Spam5() // Pattern 2
    {
        playerScript.Attack1(); // Attack 1, then 3, then 2
        playerScript.Attack3();
        playerScript.Attack2();
    }

    void Spam6() // Pattern 3
    {
        playerScript.Attack2(); // Attack 2, then 3, then 1
        playerScript.Attack3();
        playerScript.Attack1();
    }

    void Spam7()
    {
        // Random
        int rand = Random.Range(1, 5);

        if (rand == 4)
            playerScript.Attack4();
        else
            playerScript.Release4();

        if (rand == 1)
            playerScript.Attack1();
        if (rand == 2)
            playerScript.Attack2();
        if (rand == 3)
            playerScript.Attack3();
    }
}
