using UnityEngine;

public class ReelController : MonoBehaviour
{
    [Header("Spin Settings")]
    public float spinSpeed = 20f;

    public float[] stopPositions;

    public string[] symbolNames;

    private bool isSpinning;

    private bool isStopping;

    private float targetY;

    private int resultIndex;

    private string currentSymbol;

    void Update()
    {
        if(isSpinning)
        {
            transform.Translate(Vector3.down * spinSpeed * Time.deltaTime);

            if(transform.position.y <= -15f)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    15f,
                    transform.position.z
                );
            }
        }

        if(isStopping)
        {
            Vector3 targetPosition = new Vector3(
                transform.position.x,
                targetY,
                transform.position.z
            );

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                25f * Time.deltaTime
            );

            if(Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;

                isStopping = false;
            }
        }
    }

    public void StartSpin()
    {
        isSpinning = true;

        isStopping = false;
    }

    public void StopSpin()
    {
        isSpinning = false;

        resultIndex = Random.Range(0, stopPositions.Length);

        targetY = stopPositions[resultIndex];

        currentSymbol = symbolNames[resultIndex];

        isStopping = true;
    }

    public string GetSymbol()
    {
        return currentSymbol;
    }
}