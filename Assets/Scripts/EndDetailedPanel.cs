using TMPro;
using UnityEngine;
public class EndDetailedPanel : MonoBehaviour
{
    [SerializeField] private ScriptableItemManager inventory;
    [SerializeField] private TextMeshProUGUI rarity;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI goldValue;
    [SerializeField] private TextMeshProUGUI damageText, defenceText, attackSpeedText, healthText;
    [SerializeField] private GameObject damage, defence, attackSpeed, health;
    [SerializeField] private LootSpawner lootSpawner;

    private ItemScriptable item;
    private GameObject itemIcon;

    public void UpdateRightPanel(ItemScriptable item, GameObject itemIcon)
    {
        this.item = item;
        this.itemIcon = itemIcon;
        if (item != null)
        {
            rarity.text = item.rarity.ToString();
            itemName.text = item.name;
            goldValue.text = item.sellValue.ToString();
            if (item.damage != 0)
            {
                damage.SetActive(true);
                damageText.text = item.damage.ToString();
            }
            else damage.SetActive(false);
            if (item.armor != 0)
            {
                defence.SetActive(true);
                defenceText.text = item.armor.ToString();
            }
            else defence.SetActive(false);
            if (item.attackRate != 0)
            {
                attackSpeed.SetActive(true);
                attackSpeedText.text = item.attackRate.ToString();
            }
            else attackSpeed.SetActive(false);
            if (item.hp != 0)
            {
                health.SetActive(true);
                healthText.text = item.hp.ToString();
            }
            else health.SetActive(false);
        }
        else gameObject.SetActive(false);
    }

    public void SellItem()
    {
        if (itemIcon != null)
        {

            inventory.Gold.value += item.sellValue;
            lootSpawner.RemoveDroopedItem(item);
            Destroy(itemIcon);

            UpdateRightPanel(null, null);
        }
    }

}
