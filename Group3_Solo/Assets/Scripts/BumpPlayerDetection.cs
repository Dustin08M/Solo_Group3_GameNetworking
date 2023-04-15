using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BumpPlayerDetection : MonoBehaviour
{
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bump") && PhotonView.Get(collision.gameObject).IsMine)
        {
            Debug.Log("bumped");

            // Call the TakeDamage() function on the bumped player using a PUN RPC
            collision.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered);
        }
    }*/
}
