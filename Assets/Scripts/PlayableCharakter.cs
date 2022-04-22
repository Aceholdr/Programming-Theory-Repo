using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayableCharakter : Character
{
    Character activeChar;

    private Button[] actionButton;
    private Button[] friendButton;
    private Button[] enemyButton;

    [SerializeField] protected TextMeshProUGUI healthDisplay;

    public bool isWaiting = true;
    private string lastAction;



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

        LoadStats();
    }

    virtual protected void LoadStats()
    {

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

        if (isWaiting && activeChar == this)
        {
            lastAction = "Heal";

            switch (friendStr)
            {
                case "Healer":
                    Heal(healing, BattleField.Friends[0]);
                    isWaiting = false;
                    break;
                case "DPS Left":
                    Heal(healing, BattleField.Friends[1]);
                    isWaiting = false;
                    break;
                case "DPS Right":
                    Heal(healing, BattleField.Friends[2]);
                    isWaiting = false;
                    break;
                case "Tank":
                    Heal(healing, BattleField.Friends[3]);
                    isWaiting = false;
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
    }

    // Inflicts damage to the chosen enemy
    private void SelectEnemy(Button enemyBttn)
    {
        string enemyStr = enemyBttn.GetComponentInChildren<TextMeshProUGUI>().text;

        if (isWaiting && activeChar == this)
        {
            lastAction = "Attack";

            switch (enemyStr)
            {
                case "Left":
                    Attack(damage, BattleField.Enemies[0]);
                    isWaiting = false;
                    Debug.Log(this.name + " clicked");
                    Debug.Log(activeChar + "was active");
                    break;
                case "Middle L":
                    Attack(damage, BattleField.Enemies[1]);
                    isWaiting = false;
                    break;
                case "Middle R":
                    Attack(damage, BattleField.Enemies[2]);
                    isWaiting = false;
                    break;
                case "Right":
                    Attack(damage, BattleField.Enemies[3]);
                    isWaiting = false;
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
    }

    protected IEnumerator WaitInTurn()
    {
        if(isWaiting == false)
        {
            yield return new WaitForSeconds(2);
            infoText.text = "vorbei";
            StopAllCoroutines();

            BattleField.isTurnDone = true;
            ResetUI();
            this.activeChar = null;
        }
    }

    public override void MakeTurn(Character activeChar)
    {
        this.activeChar = activeChar;
        StartCoroutine(WaitInTurn());
    }

    // Moves the UI Buttons into the screen
    private void MoveGameObjectIn(GameObject g)
    {
        g.transform.localScale = new Vector3(1, 1);
    }

    // Moves the UI Buttons out of the screen
    private void MoveGameObjectOut(GameObject g)
    {
        g.transform.localScale = new Vector3(0.005f, 0.005f);
    }

    void ResetUI()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Action"))
        {
            MoveGameObjectIn(g);   
        }

        if (lastAction == "Heal")
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Friend"))
            {
                MoveGameObjectOut(g);
            }
        }
        else if (lastAction == "Attack")
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                MoveGameObjectOut(g);
            }
        }
    }
}
