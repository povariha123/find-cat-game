using UnityEngine;

public class CanvasScaler : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] GameObject buttons1;
    [SerializeField] GameObject buttons2;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        if (rectTransform.sizeDelta.y >= rectTransform.sizeDelta.x)
            ToogleButtons(true);
        else 
            ToogleButtons(false);
    }    

    private void ToogleButtons(bool state)
    {
        if (state)
        {
            buttons1.SetActive(false);
            buttons2.SetActive(true);
        }
        else
        {
            buttons1.SetActive(true);
            buttons2.SetActive(false);
        }
    }
}
