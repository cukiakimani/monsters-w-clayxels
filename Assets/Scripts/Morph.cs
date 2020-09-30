using System;
using System.Collections;
using System.Collections.Generic;
using Clayxels;
using UnityEngine;

public class Morph : MonoBehaviour
{
    [SerializeField] private Transform fromTransformRoot;
    [SerializeField] private Transform toTransformRoot;

    [Range(0f, 1f)] public float Time;

    [SerializeField] private bool update = true;

    private MorphClayObject[] morphClayObjects;
    private ClayContainer[] clayContainers;
    
    private void Start()
    {
        SetUp();
        clayContainers = GetComponentsInChildren<ClayContainer>();
    }

    private void Update()
    {
        foreach (var clayContainer in clayContainers)
        {
            clayContainer.forceUpdate = update;
        }
        
        foreach (var clayObjectMorpher in morphClayObjects)
        {
            clayObjectMorpher.update = update;
            
            if (update)
                clayObjectMorpher.Time = Time;
        }
    }

    private void SetUp()
    {
        AddComponentToChildren(fromTransformRoot, toTransformRoot, transform);
        morphClayObjects = GetComponentsInChildren<MorphClayObject>();
    }

    private void AddComponentToChildren(Transform @from, Transform to, Transform main)
    {
        for (int i = 0; i < main.childCount; i++)
        {
            var child = main.GetChild(i);
            var fromChild = @from.GetChild(i);
            var toChild = to.GetChild(i);

            if (child.GetComponent<MorphClayObject>() == null)
            {
                var morpher = child.gameObject.AddComponent<MorphClayObject>();

                morpher.fromTransform = fromChild;
                morpher.toTransform = toChild;
            }

            if (fromChild.childCount > 0)
                AddComponentToChildren(fromChild, toChild, child);
        }
    }
}