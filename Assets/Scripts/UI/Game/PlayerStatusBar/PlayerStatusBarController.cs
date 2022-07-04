using Abstracts;
using Gameplay.Health;
using Gameplay.Player;
using UnityEngine;
using UnityEngine.UI;
using Utilities.ResourceManagement;

public class PlayerStatusBarController : BaseController
{
    private readonly ResourcePath healtShieldToolBarPath = new ResourcePath("Prefabs/Canvas/Game/PlayerStatusBar");
    private readonly ResourcePath canvasPath = new ResourcePath("Prefabs/Canvas/Canvas");

    private readonly PlayerStatusBarView healthShieldToolBarView;
    private readonly HealthModel playerHealthShieldModel;
    private readonly Transform barTransform;

    private Slider healthToolBarSlider;
    private Slider shieldToolBarSlider;

    public PlayerStatusBarController()
    {
        Canvas canvase = LoadView<Canvas>(canvasPath);
        healthShieldToolBarView = LoadView<PlayerStatusBarView>(healtShieldToolBarPath);

        barTransform = healthShieldToolBarView.gameObject.transform;
        healthToolBarSlider = GameObject.Instantiate<Slider>(healthShieldToolBarView.HealthSlider);
        healthToolBarSlider.transform.parent = barTransform.transform;
        shieldToolBarSlider = GameObject.Instantiate<Slider>(healthShieldToolBarView.ShieldSlider);
        shieldToolBarSlider.transform.parent = barTransform.transform;

        barTransform.parent = canvase.transform;
        barTransform.localScale = Vector3.one;
        RectTransform rectTransformToolBar = healthShieldToolBarView.gameObject.GetComponent<RectTransform>();
        rectTransformToolBar.anchoredPosition = Vector3.zero;
        rectTransformToolBar.sizeDelta = Vector3.zero;

        EntryPoint.SubscribeToUpdate(UpdateHealtShieldToolBar);
    }

    private void UpdateHealtShieldToolBar()
    {
        /* Освободить, когда привяжем игрокувсе начальные максимальные и фактические параметры здоровья и щита отличные от 0
        if (healthToolBarSlider.maxValue != playerHealthShieldModel.MaximumHealth)
        {
            healthToolBarSlider.maxValue = playerHealthShieldModel.MaximumHealth;
        }

        if (shieldToolBarSlider.maxValue != playerHealthShieldModel.MaximumShield)
        {
            shieldToolBarSlider.maxValue = playerHealthShieldModel.MaximumShield;
        }
        
        healthToolBarSlider.value = playerHealthShieldModel.CurrentHealth;

        shieldToolBarSlider.value = playerHealthShieldModel.CurrentShield;
        */
    }

    protected override void OnDispose()
    {
        EntryPoint.UnsubscribeFromUpdate(UpdateHealtShieldToolBar);
    }
}
