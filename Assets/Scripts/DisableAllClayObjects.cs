using System.Collections;
using System.Collections.Generic;
using Clayxels;
using UnityEngine;

public class DisableAllClayObjects : MonoBehaviour
{
    private void Start()
    {
        var clayObjects = GetComponentsInChildren<ClayObject>();
        
        foreach (var clayObject in clayObjects)
        {
            clayObject.enabled = false;
        }
    }
}
