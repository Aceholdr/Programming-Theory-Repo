using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 7;
        health = 300;
        damage = 15;
        healing = 10;
        isBlocking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
