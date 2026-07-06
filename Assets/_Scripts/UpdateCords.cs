using UnityEngine;
using UnityEngine.UI;

public class UpdateCords : MonoBehaviour
{
    [SerializeField] private Transform catBox;
    void Update()
    {
        GetComponent<Text>().text = catBox.localPosition.x.ToString() + ", " + catBox.localPosition.y.ToString();
    }
}
