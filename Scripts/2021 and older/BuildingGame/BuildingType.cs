using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingType : MonoBehaviour
{
    public enum BuiltType { house, store }

    public BuiltType builtType = new BuiltType();

    private void Start()
    {
        if (builtType == BuiltType.house)
        {
            gameObject.AddComponent<House>();
            Destroy(GetComponent<BuildingType>());
        }

        if(builtType == BuiltType.store)
        {
            gameObject.AddComponent<Store>();
            Destroy(GetComponent<BuildingType>());
        }
    }
}
