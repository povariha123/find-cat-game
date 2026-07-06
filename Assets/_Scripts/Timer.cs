using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float elapsedTime;

    void Start()
    {
        UpdateTimer();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        DisplayTime(elapsedTime);
    }

    void DisplayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateTimer()
    {
        elapsedTime = 0f;
        DisplayTime(elapsedTime);
    }
}
