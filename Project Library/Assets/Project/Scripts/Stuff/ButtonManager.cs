using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private bool _targetStatus = false;


    public void ObjectStatus()
    {
        _targetStatus = !_targetStatus;
        if (gameObject != null) gameObject.SetActive(_targetStatus);
    }
}