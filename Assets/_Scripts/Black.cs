using UnityEngine;
using UnityEngine.UI;

public class Black : MonoBehaviour
{
    [SerializeField] private Levels levels;

    public void SetPanel()
    {
        if (levels.CatIsOnRight())
            transform.GetComponent<Image>().fillOrigin = 0;
        else
            transform.GetComponent<Image>().fillOrigin = 1;
    }
}
