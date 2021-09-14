using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    static public bool automation;
    static public int autoNumber;

    void Awake()
    {
        automation = false; // Set automation on start here
        autoNumber = -1; // Set number of automation here
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
