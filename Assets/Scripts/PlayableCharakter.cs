using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayableCharakter : Character
{
    private Button[] actionButton;
    private Button[] friendButton;
    private Button[] enemyButton;

    private BattleField battleField;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Assigns every button
    private void Awake()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Action"))
        {
            Button b = g.GetComponent<Button>();
            b.onClick.AddListener(delegate { SelectAction(b); });
        }

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Friend"))
        {
            Button b = g.GetComponent<Button>();
            b.onClick.AddListener(delegate { SelectFriend(b); });

            MoveGameObjectOut(g);
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Button b = g.GetComponent<Button>();
            b.onClick.AddListener(delegate { SelectEnemy(b); });

            MoveGameObjectOut(g);
        }

        battleField = GameObject.FindWithTag("BattleField").GetComponent<BattleField>();
    }

    // Selects the action the player takes
    private void SelectAction(Button action)
    {
        string actionStr = action.GetComponentInChildren<TextMeshProUGUI>().text;

        if (actionStr == "Heal")
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Action"))
            {
                MoveGameObjectOut(g);
            }

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Friend"))
            {
                MoveGameObjectIn(g);
            }
        }
        else if (actionStr == "Attack")
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Action"))
            {
                MoveGameObjectOut(g);
            }

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                MoveGameObjectIn(g);
            }
        }
        else if (actionStr == "Block")
        {
            Block();
        }
    }

    // Heals the chosen friend
    private void SelectFriend(Button friendBttn)
    {
        string friendStr = friendBttn.GetComponentInChildren<TextMeshProUGUI>().text;

        switch (friendStr)
        {
            case "Healer":
                Heal(healing, battleField.Charakters[0]);
                break;
            case "DPS Left":
                Heal(healing, battleField.Charakters[1]);
                break;
            case "DPS Right":
                Heal(healing, battleField.Charakters[2]);
                break;
            case "Tank":
                Heal(healing, battleField.Charakters[3]);
                break;
            default:
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Action"))
                {
                    MoveGameObjectIn(g);
                }

                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Friend"))
                {
                    MoveGameObjectOut(g);
                }
                break;
        }
    }

    // Inflicts damage to the chosen enemy
    private void SelectEnemy(Button enemyBttn)
    {
        string enemyStr = enemyBttn.GetComponentInChildren<TextMeshProUGUI>().text;

        switch (enemyStr)
        {
            case "Left":
                Attack(damage, battleField.Enemies[0]);
                break;
            case "Middle L":
                Attack(damage, battleField.Enemies[1]);
                break;
            case "Middle R":
                Attack(damage, battleField.Enemies[2]);
                break;
            case "Right":
                Attack(damage, battleField.Enemies[3]);
                break;
            default:
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Action"))
                {
                    MoveGameObjectIn(g);
                }

                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    MoveGameObjectOut(g);
                }
                break;
        }
    }

    protected override void MakeTurn()
    {

    }

    // Moves the UI Buttons into the screen
    private void MoveGameObjectIn(GameObject g)
    {
        g.transform.position += new Vector3(transform.position.x + 220, transform.position.y - 1, transform.position.z);
    }

    // Moves the UI Buttons out of the screen
    private void MoveGameObjectOut(GameObject g)
    {
        g.transform.position += new Vector3(transform.position.x - 200, transform.position.y, transform.position.z);
    }
}
