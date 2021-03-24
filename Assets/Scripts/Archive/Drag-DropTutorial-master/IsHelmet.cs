public class IsHelmet : DropCondition
{
	public override bool Check(DraggableComponent draggable)
	{
		return draggable.GetComponent<SetItemIcon>().GetItem.slotPosition == SlotPosition.head;
	}
}