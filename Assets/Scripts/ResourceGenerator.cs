using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private BuildingTypeSO _buildingType;
    private float _timer;
    private float _timerMax;

    private void Awake()
    {
        _buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        _timerMax = _buildingType.resourceGeneratorData.timeToGenerate;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            ResourceManager.Instance.AddResource(_buildingType.resourceGeneratorData.resourceType, 1);
            _timer += _timerMax;
        }
    }
}
