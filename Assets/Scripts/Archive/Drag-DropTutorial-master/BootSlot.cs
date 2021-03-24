
public class BootSlot : EquipmentSlot
{
	protected override void Awake()
	{
		base.Awake();
		DropArea.DropConditions.Add(new IsBoots());
	}
}
