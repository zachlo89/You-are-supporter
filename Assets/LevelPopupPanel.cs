using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelPopupPanel : MonoBehaviour
{
    private GameManager gameManager;
    private Level level;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private Transform spawningPoint;
    [SerializeField] private Button finishNow, fight;
    [SerializeField] private GameObject locker;
    [SerializeField] private ScriptableItemManager inventory;
    [SerializeField] private FastClearLootDrooper drooper;
    private Animator anim;
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
    }

    public void Constructor(Level level)
    {
        this.level = level;
        foreach (Transform child in spawningPoint)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < level.enemiesList.Count; i++)
        {
            GameObject temp = Instantiate(characterPrefab, spawningPoint);
            temp.GetComponent<PopuCharacterSlot>().PopulateHeroPanel(level.enemiesList[i], level.stars);
        }
        if (level.stars >= 3 && inventory.Energy.value >= (level.itemsRarityDrop * 3))
        {
            finishNow.interactable = true;
            locker.SetActive(false);
        } else
        {
            finishNow.interactable = false;
            locker.SetActive(true);
        }
    }


    public void MoveToGameScene()
    {
        gameManager.SetCurrentLevel(level);
        gameManager.SaveGame();
        SceneManager.LoadSceneAsync(3);
    }

    public void FastClear()
    {
        anim.SetTrigger("Expand");
        inventory.AddEnergy(-level.itemsRarityDrop * 3);
        drooper.SpawnItems();
        if (level.stars >= 3 && inventory.Energy.value >= (level.itemsRarityDrop * 3))
        {
            finishNow.interactable = true;
            locker.SetActive(false);
        }
        else
        {
            finishNow.interactable = false;
            locker.SetActive(true);
        }
    }
}
