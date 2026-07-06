using UnityEngine;

public class Loose : MonoBehaviour
{
    [SerializeField] CanvasGroup black;
    [SerializeField] BezierMovement bmHearts;
    [SerializeField] BezierMovement bmCoins;
    [SerializeField] CanvasGroup musicButtonCanvasGroup;
    [SerializeField] AudioSource audioSource;
    [SerializeField] RectTransform canvasRectTransform;
    [SerializeField] private float dimmingSpeed = 0.02f;

    public bool dimmingIsEnabled;

    private void Update()
    {
        float aspectRatio = canvasRectTransform.rect.height / canvasRectTransform.rect.width;
        float tValue = 1;
        if (aspectRatio >= 0.57)
            tValue = 0.6f;

        if (dimmingIsEnabled)
        {
            if (black.alpha < 1)
            {
                black.alpha = Mathf.Lerp(black.alpha, 1, dimmingSpeed * 1.5f);
                musicButtonCanvasGroup.alpha = Mathf.Lerp(musicButtonCanvasGroup.alpha, 0, dimmingSpeed * 2f);
                bmHearts.t = Mathf.Lerp(bmHearts.t, tValue, dimmingSpeed);
                bmCoins.t = Mathf.Lerp(bmCoins.t, tValue, dimmingSpeed);
                audioSource.pitch = Mathf.Lerp(audioSource.pitch, 0.7f, dimmingSpeed * 1.5f);
                black.blocksRaycasts = true;
                musicButtonCanvasGroup.blocksRaycasts = false;
            }
        }
        else
        {
            if (black.alpha > 0)
            {
                black.alpha = Mathf.Lerp(black.alpha, 0, dimmingSpeed * 1.5f);
                musicButtonCanvasGroup.alpha = Mathf.Lerp(musicButtonCanvasGroup.alpha, 1, dimmingSpeed * 2f);
                bmHearts.t = Mathf.Lerp(bmHearts.t, 0, dimmingSpeed);
                bmCoins.t = Mathf.Lerp(bmCoins.t, 0, dimmingSpeed);
                audioSource.pitch = Mathf.Lerp(audioSource.pitch, 1, dimmingSpeed * 1.5f);
                black.blocksRaycasts = false;
                musicButtonCanvasGroup.blocksRaycasts = true;
            }
        }
    }
}
