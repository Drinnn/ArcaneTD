using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    [SerializeField] private GameObject spriteGameObject;

    private void Awake()
    {
        Hide();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void Update()
    {
        transform.position = Utils.GetMouseWorldPosition();
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        if (e.activeBuildingType)
        {
            Show(e.activeBuildingType.sprite);
        }
        else
        {
            Hide();
        }
    }

    private void Show(Sprite ghostSprite)
    {
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
        spriteGameObject.SetActive(true);
    }

    private void Hide()
    {
        spriteGameObject.SetActive(false);
    }

}
