using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndPanel : MonoBehaviour
{
    [SerializeField] private LootSpawner lootSpawner;
    [SerializeField] private ScriptableItemManager inventory;
    private GameManager gm;
    private bool isWon;
    [SerializeField] private GameObject nextRoundButton;
    private void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        nextRoundButton.SetActive(false);
    }

    public void SetWon(bool isWon)
    {
        this.isWon = isWon;
        nextRoundButton.SetActive(true);
    }
    public void BackToMenu()
    {
        if (isWon)
        {
            AddLootToInventory();
            gm.MarkAsCompleted();
        }
        SceneManager.LoadScene("Lobby");
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        if (isWon)
        {
            AddLootToInventory();
            gm.MarkAsCompleted();
        }
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }

    public void GetNextLevel()
    {
        if (isWon)
        {
            AddLootToInventory();
            gm.MarkAsCompleted();
            gm.SetNextLevel();
            SceneManager.LoadScene("GameScene");
        }
    }

    private void AddLootToInventory()
    {
        if (isWon)
        {
            if (lootSpawner != null && inventory != null)
            {
                foreach (ItemScriptable item in lootSpawner.GetDroppedItems())
                {
                    if (item != null)
                    {
                        inventory.AddItem(item);
                    }
                }
                inventory.Gold.value += lootSpawner.GetDroppedGold();
            }
            else Debug.Log("End panel something went wrong");

            gm.UpdateTeamCharactersAfterBattle();
        }
    }


}
