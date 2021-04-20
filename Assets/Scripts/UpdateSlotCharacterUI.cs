using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateSlotCharacterUI : MonoBehaviour
{
    private GameObject characterPrefab;
    private ScriptableCharacter hero;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI expirience;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private Transform spawningPoint;
    [SerializeField] private Slider slider;
    [SerializeField] private List<GameObject> stars;
    [SerializeField] private GameObject characterLock;
    

    public void UpdateSlotCharacter(ScriptableCharacter hero)
    {
        this.hero = hero;
        characterPrefab = hero.prefab;
        characterName.text = hero.characterName;
        expirience.text = hero.expirence + "/" + hero.toNextLevel;
        level.text = hero.level.ToString();
        characterPrefab = Instantiate(hero.prefab, spawningPoint);
        characterPrefab.GetComponent<UpdateFaceAndBody>().SetUpFace(hero);
        characterPrefab.transform.localScale *= 1.5f;
        characterPrefab.GetComponent<UpdateEquipment>().EquipAll(hero.equipment);
        characterPrefab.GetComponentInChildren<Animator>().Play("IdleMelee");
        slider.value = hero.expirence / hero.toNextLevel;
        for(int i = 0; i < hero.stars; i++)
        {
            stars[i].SetActive(true);
        }
    }

    public void LockCharacter()
    {
        characterLock.SetActive(true);
    }

}
