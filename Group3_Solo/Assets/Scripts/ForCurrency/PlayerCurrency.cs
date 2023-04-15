using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayerCurrency : MonoBehaviour
{
    public static PlayerCurrency instance;
    public Text PlayerHp;
    public Text _Coin;

    public int hpAmount;
    public int cnAmount;

    public void Init(string currencyCode, int balance)
    {
        switch (currencyCode)
        {
            case "HP":

                hpAmount = balance;
                break;
            case "CN":
                cnAmount = balance;
                break;
        }

    }
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GetVirtualCurrency();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetVirtualCurrency()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), GetUserCurrencySuccess, OnError);
    }


    public void GetUserCurrencySuccess(GetUserInventoryResult result)
    {
        int _playerHp = result.VirtualCurrency["HP"];
        PlayerHp.text = _playerHp.ToString();

        int _playerCoin = result.VirtualCurrency["CN"];
        _Coin.text = _playerCoin.ToString();
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error: " + error.ErrorMessage);
    }
}
