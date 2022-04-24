using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : PlayableCharakter
{

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = Health + "/" + MaxHealth;
    }

    protected override void LoadStats()
    {
        MaxHealth = 400;
        Speed = 3;
        Health = 400;
        Damage = 50;
        Healing = 25;
        IsBlocking = false;
    }
}
