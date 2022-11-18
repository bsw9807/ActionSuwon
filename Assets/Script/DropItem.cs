using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private Rigidbody rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }
}
