using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCBase : MonoBehaviour
{
    [SerializeField]
    private GameObject popupObj;
    bool isOn = false;
    private void OnTriggerEnter(Collider other)
    {
        if(!isOn && other.CompareTag("Player"))
        {
            isOn = true;
            if( popupObj.TryGetComponent<IBaseTownPopup>(out IBaseTownPopup popup) )
                popup.PopupOpen();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(isOn && other.CompareTag("Player"))
        {
            isOn = false;
            if( popupObj.TryGetComponent<IBaseTownPopup>(out IBaseTownPopup popup))
                popup.PopupClose();
            
            
        }
    }
}
