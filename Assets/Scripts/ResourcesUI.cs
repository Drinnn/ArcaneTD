using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] private Transform resourceTemplate;
    [SerializeField] private float firstResourceOffset = -39f;
    [SerializeField] private float offsetAmount = -160f;

    private ResourceTypeListSO _resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> _resourceTypeTransformDictionary;

    private void Awake()
    {
        resourceTemplate.gameObject.SetActive(false);

        _resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        _resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();

        int index = 0;
        foreach (ResourceTypeSO resourceType in _resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            _resourceTypeTransformDictionary[resourceType] = resourceTransform;

            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(index == 0 ? firstResourceOffset : offsetAmount * index + firstResourceOffset, 0);
            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite;

            index++;
        }
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResourceAmount();
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, EventArgs e)
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in _resourceTypeList.list)
        {
            Transform resourceTransform = _resourceTypeTransformDictionary[resourceType];

            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().text = resourceAmount.ToString();
        }
    }

}
