using UnityEngine;
using TMPro;
using Photon.Pun;

public class NameplateController : MonoBehaviourPunCallbacks
{
    public TextMeshPro nameText;
    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        if (photonView.IsMine)
        {
            // Set the offset to the local player's head position
            offset = transform.position - player.transform.position;
        }
        else
        {
            // Disable the nameplate if it doesn't belong to the local player
            nameText.enabled = false;
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // Update the position of the nameplate to follow the local player's head
            transform.position = player.transform.position + offset;
        }
    }

    [PunRPC]
    void SetName(string name)
    {
        nameText.text = name;
    }
}
