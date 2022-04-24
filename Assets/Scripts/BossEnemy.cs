using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = 600;
        Speed = 4;
        Health = 600;
        Damage = 45;
        Healing = 50;
        IsBlocking = false;
    }
}
