using UnityEngine;
using TMPro;
using System.Collections;

public class SlotMachineController : MonoBehaviour
{
    [Header("Money")]

    // Player starting money
    public int currentMoney = 50;

    // UI text for displaying money
    public TextMeshProUGUI moneyText;

    [Header("Popup")]

    // Bet selection popup
    public GameObject betPopup;

    // Win/Lose message text
    public TextMeshProUGUI messageText;

    [Header("Start Panel")]

    // Start screen panel
    public GameObject startPanel;

    [Header("Reels")]

    // Reel references
    public ReelController reel1;
    public ReelController reel2;
    public ReelController reel3;

    [Header("Handle")]

    // Handle animation objects
    public GameObject handle1;
    public GameObject handle2;

    [Header("Shake")]

    // Parent object of full slot machine
    public Transform slotMachineRoot;

    // Shake intensity
    public float shakeAmount = 0.03f;

    [Header("Audio")]

    // Audio manager reference
    public AudioManager audioManager;

    // Current selected bet amount
    private int currentBet;

    // Original machine position
    private Vector3 originalPosition;

    void Start()
    {
        UpdateMoneyUI();

        // Show start panel initially
        startPanel.SetActive(true);

        // Show betting popup
        betPopup.SetActive(true);

        // Default handle state
        handle1.SetActive(true);
        handle2.SetActive(false);

        // Store original position for shake reset
        originalPosition = slotMachineRoot.localPosition;
    }

    // Called when ENTER button is clicked
    public void EnterGame()
    {
        startPanel.SetActive(false);

        // Stop intro loop sound
        audioManager.StopLoopSound();
    }

    // Update money UI text
    void UpdateMoneyUI()
    {
        moneyText.text = currentMoney.ToString();
    }

    // Bet button functions
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

    // Handles betting logic
    void PlaceBet(int amount)
    {
        // Check if player has enough money
        if (currentMoney >= amount)
        {
            currentMoney -= amount;

            currentBet = amount;

            UpdateMoneyUI();

            messageText.text = "";

            // Play click sound
            audioManager.PlayClickSound();

            // Hide popup during spin
            betPopup.SetActive(false);

            StartCoroutine(StartSpin());
        }
        else
        {
            messageText.text = "Not Enough Money!";
        }
    }

    // Reel spinning sequence
    IEnumerator StartSpin()
    {
        // Fake handle pull animation
        handle1.SetActive(false);
        handle2.SetActive(true);

        yield return new WaitForSeconds(0.15f);

        handle1.SetActive(true);
        handle2.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        // Start machine shake
        StartCoroutine(ShakeMachine(3f));

        // Start reel sound
        audioManager.StartReelSound();

        // Start spinning reels
        reel1.StartSpin();
        reel2.StartSpin();
        reel3.StartSpin();

        yield return new WaitForSeconds(2f);

        // Stop reels one by one
        reel1.StopSpin();

        yield return new WaitForSeconds(0.5f);

        reel2.StopSpin();

        yield return new WaitForSeconds(0.5f);

        reel3.StopSpin();

        // Stop reel sound
        audioManager.StopReelSound();

        yield return new WaitForSeconds(1f);

        CheckWin();
    }

    // Machine shake effect
    IEnumerator ShakeMachine(float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float randomX = Random.Range(-shakeAmount, shakeAmount);
            float randomY = Random.Range(-shakeAmount, shakeAmount);

            slotMachineRoot.localPosition =
                originalPosition + new Vector3(randomX, randomY, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Reset machine position
        slotMachineRoot.localPosition = originalPosition;
    }

    // Check winning combinations
    void CheckWin()
    {
        string symbol1 = reel1.GetSymbol();
        string symbol2 = reel2.GetSymbol();
        string symbol3 = reel3.GetSymbol();

        int reward = 0;

        // Jackpot condition
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

        // Small win for 2 matching symbols
        else if (
            symbol1 == symbol2 ||
            symbol2 == symbol3 ||
            symbol1 == symbol3
        )
        {
            reward = currentBet;

            currentMoney += reward;

            messageText.text = "YOU WIN: " + reward;
        }
        else
        {
            messageText.text = "YOU LOSE";
        }

        UpdateMoneyUI();

        // Show popup again after result
        betPopup.SetActive(true);
    }

    // Exit game button
    public void ExitGame()
    {
        // Play click sound
        audioManager.PlayClickSound();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}