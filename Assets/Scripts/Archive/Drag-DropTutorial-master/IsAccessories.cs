
public class IsAccessories : DropCondition
{
	public override bool Check(DraggableComponent draggable)
	{
		return draggable.GetComponent<SetItemIcon>().GetItem.slotPosition == SlotPosition.accessories;
	}
}