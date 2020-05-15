using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    
    public bool initialValue;

    [NonSerialized]
    public bool runTimeValue;
    public void OnBeforeSerialize()
    {

    }
    public void OnAfterDeserialize()
    {
        runTimeValue = initialValue;
    }
}
