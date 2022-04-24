using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPS : PlayableCharakter
{
    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = Health + "/" + MaxHealth;
    }

    protected override void LoadStats()
    {
        MaxHealth = 250;
        Speed = 5;
        Health = 250;
        Damage = 75;
        Healing = 25;
        IsBlocking = false;
    }
}
