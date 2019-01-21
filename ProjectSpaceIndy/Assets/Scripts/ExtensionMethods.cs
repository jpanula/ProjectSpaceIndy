using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {

    public static TComponent GetOrAddComponent<TComponent>(this GameObject gameObject)
        where TComponent : Component
    {
        TComponent component = gameObject.GetComponent<TComponent>();
        if (component == null)
        {
            component = gameObject.AddComponent<TComponent>();
        }

        return component;
    }
}
