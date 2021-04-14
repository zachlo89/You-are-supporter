using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectHeroes : MonoBehaviour
{
    [SerializeField] private DelegateToUpdateCharacterEquipment delegator;
    [SerializeField] private Team team;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject emptyPrefab;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject selectHeroPanel;

    private void Start()
    {
        PopulatePanel();
        delegator.changeSprites += PopulatePanel;
    }

    public void PopulatePanel()
    {
        foreach (Transform child in parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for(int i = 0; i < team.maxTeamSize; i++)
        {
            try
            {
                if (team.heroesList[i] != null && i < team.avaliabeTeamSize)
                {
                    GameObject temp = Instantiate(prefab, parent.transform);
                    temp.GetComponent<UpdateSlotCharacterUI>().UpdateSlotCharacter(team.heroesList[i]);
                    temp.GetComponent<AddHeroToTeam>().AddSelectHeroPanel(selectHeroPanel, this.gameObject);
                    if (i == 0)
                    {
                        temp.GetComponent<UpdateSlotCharacterUI>().LockCharacter();
                        Destroy(temp.GetComponent<Button>());
                    }
                }
                else if(i < team.avaliabeTeamSize)
                {
                    GameObject temp1 = Instantiate(emptyPrefab, parent.transform);
                    temp1.GetComponent<SlotAddOrLock>().AddTeamMemeber();
                    temp1.GetComponent<AddHeroToTeam>().AddSelectHeroPanel(selectHeroPanel, this.gameObject);
                }
            }
            catch
            {
                if (i < team.avaliabeTeamSize)
                {
                    GameObject temp1 = Instantiate(emptyPrefab, parent.transform);
                    temp1.GetComponent<SlotAddOrLock>().AddTeamMemeber();
                    temp1.GetComponent<AddHeroToTeam>().AddSelectHeroPanel(selectHeroPanel, this.gameObject);
                }
                else
                {
                    GameObject temp2 = Instantiate(emptyPrefab, parent.transform);
                    temp2.GetComponent<SlotAddOrLock>().LockedSlot();
                }
            }
        }
    }
}
