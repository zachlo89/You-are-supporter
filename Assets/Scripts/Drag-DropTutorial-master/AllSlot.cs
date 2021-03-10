

public class AllSlot : EquipmentSlot
{
	protected override void Awake()
	{
		base.Awake();
		DropArea.DropConditions.Add(new AcceptAll());
	}
}
