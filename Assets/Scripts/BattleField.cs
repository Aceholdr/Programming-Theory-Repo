using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BattleField : MonoBehaviour
{
    [SerializeField] GameObject[] friendsImport;
    [SerializeField] GameObject[] enemiesImport;

    public static GameObject[] friends;
    public static GameObject[] enemies;

    public static bool isTurnDone;
    public static GameObject[] Friends { get { return friends; } }
    public static GameObject[] Enemies { get { return enemies; } }

    private List<Character> turnOrder = new List<Character>();
    private LinkedList<Character> turnOrderLinked = new LinkedList<Character>();

    // Start is called before the first frame update
    void Start()
    {
        friends = new GameObject[friendsImport.Length];
        enemies = new GameObject[enemiesImport.Length];

        for (int i = 0; i < friendsImport.Length; i++)
        {
            friends[i] = friendsImport[i];
            enemies[i] = enemiesImport[i];
        }

        DetermineOrder();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurnDone)
        {
            Debug.Log("New Turn");
            isTurnDone = false;

            if (turnOrderLinked.First.Next != null)
            {
                turnOrderLinked.RemoveFirst();
            }
            else
            {
                DetermineOrder();
            }
        }
        else
        {
            Character first = turnOrderLinked.First.Value;
            first.MakeTurn(first);
        }
    }

    void DetermineOrder()
    {
        turnOrder.Clear();
        turnOrderLinked.Clear();

        foreach (GameObject character in friends)
        {
            turnOrder.Add(character.GetComponent<Character>());
            character.GetComponent<PlayableCharakter>().isWaiting = true;
        }

        foreach (GameObject enemy in enemies)
        {
            turnOrder.Add(enemy.GetComponent<Character>());
            enemy.GetComponent<Enemy>().hasAction = true;
        }

        turnOrder.Sort();

        foreach(Character turn in turnOrder)
        {
            turnOrderLinked.AddFirst(turn);
            
        }
        foreach (Character turn in turnOrderLinked)
        {
            Debug.Log(turn.name + " has a speed of: " +turn.Speed);
        }
    }
}
