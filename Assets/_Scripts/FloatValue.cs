using System;
using UnityEngine;

[CreateAssetMenu(fileName ="FloatValue",
    menuName ="ScriptableObjects/Value/Float")]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] float InitialValue;

    [NonSerialized]
    public float RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize()
    {

    }
}