using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class WinTriggerCheck : MonoBehaviour
{
    [Header("Win Condition")]
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private TMP_Text PlayerWinText;
    //[SerializeField] private TMP_Text YouLostText;

    private static GameObject localPlayer;
    private PhotonView photonView;

    PlayerCurrency currency;

    void Start()
    {
        WinPanel.SetActive(false);
        photonView = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PhotonView.Get(collision.gameObject).IsMine)
        {
            // Show the WinPanel with the PlayerWinText if the local player collides with the finish line
            WinPanel.SetActive(true);
            PlayerWinText.gameObject.SetActive(true);
            PlayerWinText.text = "You Won!";
            AddPlayerHP();
            AddPlayerCoin();

            // Call the displayWin() function on all other players to display the YouLostText
            photonView.RPC("displayEnemyWin", RpcTarget.Others);
        }
    }

    [PunRPC]
    private void displayEnemyWin()
    {
        // Show the WinPanel with the YouLostText if other players collide with the finish line
        WinPanel.SetActive(true);
        PlayerWinText.gameObject.SetActive(false);
        PlayerWinText.gameObject.SetActive(true);
        PlayerWinText.text = "You Lost!";
    }

    public void AddPlayerHP() //Add Player HP by 1 (Tries value) whenever he wins
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "HP",
            Amount = 1
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnGrantCurrencySuccess, OnError);
    }

    public void AddPlayerCoin() //Add Player coins along with HP
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "CN",
            Amount = 50
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnGrantCurrencySuccess, OnError);
    }

    public void SubtractPlayerHP() //Lose 1Hp when other player gets in finish line first
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "HP",
            Amount = 1
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubtractCurrencySuccess, OnError);
    }

    void OnGrantCurrencySuccess(ModifyUserVirtualCurrencyResult result) 
    {
        Debug.Log("Currency Granted!");
        PlayerCurrency.instance.GetVirtualCurrency();
    }

    void OnSubtractCurrencySuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Currency Subtracted");
        PlayerCurrency.instance.GetVirtualCurrency();
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error: " + error.ErrorMessage);
    }
}
