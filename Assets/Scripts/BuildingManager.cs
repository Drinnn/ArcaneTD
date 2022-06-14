using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    [SerializeField] float maxDistanceBetweenBuildings = 25f;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    private BuildingTypeListSO _buildingTypeList;
    private BuildingTypeSO _activeBuildingType;

    private void Awake()
    {
        Instance = this;

        _buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && _activeBuildingType)
        {
            if (CanSpawnBuilding(_activeBuildingType, Utils.GetMouseWorldPosition()))
            {
                Instantiate(_activeBuildingType.prefab, Utils.GetMouseWorldPosition(), Quaternion.identity);
            }
        }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        _activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = buildingType });
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return _activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear)
            return false;

        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder && buildingTypeHolder.buildingType == buildingType)
            {
                return false;
            }
        }

        collider2DArray = Physics2D.OverlapCircleAll(position, maxDistanceBetweenBuildings);
        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder)
            {
                return true;
            }
        }

        return false;
    }

}
