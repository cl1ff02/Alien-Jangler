using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Fishing : MonoBehaviour
{
    InputAction fishingAction;
    private bool canFish = false;
    public Image FishingBar;
    public float currentFishingPos;
    public float maxFishingPos, perfectFishingPos;
    public float fishingEffort;
    public Action<float> OnPressChange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishingAction = InputSystem.actions.FindAction("Fishing");
        OnPressChange?.Invoke(currentFishingPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (canFish)
        {
            if (fishingAction.IsPressed())
            {
                currentFishingPos += fishingEffort;
                OnPressChange?.Invoke(currentFishingPos);
                if(fishingEffort > maxFishingPos)
                {
                    fishingEffort = maxFishingPos;
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canFish = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canFish = false;
        }
    }
}
