using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 4;
        health = 600;
        damage = 30;
        healing = 50;
        isBlocking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
