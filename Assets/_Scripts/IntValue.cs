using System;
using UnityEngine;

[CreateAssetMenu(fileName ="IntValue",
    menuName ="ScriptableObjects/Value/Int")]
public class IntValue : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] int InitialValue;

    [NonSerialized]
    public int RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize()
    {

    }
}
