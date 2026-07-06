using System.Collections;
using UnityEngine;

public class HelpPanel : MonoBehaviour
{
    [SerializeField] private GameObject blackPanel;
    [SerializeField] private Levels levels;
    [SerializeField] private GameObject coins;
    [SerializeField] private SkipPanel skipPanel;

    public bool opened;
    private CanvasGroup canvasGroup;

    public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void TooglePanel()
    {
        if (blackPanel.activeSelf || skipPanel.opened)
            return;
        opened = !opened;
        StartCoroutine(FadePanel(opened));
    } 

    private IEnumerator FadePanel(bool state)
    {
        if (state)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += 0.25f;
                canvasGroup.blocksRaycasts = true;
                yield return new WaitForSeconds(0.02f);
            }
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }

        yield return null;
    }

    public void ShowHelp()
    {
        TooglePanel();
        if (levels.coins >= 10)
        {
            levels.coins -= 10;
            levels.UpdateCoins();
            blackPanel.SetActive(true);
            blackPanel.GetComponent<Black>().SetPanel();
        }
        else
        {
            coins.LeanRotateZ(10, 0.09f).setLoopPingPong(2); 
        }

    }

    public void HideHelp()
    {
        blackPanel.SetActive(false);
    }
}
