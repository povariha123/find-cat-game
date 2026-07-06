using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    [SerializeField] private Sprite iconOn;
    [SerializeField] private Sprite iconOff;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Loose loose;
    [SerializeField] private Text coinsCounter;
    [SerializeField] private GameObject coinsObject;
    [SerializeField] private Levels levels;

    private int lives = 3;

    public void MissHit()
    {
        hearts[lives - 1].sprite = iconOff;
        hearts[lives - 1].gameObject.LeanScale(new Vector3(0.8f, 0.8f, 0.8f), 0.11f).setEaseOutQuint().setLoopPingPong(1);
        lives--;
        if (lives < 1) 
        {
            loose.dimmingIsEnabled = true;
            StartCoroutine(ResetLives());
        }
    }

    private IEnumerator ResetLives()
    {
        yield return new WaitForSeconds(0.5f);
        lives = 3;
        for (int i = 0; i < 3; i++)
        {
            hearts[i].sprite = iconOn;
            hearts[i].gameObject.LeanScale(new Vector3(0.8f, 0.8f, 0.8f), 0.11f).setEaseOutQuint().setLoopPingPong(1);
            if(levels.coins >= 2)
                levels.coins -= 2;
            coinsCounter.text = levels.coins.ToString();
            coinsObject.LeanRotateZ(10, 0.09f).setLoopPingPong(2);
            coinsObject.LeanScale(new Vector3(0.8f, 0.8f, 0.8f), 0.11f).setEaseOutQuint().setLoopPingPong(1);
            yield return new WaitForSeconds(0.6f);
        }
        loose.dimmingIsEnabled = false;
        levels.SaveData();
    }

}