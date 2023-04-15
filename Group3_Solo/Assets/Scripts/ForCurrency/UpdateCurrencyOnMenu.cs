using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class UpdateCurrencyOnMenu : MonoBehaviour
{
    void Awake()
    {
        PlayerCurrency.instance.GetVirtualCurrency();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
