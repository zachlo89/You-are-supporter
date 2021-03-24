
public class AcceptAll : DropCondition
{
	public override bool Check(DraggableComponent draggable)
	{
		return draggable.GetComponent<SetItemIcon>().GetItem.slotPosition == SlotPosition.meelWeapon ||
			draggable.GetComponent<SetItemIcon>().GetItem.slotPosition == SlotPosition.accessories ||
			draggable.GetComponent<SetItemIcon>().GetItem.slotPosition == SlotPosition.armor ||
			draggable.GetComponent<SetItemIcon>().GetItem.slotPosition == SlotPosition.head ||
			draggable.GetComponent<SetItemIcon>().GetItem.slotPosition == SlotPosition.shoes;
	}
}
