using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    public float blinkSpeed = 2f;

    private TextMeshProUGUI tmpText;

    void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (tmpText != null)
        {
            Color color = tmpText.color;

            color.a = Mathf.PingPong(Time.time * blinkSpeed, 1f);

            tmpText.color = color;
        }
    }
}