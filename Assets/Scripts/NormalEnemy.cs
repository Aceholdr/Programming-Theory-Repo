using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = 300;
        Speed = 7;
        Health = 300;
        Damage = 25;
        Healing = 10;
        IsBlocking = false;
    }
}
