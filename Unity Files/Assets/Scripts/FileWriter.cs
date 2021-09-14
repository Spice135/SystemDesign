using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileWriter : MonoBehaviour
{
    // Max DPS, Min DPS and Average DPS
    float dps;
    int startHealth;
    int enemyHealth;
    int num;
    string PathOptimum = "Files/Optimum.csv";
    string PathSpam1 = "Files/Spam1.csv";
    string PathSpam2 = "Files/Spam2.csv";
    string PathSpam3 = "Files/Spam3.csv";
    string PathSpam4 = "Files/Pattern1.csv";
    string PathSpam5 = "Files/Pattern2.csv";
    string PathSpam6 = "Files/Pattern3.csv";
    string PathRandom1 = "Files/Random.csv";

    string optimum = "";
    string spam1 = "";
    string spam2 = "";
    string spam3 = "";
    string spam4 = "";
    string spam5 = "";
    string spam6 = "";
    string random = "";

    static StreamWriter WriteOptimum;
    static StreamWriter WriteSpam1;
    static StreamWriter WriteSpam2;
    static StreamWriter WriteSpam3;
    static StreamWriter WriteSpam4;
    static StreamWriter WriteSpam5;
    static StreamWriter WriteSpam6;
    static StreamWriter WriteRandom;

    // Start is called before the first frame update
    void Start()
    {
        WriteOptimum = new StreamWriter(PathOptimum, true);
        WriteSpam1 = new StreamWriter(PathSpam1, true);
        WriteSpam2 = new StreamWriter(PathSpam2, true);
        WriteSpam3 = new StreamWriter(PathSpam3, true);
        WriteSpam4 = new StreamWriter(PathSpam4, true);
        WriteSpam5 = new StreamWriter(PathSpam5, true);
        WriteSpam6 = new StreamWriter(PathSpam6, true);
        WriteRandom = new StreamWriter(PathRandom1, true);

        WriteOptimum.Write("Encounter Start, Encounter End, Damage Done, Damage Taken, Result, DPS \n");
        WriteSpam1.Write("Encounter Start, Encounter End, Damage Done, Damage Taken, Result, DPS \n");
        WriteSpam2.Write("Encounter Start, Encounter End, Damage Done, Damage Taken, Result, DPS \n");
        WriteSpam3.Write("Encounter Start, Encounter End, Damage Done, Damage Taken, Result, DPS \n");
        WriteSpam4.Write("Encounter Start, Encounter End, Damage Done, Damage Taken, Result, DPS \n");
        WriteSpam5.Write("Encounter Start, Encounter End, Damage Done, Damage Taken, Result, DPS \n");
        WriteSpam6.Write("Encounter Start, Encounter End, Damage Done, Damage Taken, Result, DPS \n");
        WriteRandom.Write("Encounter Start, Encounter End, Damage Done, Damage Taken, Result, DPS \n");
        startHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        num = GetComponent<Automation>().GetNum();
    }

    public void NewEncounter()
    {
        PlayerController script = GetComponent<PlayerController>();
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        GameObject enemy2 = GameObject.FindGameObjectWithTag("EnemyRange");
        RangeEnemyController enScript2 = enemy2.GetComponent<RangeEnemyController>();
        EnemyController enScript = enemy.GetComponent<EnemyController>();
        startHealth = script.Health;
        if (startHealth <= 0)
            startHealth = 100;
        enemyHealth = enScript.health + enScript2.health;

        dps = Time.time;

        if (num == 0)
            optimum = Time.time.ToString();
        else if (num == 1)
            spam1 = Time.time.ToString();
        else if (num == 2)
            spam2 = Time.time.ToString();
        else if (num == 3)
            spam3 = Time.time.ToString();
        else if (num == 4)
            spam4 = Time.time.ToString();
        else if (num == 5)
            spam5 = Time.time.ToString();
        else if (num == 6)
            spam6 = Time.time.ToString();
        else if (num == 7)
            random = Time.time.ToString();

        //FinalWrite();
    }

    public void EndEncounter()
    {
        dps = 100 / (Time.time - dps);
        PlayerController script = GetComponent<PlayerController>();
        if (num == 0)
            optimum = optimum + "," + Time.time.ToString() + ", 100," + (startHealth - script.Health).ToString() + ", WIN," + dps + "\n";
        else if (num == 1)
            spam1 = spam1 + "," + Time.time.ToString() + ", 100," + (startHealth - script.Health).ToString() + ", WIN," + dps + "\n";
        else if (num == 2)
            spam2 = spam2 + "," + Time.time.ToString() + ", 100," + (startHealth - script.Health).ToString() + ", WIN," + dps + "\n";
        else if (num == 3)
            spam3 = spam3 + "," + Time.time.ToString() + ", 100," + (startHealth - script.Health).ToString() + ", WIN," + dps + "\n";
        else if (num == 4)
            spam4 = spam4 + "," + Time.time.ToString() + ", 100," + (startHealth - script.Health).ToString() + ", WIN," + dps + "\n";
        else if (num == 5)
            spam5 = spam5 + "," + Time.time.ToString() + ", 100," + (startHealth - script.Health).ToString() + ", WIN," + dps + "\n";
        else if (num == 6)
            spam6 = spam6 + "," + Time.time.ToString() + ", 100," + (startHealth - script.Health).ToString() + ", WIN," + dps + "\n";
        else if (num == 7)
            random = random + "," + Time.time.ToString() + ", 100," + (startHealth - script.Health).ToString() + ", WIN," + dps + "\n";

        FinalWrite(); // Write line to file
    }

    public void PlayerDeath()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        GameObject enemy2 = GameObject.FindGameObjectWithTag("EnemyRange");
        RangeEnemyController enScript2 = enemy2.GetComponent<RangeEnemyController>();
        EnemyController enScript = enemy.GetComponent<EnemyController>();
        int endEnemyHealth = enScript.health + enScript2.health;
        PlayerController script = GetComponent<PlayerController>();
        if (num == 0)
            optimum = optimum + "," + Time.time.ToString() + "," + (enemyHealth - endEnemyHealth).ToString() + "," + (startHealth - script.Health).ToString() + ", LOSS\n";
        else if (num == 1)
            spam1 = spam1 + "," + Time.time.ToString() + "," + (enemyHealth - endEnemyHealth).ToString() + "," + (startHealth - script.Health).ToString() + ", LOSS\n";
        else if (num == 2)
            spam2 = spam2 + "," + Time.time.ToString() + "," + (enemyHealth - endEnemyHealth).ToString() + "," + (startHealth - script.Health).ToString() + ", LOSS\n";
        else if (num == 3)
            spam3 = spam3 + "," + Time.time.ToString() + "," + (enemyHealth - endEnemyHealth).ToString() + "," + (startHealth - script.Health).ToString() + ", LOSS\n";
        else if (num == 4)
            spam4 = spam4 + "," + Time.time.ToString() + "," + (enemyHealth - endEnemyHealth).ToString() + "," + (startHealth - script.Health).ToString() + ", LOSS\n";
        else if (num == 5)
            spam5 = spam5 + "," + Time.time.ToString() + "," + (enemyHealth - endEnemyHealth).ToString() + "," + (startHealth - script.Health).ToString() + ", LOSS\n";
        else if (num == 6)
            spam6 = spam6 + "," + Time.time.ToString() + "," + (enemyHealth - endEnemyHealth).ToString() + "," + (startHealth - script.Health).ToString() + ", LOSS\n";
        else if (num == 7)
            random = random + "," + Time.time.ToString() + "," + (enemyHealth - endEnemyHealth).ToString() + "," + (startHealth - script.Health).ToString() + ", LOSS\n";

        FinalWrite(); // Write line to file
        //CloseFiles(); // Close files before restarting
    }

    void FinalWrite()
    {
        if(WriteOptimum != null)
            WriteOptimum.Write(optimum);

        if(WriteSpam1 != null)
            WriteSpam1.Write(spam1);

        if (WriteSpam2 != null)
            WriteSpam2.Write(spam2);

        if (WriteSpam3 != null)
            WriteSpam3.Write(spam3);

        if (WriteSpam4 != null)
            WriteSpam4.Write(spam4);

        if (WriteSpam5 != null)
            WriteSpam5.Write(spam5);

        if (WriteSpam6 != null)
            WriteSpam6.Write(spam6);

        if (WriteRandom != null)
            WriteRandom.Write(random);

        optimum = "";
        spam1 = "";
        spam2 = "";
        spam3 = "";
        spam4 = "";
        spam5 = "";
        spam6 = "";
        random = "";
    }

    void CloseFiles()
    {
        if (WriteOptimum != null)
            WriteOptimum.Close();

        if (WriteSpam1 != null)
            WriteSpam1.Close();

        if (WriteSpam2 != null)
            WriteSpam2.Close();

        if (WriteSpam3 != null)
            WriteSpam3.Close();

        if (WriteSpam4 != null)
            WriteSpam4.Close();

        if (WriteSpam5 != null)
            WriteSpam5.Close();

        if (WriteSpam6 != null)
            WriteSpam6.Close();

        if (WriteRandom != null)
            WriteRandom.Close();

        WriteOptimum = null;
        WriteSpam1 = null;
        WriteSpam2 = null;
        WriteSpam3 = null;
        WriteSpam4 = null;
        WriteSpam5 = null;
        WriteSpam6 = null;
        WriteRandom = null;
    }

    /* void MinMax()
    {
        if (num == 0)
            optimum = "\n,MAX DPS,=MAX(F:F)\n, MIN DPS,=MIN(F:F)\n, AVG DPS,=AVERAGE(F:F)\n";
        else if (num == 1)
            spam1 = "\n,MAX DPS,=MAX(F:F)\n, MIN DPS,=MIN(F:F)\n, AVG DPS,=AVERAGE(F:F)\n";
        else if (num == 2)
            spam2 = "\n,MAX DPS,=MAX(F:F)\n, MIN DPS,=MIN(F:F)\n, AVG DPS,=AVERAGE(F:F)\n";
        else if (num == 3)
            spam3 = "\n,MAX DPS,=MAX(F:F)\n, MIN DPS,=MIN(F:F)\n, AVG DPS,=AVERAGE(F:F)\n";
        else if (num == 4)
            spam4 = "\n,MAX DPS,=MAX(F:F)\n, MIN DPS,=MIN(F:F)\n, AVG DPS,=AVERAGE(F:F)\n";
        else if (num == 5)
            spam5 = "\n,MAX DPS,=MAX(F:F)\n, MIN DPS,=MIN(F:F)\n, AVG DPS,=AVERAGE(F:F)\n";
        else if (num == 5)
            spam5 = "\n,MAX DPS,=MAX(F:F)\n, MIN DPS,=MIN(F:F)\n, AVG DPS,=AVERAGE(F:F)\n";

        FinalWrite();
    } */
    void OnApplicationQuit()
    {
        FinalWrite(); // Write any remaining data
        //MinMax(); // Write min max and average dps
        CloseFiles(); // Close files
    }

    void OnDestroy()
    {
        CloseFiles();
    }
}
