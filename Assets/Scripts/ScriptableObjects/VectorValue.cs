using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 initialValue;

    [NonSerialized]
    public Vector2 startValue;
    public void OnBeforeSerialize()
    {

    }
    public void OnAfterDeserialize()
    {
        startValue = initialValue;
    }
}
