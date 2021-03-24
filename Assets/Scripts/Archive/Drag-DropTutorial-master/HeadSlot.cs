

public class HeadSlot : EquipmentSlot
{
	protected override void Awake()
	{
		base.Awake();
		DropArea.DropConditions.Add(new IsHelmet());
	}
}
