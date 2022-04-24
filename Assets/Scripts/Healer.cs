using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : PlayableCharakter
{
    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = Health + "/" + MaxHealth;
    }

    protected override void LoadStats()
    {
        MaxHealth = 100;
        Speed = 8;
        Health = 100;
        Damage = 25;
        Healing = 75;
        IsBlocking = false;
    }
}
