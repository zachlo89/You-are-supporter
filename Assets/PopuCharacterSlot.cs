using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopuCharacterSlot : MonoBehaviour
{
    [SerializeField] private Transform spawningPoint;
    [SerializeField] private List<GameObject> starsList;
    [SerializeField] private List<Image> imagesColorsToChange = new List<Image>();
    [SerializeField] private List<Color> starsColors = new List<Color>();
    [SerializeField] private ScriptableItemManager inventory;
    private Animator anim;

    private void OnEnable()
    {
        if(anim != null)
        {
            anim.Play("IdleMelee");
        }
    }

    public void PopulateHeroPanel(ScriptableCharacter hero, int stars)
    {
        for (int i = 0; i < starsList.Count; i++)
        {
            starsList[i].SetActive(false);
        }
        for (int i = 0; i < hero.stars; i++)
        {
            starsList[i].SetActive(true);
        }

        GameObject temp = Instantiate(hero.prefab, spawningPoint);
        temp.GetComponent<UpdateFaceAndBody>().SetUpFace(hero);
        temp.GetComponent<UpdateEquipment>().EquipAll(hero.equipment);
        anim = temp.GetComponentInChildren<Animator>();
        anim.Play("IdleMelee");
        
        
        foreach(Image img in imagesColorsToChange)
        {
            img.color = starsColors[stars];
        }
    }
}
