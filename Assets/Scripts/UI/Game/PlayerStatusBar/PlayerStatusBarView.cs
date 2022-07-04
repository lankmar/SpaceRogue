using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusBarView : MonoBehaviour
{
    [field: SerializeField] public Slider HealthSlider { get; private set; }
    [field: SerializeField] public Slider ShieldSlider { get; private set; }
}
