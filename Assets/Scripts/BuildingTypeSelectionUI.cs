using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectionUI : MonoBehaviour
{
    [SerializeField] private List<BuildingTypeSO> ignoredBuildingTypes;
    [SerializeField] private Transform buttonTemplate;
    [SerializeField] private float offsetAmount = 160f;
    [SerializeField] private Sprite cursorSprite;

    private Dictionary<BuildingTypeSO, Transform> _buildingTypeTransformDictionary;
    private Transform _cursorButton;

    private void Awake()
    {
        buttonTemplate.gameObject.SetActive(false);

        _buildingTypeTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();
        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        CreateCursorButton();

        int index = 1;
        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            if (ignoredBuildingTypes.Contains(buildingType))
            {
                continue;
            }

            Transform btnTransform = Instantiate(buttonTemplate, transform);
            _buildingTypeTransformDictionary[buildingType] = btnTransform;

            btnTransform.gameObject.SetActive(true);
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            btnTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            index++;
        }
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        UpdateActiveBuildingTypeButton();
    }

    private void CreateCursorButton()
    {
        _cursorButton = Instantiate(buttonTemplate, transform);
        _cursorButton.gameObject.SetActive(true);
        _cursorButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        _cursorButton.Find("image").GetComponent<Image>().sprite = cursorSprite;
        _cursorButton.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);

        _cursorButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton()
    {
        _cursorButton.Find("selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in _buildingTypeTransformDictionary.Keys)
        {
            Transform btnTransform = _buildingTypeTransformDictionary[buildingType];
            btnTransform.Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingType == null)
        {
            _cursorButton.Find("selected").gameObject.SetActive(true);
        }
        else
        {
            _buildingTypeTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }
    }
}
