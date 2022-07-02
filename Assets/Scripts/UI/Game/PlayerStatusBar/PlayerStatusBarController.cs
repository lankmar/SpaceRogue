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

    private Slider hpToolBarSlider;
    private Slider spToolBarSlider;

    public PlayerStatusBarController()
    {
        Canvas canvase = LoadView<Canvas>(canvasPath);
        healthShieldToolBarView = LoadView<PlayerStatusBarView>(healtShieldToolBarPath);

        barTransform = healthShieldToolBarView.gameObject.transform;
        hpToolBarSlider = GameObject.Instantiate<Slider>(healthShieldToolBarView.Health);
        hpToolBarSlider.transform.parent = barTransform.transform;
        spToolBarSlider = GameObject.Instantiate<Slider>(healthShieldToolBarView.Shield);
        spToolBarSlider.transform.parent = barTransform.transform;

        barTransform.parent = canvase.transform;
        barTransform.localScale = Vector3.one;
        RectTransform rect = healthShieldToolBarView.gameObject.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector3.zero;
        rect.sizeDelta = Vector3.zero;

        EntryPoint.SubscribeToUpdate(UpdateHealtShieldToolBar);
    }

    private void UpdateHealtShieldToolBar()
    {
        if (hpToolBarSlider.maxValue != playerHealthShieldModel.MaximumHealth)
        {
            hpToolBarSlider.maxValue = playerHealthShieldModel.MaximumHealth;
        }

        hpToolBarSlider.value = playerHealthShieldModel.CurrentHealth;

        if (spToolBarSlider.maxValue != playerHealthShieldModel.MaximumShield)
        {
            spToolBarSlider.maxValue = playerHealthShieldModel.MaximumShield;
        }

        spToolBarSlider.value = playerHealthShieldModel.CurrentShield;
    }

    protected override void OnDispose()
    {
        EntryPoint.UnsubscribeFromUpdate(UpdateHealtShieldToolBar);
    }
}
