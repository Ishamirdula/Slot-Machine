using UnityEngine;
using TMPro;
using System.Collections;

public class SlotMachineController : MonoBehaviour
{
    [Header("Money")]
    public int currentMoney = 50;
    public TextMeshProUGUI moneyText;

    [Header("Popup")]
    public GameObject betPopup;
    public TextMeshProUGUI messageText;

    [Header("Reels")]
    public ReelController reel1;
    public ReelController reel2;
    public ReelController reel3;

    private int currentBet;

    void Start()
    {
        UpdateMoneyUI();
        betPopup.SetActive(true);
    }

    void UpdateMoneyUI()
    {
        moneyText.text = currentMoney.ToString();
    }

    public void Bet10()
    {
        PlaceBet(10);
    }

    public void Bet50()
    {
        PlaceBet(50);
    }

    public void Bet100()
    {
        PlaceBet(100);
    }

    void PlaceBet(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;

            currentBet = amount;

            UpdateMoneyUI();

            messageText.text = "";

            betPopup.SetActive(false);

            StartCoroutine(StartSpin());
        }
        else
        {
            messageText.text = "Not Enough Money!";
        }
    }

    IEnumerator StartSpin()
    {
        reel1.StartSpin();
        reel2.StartSpin();
        reel3.StartSpin();

        yield return new WaitForSeconds(2f);

        reel1.StopSpin();

        yield return new WaitForSeconds(0.5f);

        reel2.StopSpin();

        yield return new WaitForSeconds(0.5f);

        reel3.StopSpin();

        yield return new WaitForSeconds(1f);

        CheckWin();
    }

    void CheckWin()
    {
        string symbol1 = reel1.GetSymbol();
        string symbol2 = reel2.GetSymbol();
        string symbol3 = reel3.GetSymbol();

        int reward = 0;

        if (symbol1 == symbol2 && symbol2 == symbol3)
        {
            switch (symbol1)
            {
                case "Cherry":
                    reward = currentBet * 2;
                    break;

                case "Bell":
                    reward = currentBet * 4;
                    break;

                case "TripleBar":
                    reward = currentBet * 6;
                    break;

                case "Diamond":
                    reward = currentBet * 8;
                    break;

                case "Seven":
                    reward = currentBet * 10;
                    break;
            }

            currentMoney += reward;

            messageText.text = "YOU WIN: " + reward;
        }
        else if (
            symbol1 == symbol2 ||
            symbol2 == symbol3 ||
            symbol1 == symbol3
        )
        {
            reward = currentBet;

            currentMoney += reward;

            messageText.text = "SMALL WIN: " + reward;
        }
        else
        {
            messageText.text = "YOU LOSE";
        }

        UpdateMoneyUI();

        betPopup.SetActive(true);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}