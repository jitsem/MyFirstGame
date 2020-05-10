﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;

    [NonSerialized]
    public float runTimeValue;
    public void OnBeforeSerialize()
    {

    }
    public void OnAfterDeserialize()
    {
        runTimeValue = initialValue;
    }
}
