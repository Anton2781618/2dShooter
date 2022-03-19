using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemGrid selectedItemGrid;
    public ItemGrid SelectedItemGrid 
    {
        get => selectedItemGrid; 
        set 
        {
            selectedItemGrid = value;
            inventoryIHighLight.SetParent(value);
        } 
    }

    InventoryItem selectedItem;    
    InventoryItem overlapItem;    
    RectTransform rectTransform;

    [SerializeField] private List<ItemData> items;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform canvasTransform;

    private InventoryIHighLight inventoryIHighLight;

    private void Awake() 
    {
        inventoryIHighLight = GetComponent<InventoryIHighLight>();    
    }

    private void Update() 
    {
        ItemIconDrah();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(selectedItem == null)
            {
                CreateRandomItem();
            }
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            InsertRandomItem();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }

        if(SelectedItemGrid == null)
        {
            inventoryIHighLight.Show(false);
            return;
        }

        HandleHighlight();
        
        if(Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }
    }

    private void RotateItem()
    {
        if(selectedItem == null) {return;}

        selectedItem.Rotated();
    }

    private void InsertRandomItem()
    {
        if(selectedItemGrid == null) {return;}

        CreateRandomItem();
        InventoryItem itemToInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemToInsert );
    }

    private void InsertItem(InventoryItem itemToInsert)
    {
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);

        if(posOnGrid == null) {return;}

        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    Vector2Int oldPosition;
    InventoryItem itemToHighLight;

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTitleGridPosition();
        if(oldPosition == positionOnGrid){return;}
         
        oldPosition = positionOnGrid;
        if(selectedItem == null)
        {
            itemToHighLight = SelectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

            if(itemToHighLight != null)
            {
                inventoryIHighLight.Show(true);
                inventoryIHighLight.SetSize(itemToHighLight);
                inventoryIHighLight.SetPosition(SelectedItemGrid, itemToHighLight);
            }
            else
            {
                inventoryIHighLight.Show(false);
            }
        }
        else
        {
            inventoryIHighLight.Show(SelectedItemGrid.BoundryCheck(positionOnGrid.x, positionOnGrid.y,
                                    selectedItem.WIDTH, selectedItem.HEIGHT));
            inventoryIHighLight.SetSize(selectedItem);
            inventoryIHighLight.SetPosition(SelectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y);
        }
    }

    //создать случайный итем
    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        rectTransform.SetAsLastSibling();

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }

    //метод перемещает итем в след за мышкой
    private void ItemIconDrah()
    {
        if(selectedItem)
        {
            rectTransform.position = Input.mousePosition;
        }
    }

    //метод выделяет итем и станавлевает его в ячейку
    private void LeftMouseButtonPress()
    {
        Vector2Int titleGridPosition = GetTitleGridPosition();
        if (selectedItem == null)
        {
            PickUpItem(titleGridPosition);
        }
        else
        {
            PlaceItem(titleGridPosition);
        }
    }

    //тут мы устанавливаем итем на сетку со смещением. Это для того что бы распологать итем по центру а не с краю мышки
    private Vector2Int GetTitleGridPosition()
    {
        Vector2 position = Input.mousePosition;
        
        if (selectedItem != null)
        {
            position.x -= (selectedItem.WIDTH - 1) * ItemGrid.titleSizeWidth / 2;
            position.y += (selectedItem.HEIGHT - 1) * ItemGrid.titleSizeHeight / 2;
        }

        return SelectedItemGrid.GetTitleGridPosition(position);
    }

    private void PickUpItem(Vector2Int titleGridPosition)
    {
        selectedItem = SelectedItemGrid.PickUpIteme(titleGridPosition.x, titleGridPosition.y);
        if (selectedItem) rectTransform = selectedItem.GetComponent<RectTransform>();
    }

    private void PlaceItem(Vector2Int titleGridPosition)
    {
        bool complete =  SelectedItemGrid.PlaceItem(selectedItem, titleGridPosition.x, titleGridPosition.y, ref overlapItem);        
        if(complete)
        {
            selectedItem = null;
            if(overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
            }
        } 
    }
}
