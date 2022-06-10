using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    private Dictionary<ResourceTypeSO, int> _resourcesAmountDictionary;

    private void Awake()
    {
        Instance = this;

        _resourcesAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            _resourcesAmountDictionary[resourceType] = 0;
        }

        TestLogResourceAmountDictionary();
    }

    private void TestLogResourceAmountDictionary()
    {
        foreach (ResourceTypeSO resourceType in _resourcesAmountDictionary.Keys)
        {
            Debug.Log(resourceType.nameString + ": " + _resourcesAmountDictionary[resourceType]);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        _resourcesAmountDictionary[resourceType] += amount;
        TestLogResourceAmountDictionary();
    }
}
