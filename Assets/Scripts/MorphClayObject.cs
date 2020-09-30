using Clayxels;
using UnityEngine;

public class MorphClayObject : MonoBehaviour
{
    public Transform fromTransform;
    public Transform toTransform;
    
    [Space] public bool ignorePosition;
    
    [Space, Range(0f, 1f)] public float Time;

    [HideInInspector] public bool update;
    
    private ClayObject clayObject;
    private ClayObject fromClayObject;
    private ClayObject toClayObject;

    private bool clayObjectNull;
    
    private void Start()
    {
        clayObject = GetComponent<ClayObject>();

        clayObjectNull = clayObject == null;
        
        if (!clayObjectNull)
        {
            fromClayObject = fromTransform.GetComponent<ClayObject>();
            toClayObject = toTransform.GetComponent<ClayObject>();
        }
    }
    
    private void Update()
    {
        if (!update)
            return;
        
        transform.localScale = Vector3.Lerp(fromTransform.localScale, toTransform.localScale, Time);
        transform.rotation = Quaternion.Slerp(fromTransform.rotation, toTransform.rotation, Time);
        
        if (!ignorePosition)
            transform.localPosition = Vector3.Lerp(fromTransform.localPosition, toTransform.localPosition, Time);
        
        if (!clayObjectNull)
            clayObject.color = Color.Lerp(fromClayObject.color, toClayObject.color, Time);
    }
}
