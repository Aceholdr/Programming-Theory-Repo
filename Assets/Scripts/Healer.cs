using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : PlayableCharakter
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = health + "/100";
    }

    protected override void LoadStats()
    {
        speed = 8;
        health = 100;
        damage = 25;
        healing = 75;
        isBlocking = false;
    }
}
