using UnityEngine;
using UnityEngine.UI;

public class DestroyPlayerBottonView : MonoBehaviour
{
    public GameObject gameObject;
    public void BackToMenu()
    {
        gameObject.GetComponent<Text>().name = "QUTI!";
    }
}