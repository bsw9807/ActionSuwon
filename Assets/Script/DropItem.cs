using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DropItemType
{
    buffItem,
    unventoryItem,

}
public class DropItem : MonoBehaviour
{
    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rotTrans = transform.GetChild(0);
        InitItem();
    }

    private Transform rotTrans;
    private bool isDrop;
    private Vector3 pos;
    private float dropPosY;
    private float time;

    public void InitItem()
    {
        rigid.velocity = Vector3.zero;
        rigid.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        isDrop = false;
        time = 0f;
    }

    private void Update()
    {
        if (isDrop)
        {
            rotTrans.Rotate(Vector3.up * 90.0f * Time.deltaTime);
            pos = rotTrans.position;
            time += Time.deltaTime;
            pos.y = dropPosY + 0.3f * Mathf.Sin(time);
            rotTrans.position = pos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isDrop==false && other.CompareTag("Ground"))
        {
            isDrop = true;
            rigid.useGravity = false;
            rigid.velocity = Vector3.zero;
            dropPosY = rotTrans.position.y;
        }

        if(isDrop && other.CompareTag("Player") )
        {
            InventoryItemData newItem = new InventoryItemData();
            newItem.uid = Random.Range(1001, 1011);
            newItem.amount = 1;
            if(GameManager.Inst.LootingItem(newItem))
            {
                Destroy(gameObject);
            }
        }
    }
}
