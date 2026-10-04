using UnityEngine;
using UnityEngine.UI;

public class FishingBar : MonoBehaviour
{
    [SerializeField] private Fishing fishing;
    [SerializeField] private Slider fishingSlider;
    private bool maxFishInitialized;
    private void OnEnable() => fishing.OnPressChange += HandleFishPosChanged;
    private void OnDisable() => fishing.OnPressChange -= HandleFishPosChanged;
    private void HandleFishPosChanged(float currentFishgingPos)
    {
        if (!maxFishInitialized)
        {
            fishingSlider.maxValue = currentFishgingPos;
            maxFishInitialized = true;
        }
        fishingSlider.value = currentFishgingPos;
    }
}
