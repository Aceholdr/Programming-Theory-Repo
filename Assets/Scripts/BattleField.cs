using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleField : MonoBehaviour
{
    [SerializeField] GameObject[] friendsImport;
    [SerializeField] GameObject[] enemiesImport;

    [SerializeField] TextMeshProUGUI turnText;
    [SerializeField] TextMeshProUGUI endText;
    [SerializeField] RawImage endTextBG;

    static GameObject[] friends;
    static GameObject[] enemies;

    public static int deadPlayerCounter;
    public static int deadEnemyCounter;

    public static bool isTurnDone;
    public static GameObject[] Friends { get { return friends; } }
    public static GameObject[] Enemies { get { return enemies; } }

    List<Character> turnOrder = new List<Character>();
    LinkedList<Character> turnOrderLinked = new LinkedList<Character>();

    // Start is called before the first frame update
    void Start()
    {
        StartFight();
    }

    // Loads the player characters and enemies and determines the turn order
    void StartFight()
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
        AdvanceTurns();
    }

    // When a turn is done, the character will be deleted from the list
    void AdvanceTurns()
    {
        if (isTurnDone)
        {
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
            do
            {
                Character first = turnOrderLinked.First.Value;
                turnText.text = first.name + " turn";
                first.MakeTurn(first);

                if (turnOrderLinked.First.Value.IsDead)
                {
                    turnOrderLinked.RemoveFirst();
                }
            } while (turnOrderLinked.First.Value.IsDead);
        }

        CheckIfEveryoneDead(); 
    }

    // If every friend is dead you lose, if every enemy is dead you win
    void CheckIfEveryoneDead()
    {
        if(deadPlayerCounter == friendsImport.Length || deadEnemyCounter == enemiesImport.Length)
        {
            if(deadPlayerCounter == friendsImport.Length)
            {
                endText.text = "You died...\nPress Esc to go back to the menu";
            }
            else if(deadEnemyCounter == enemiesImport.Length)
            {
                endText.text = "You did it!\nPress Esc to go back to the menu";
            }

            Time.timeScale = 0;
            endText.gameObject.SetActive(true);
            endTextBG.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);

                deadPlayerCounter = 0;
                deadEnemyCounter = 0;
            }
        }
    }

    // Sorts the characters by their Speed
    void DetermineOrder()
    {
        turnOrder.Clear();
        turnOrderLinked.Clear();

        foreach (GameObject friend in friends)
        {
            if (!friend.GetComponent<Character>().IsDead)
            {
                turnOrder.Add(friend.GetComponent<Character>());
                friend.GetComponent<PlayableCharakter>().isWaiting = true;
            }
        }

        foreach (GameObject enemy in enemies)
        {
            if (!enemy.GetComponent<Character>().IsDead)
            {
                turnOrder.Add(enemy.GetComponent<Character>());
                enemy.GetComponent<Enemy>().hasAction = true;
            }
        }

        turnOrder.Sort();

        foreach(Character turn in turnOrder)
        {
            turnOrderLinked.AddFirst(turn);          
        }
    }
}
