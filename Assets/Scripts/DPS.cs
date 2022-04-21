using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPS : PlayableCharakter
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        health = 250;
        damage = 75;
        healing = 25;
        isBlocking = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = health + "/250";
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        Debug.Log("Before first Scene loaded");
    }
}
