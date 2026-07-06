using UnityEngine;

public class CatBoxMover : MonoBehaviour
{
    public void MoveCatBox()
    {
        transform.position = Input.mousePosition;
    }
}
