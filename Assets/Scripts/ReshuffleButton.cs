using UnityEngine;

public class ReshuffleButton : MonoBehaviour
{
    public void OnClickReshuffle()
    {
        GameManager.Instance.ReshuffleField();
    }
}
