
public class WeaponSlot : EquipmentSlot
{
	protected override void Awake()
	{
		base.Awake();
		DropArea.DropConditions.Add(new IsWeaponCondition());
	}
}
