using UnityEngine;

public class ReelController : MonoBehaviour
{
    [Header("Spin Settings")]

    // Reel movement speed
    public float spinSpeed = 16f;

    // Symbol stop positions
    public float[] stopPositions;

    // Symbol names for payout logic
    public string[] symbolNames;

    private bool isSpinning;
    private bool isStopping;

    private float targetY;

    private int resultIndex;

    private string currentSymbol;

    void Update()
    {
        // Continuous reel movement
        if (isSpinning)
        {
            transform.localPosition += Vector3.down * spinSpeed * Time.deltaTime;

            // Reset reel for looping effect
            if (transform.localPosition.y <= -30f)
            {
                transform.localPosition = new Vector3(
                    transform.localPosition.x,
                    30f,
                    transform.localPosition.z
                );
            }
        }

        // Smooth stopping movement
        if (isStopping)
        {
            Vector3 targetPosition = new Vector3(
                transform.localPosition.x,
                targetY,
                transform.localPosition.z
            );

            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                targetPosition,
                25f * Time.deltaTime
            );

            // Snap perfectly into final position
            if (Vector3.Distance(transform.localPosition, targetPosition) < 0.01f)
            {
                transform.localPosition = targetPosition;

                isStopping = false;
            }
        }
    }

    // Start reel spinning
    public void StartSpin()
    {
        isSpinning = true;
        isStopping = false;
    }

    // Stop reel at random symbol
    public void StopSpin()
    {
        isSpinning = false;

        // RNG-based random symbol selection
        resultIndex = Random.Range(0, stopPositions.Length);

        targetY = stopPositions[resultIndex];

        currentSymbol = symbolNames[resultIndex];

        isStopping = true;
    }

    // Return selected symbol
    public string GetSymbol()
    {
        return currentSymbol;
    }
}