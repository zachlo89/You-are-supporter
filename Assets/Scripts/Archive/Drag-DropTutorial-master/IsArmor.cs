public class IsArmor : DropCondition
{
	public override bool Check(DraggableComponent draggable)
	{
		return (draggable.GetComponent<SetItemIcon>().GetItem.slotPosition == SlotPosition.armor);
	}
}
