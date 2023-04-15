using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrap : MonoBehaviour
{
    [SerializeField]
    private GameObject obj_trans;

    [Range(-50f, 150f)]
    [SerializeField]
    private float rotateSpeed;

    void Update()
    {
        transform.RotateAround(obj_trans.transform.position, new Vector3(0, 0, 5), rotateSpeed * Time.deltaTime);
    }
}
