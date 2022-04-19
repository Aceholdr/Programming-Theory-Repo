using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayableCharakter : Character
{
    [SerializeField] Button[] actionButton;
    [SerializeField] Button[] friendButton;
    [SerializeField] Button[] enemyButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        foreach (Button b in actionButton)
        {
            b.onClick.AddListener(delegate { SelectAction(b); });
        }

        foreach (Button b in actionButton)
        {
            b.onClick.AddListener(delegate { SelectFriend(b); });
            b.gameObject.SetActive(false);
        }

        foreach (Button b in enemyButton)
        {
            b.onClick.AddListener(delegate { SelectEnemy(b); });
            b.gameObject.SetActive(false);
        }
    }

    private void SelectAction(Button action)
    {
        string actionStr = action.GetComponentInChildren<TextMeshProUGUI>().text;

        if (actionStr == "Heal")
        {
            foreach (Button b in actionButton)
            {
                b.gameObject.SetActive(false);
            }

            foreach (Button b in friendButton)
            {
                b.gameObject.SetActive(true);
            }
        }
        else if (actionStr == "Attack")
        {
            foreach (Button b in actionButton)
            {
                b.gameObject.SetActive(false);
            }

            foreach (Button b in enemyButton)
            {
                b.gameObject.SetActive(true);
            }
        }
        else if (actionStr == "Block")
        {
            Block();
        }
    }

    private void SelectFriend(Button friendBttn)
    {
        string actionStr = friendBttn.GetComponentInChildren<TextMeshProUGUI>().text;

        switch (actionStr)
        {
            case "Healer":
                // code
                break;
            case "DPS Left":
                // code
                break;
            case "DPS Right":
                // code
                break;
            case "Tank":
                // code
                break;
            default:
                foreach (Button b in actionButton)
                {
                    b.gameObject.SetActive(true);
                }

                foreach (Button b in actionButton)
                {
                    b.gameObject.SetActive(false);
                }
                break;

        }
    }

    private void SelectEnemy(Button enemyBttn)
    {
        // Add Enemy Buttons...
    }

    protected void MakeTurn()
    {

    }
}
