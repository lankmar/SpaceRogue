using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusBarView : MonoBehaviour
{
    [field: SerializeField] public Slider Health { get; private set; }
    [field: SerializeField] public Slider Shield { get; private set; }
}
