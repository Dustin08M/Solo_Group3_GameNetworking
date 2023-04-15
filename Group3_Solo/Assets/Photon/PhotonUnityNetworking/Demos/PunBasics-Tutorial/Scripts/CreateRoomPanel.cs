using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class CreateRoomPanel : MonoBehaviour //Creating Room Panel that also checks if the player has retries (hp) if its 0 it cannot create/join room
{
    [SerializeField] private InputField _roomName;
    [SerializeField] private Button _createBtn;
    [SerializeField] private Text errorMessageText;
    private int currentHp = 0;
    private int currentCoins = 0;
    private int nextHpRegenTime = 0;
    private float hpRegenTime = 3600f;
    private bool isHpRegenerating;


    void Start()
    {
        GetPlayerCurrencies();
    }
    private void Update()
    {
        if (currentHp <= 0 && !isHpRegenerating)
        {
            int currentTime = (int)Time.time;
            nextHpRegenTime = currentTime + (int)hpRegenTime;
            isHpRegenerating = true;
            CallHpRegainTime();
        }
    }
    private void OnEnable()
    {
        _createBtn.onClick.AddListener(CreateRoom);        
    }

    private void OnDisable()
    {
        _roomName.text = "";
        _createBtn.onClick.RemoveAllListeners();
    }

    private void CreateRoom()
    {
            if (!string.IsNullOrEmpty(_roomName.text))
            {
                PhotonNetwork.CreateRoom(_roomName.text, new RoomOptions() { MaxPlayers = 4 });
            }    
    }

    private void GetPlayerCurrencies()
    {
        var request = new GetUserInventoryRequest();

        PlayFabClientAPI.GetUserInventory(request, result =>
        {
            foreach (var item in result.VirtualCurrency)
            {
                if (item.Key == "HP")
                {
                    currentHp = (int)item.Value;
                    Debug.Log("Player's Retries Left: " + currentHp);
                    if (currentHp == 0)
                    {
                        _createBtn.gameObject.SetActive(false);
                        errorMessageText.text = "You don't have enough Tries to continue";
                        DeleteErrorNotif();
                    }
                    UpdateHpRegenTime();
                }
                else if (item.Key == "CN")
                {
                    currentCoins = (int)item.Value;
                    Debug.Log("Player's Coins: " + currentCoins);
                }
            }
        }, error =>
        {
            Debug.LogError($"Error: {error.ErrorMessage}");
        });
    }

    private void UpdateHpRegenTime()
    {
        var request = new GetUserDataRequest() { Keys = new List<string>() { "NextHpRegenTime" } };

        PlayFabClientAPI.GetUserData(request, result =>
        {
            if (result.Data.TryGetValue("NextHpRegenTime", out var nextHpRegenTimeValue))
            {
                nextHpRegenTime = int.Parse(nextHpRegenTimeValue.Value);
                isHpRegenerating = nextHpRegenTime > Time.realtimeSinceStartup;
            }
        }, error =>
        {
            Debug.LogError($"Error: {error.ErrorMessage}");
        });
    }

    private void CallHpRegainTime()
    {
        if (isHpRegenerating == true)
        {
            Debug.Log($"Next HP Regen: {(nextHpRegenTime - (int)Time.time)} seconds");
        }
    }

    void OnGetUserInventoryFailure(PlayFabError error)
    {
        Debug.LogError($"Error: {error.ErrorMessage}");
    }

    private void DeleteErrorNotif()
    {
        errorMessageText.text = "";
    }

}
