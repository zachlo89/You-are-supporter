using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableComponent : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public event Action<PointerEventData> OnBeginDragHandler;
	public event Action<PointerEventData> OnDragHandler;
	public event Action<PointerEventData, bool> OnEndDragHandler;
	public bool FollowCursor { get; set; } = true;
	public Vector3 StartPosition;
	public bool CanDrag { get; set; } = true;

	private RectTransform rectTransform;
	private Canvas canvas;

	private Transform parentToRenturnTo = null;
	private int positionIndex;
	private ItemScriptable item;
	private EquipmentPanel equipmentPanel;
	private int index;
	private Transform inventoryPosition;
	private float startingPosition;

	private float halfScreen;

	private void Awake()
	{
		halfScreen = Screen.width / 2;
		rectTransform = GetComponent<RectTransform>();
		canvas = GetComponentInParent<Canvas>();
	}

    private void Start()
    {
		equipmentPanel = GameObject.FindObjectOfType<EquipmentPanel>();
		item = GetComponent<SetItemIcon>().GetItem;
		inventoryPosition = GameObject.FindGameObjectWithTag("Inventory").transform;
		SetIndex(transform.parent.name);
    }

    public void OnBeginDrag(PointerEventData eventData)
	{
		startingPosition = eventData.position.x;
		if (!CanDrag)
		{
			return;
		}

		OnBeginDragHandler?.Invoke(eventData);
		parentToRenturnTo = this.transform.parent;
		positionIndex = this.transform.GetSiblingIndex();
		this.transform.SetParent(transform.parent.parent.parent.parent);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!CanDrag)
		{
			return;
		}

		OnDragHandler?.Invoke(eventData);

		if (FollowCursor)
		{
			rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!CanDrag)
		{
			return;
		}

		var results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, results);

		DropArea dropArea = null;

		foreach (var result in results)
		{
			dropArea = result.gameObject.GetComponent<DropArea>();

			if (dropArea != null)
			{
				break;
			}
		}

		if (dropArea != null)
		{
			if (dropArea.Accepts(this))
			{
				SetIndex(dropArea.name);
				if (dropArea.GetComponentInChildren<SetItemIcon>() != null)
                {
					GameObject temp = dropArea.GetComponentInChildren<SetItemIcon>().gameObject;
					temp.transform.SetParent(parentToRenturnTo);
					temp.transform.SetSiblingIndex(this.positionIndex);
					equipmentPanel.UnEquipItem(temp.GetComponent<SetItemIcon>().GetItem, index);
				}
				dropArea.Drop(this);
				OnEndDragHandler?.Invoke(eventData, true);
				transform.SetParent(dropArea.transform);
                equipmentPanel.EquipItem(item, index, positionIndex);

				return;
			}
		}
		if(eventData.position.x > halfScreen && startingPosition < halfScreen)
        {
			equipmentPanel.UnEquipItem(item, index);
			
			this.transform.SetParent(inventoryPosition);
			rectTransform.anchoredPosition = StartPosition;
			OnEndDragHandler?.Invoke(eventData, false);
			return;
		}
		
		this.transform.SetParent(parentToRenturnTo);
		rectTransform.anchoredPosition = StartPosition;
		//transform.SetSiblingIndex(positionIndex);
		//equipmentPanel.UnEquipItem(item, index);
		OnEndDragHandler?.Invoke(eventData, false);
		
		
	}

	public void OnInitializePotentialDrag(PointerEventData eventData)
	{
		StartPosition = rectTransform.anchoredPosition;
	}

	private void SetIndex(String name)
    {
		switch (name)
        {
			case "Head":
				index = 0;
				break;
			case "Armor":
				index = 1;
				break;
			case "Arm1":
				index = 2;
				break;
			case "Arm2":
				index = 3;
				break;
			case "Boots":
				index = 4;
				break;
			case "Accesories":
				index = 5;
				break;

		}
    }
}
