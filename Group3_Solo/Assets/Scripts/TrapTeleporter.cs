using UnityEngine;
using Photon.Pun;

public class TrapTeleporter : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PhotonView.Get(other.gameObject).IsMine)
        {
            // Teleport the local player's game object back to spawn
            other.transform.position = spawnPoint.position;

            // Trigger an RPC to notify other players of the teleportation
            GetComponent<PhotonView>().RPC("TeleportPlayer", RpcTarget.Others, other.gameObject.GetPhotonView().ViewID);
        }
    }

    [PunRPC]
    public void TeleportPlayer(int viewID)
    {
        // Get the game object associated with the given PhotonView ID
        GameObject playerObj = PhotonView.Find(viewID).gameObject;

        // Teleport the player's game object back to spawn
        playerObj.transform.position = spawnPoint.position;
    }
}
