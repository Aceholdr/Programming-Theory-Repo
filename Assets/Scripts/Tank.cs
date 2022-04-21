using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : PlayableCharakter
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 3;
        health = 400;
        damage = 50;
        healing = 25;
        isBlocking = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = health + "/400";
    }
}
