using System.Collections;
using UnityEngine;

public class SkipPanel : MonoBehaviour
{
    [SerializeField] private HelpPanel helpPanel;

    public bool opened;
    private CanvasGroup canvasGroup;

    public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void TooglePanel()
    {
        if (helpPanel.opened)
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
}
