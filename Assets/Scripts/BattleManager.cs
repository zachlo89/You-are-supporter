using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BattleManager : MonoBehaviour
{
    private List<CharacterBattle> playerCharacters = new List<CharacterBattle>();
    private List<CharacterBattle> enemiesCharacters = new List<CharacterBattle>();
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private LootSpawner lootSpawner;
    private bool isWon;
    private bool panelOn;
    private void Start()
    {
        panelOn = false;
        Time.timeScale = 1;
        isWon = false;
        winPanel.SetActive(false);
    }
    public void PopulateList(string tag, CharacterBattle hero)
    {
        if (tag == "Player")
        {
            playerCharacters.Add(hero);
        } else
        {
            enemiesCharacters.Add(hero);
        }
    }

    public bool CheckIfEnd()
    {
        int p1 = 0;
        foreach(CharacterBattle player in playerCharacters)
        {
            if(player.IsAlive == true)
            {
                ++p1;
            }
        }
        if(p1 == 0)
        {
            TurnOnEndPanel(isWon);
            return false;
        }
        int p2 = 0;
        foreach (CharacterBattle enemy in enemiesCharacters)
        {
            if (enemy.IsAlive == true)
            {
                ++p2;
            }
        }
        if (p2 == 0)
        {
            isWon = true;
            TurnOnEndPanel(isWon);
            return false;
        }

        return true;
    }

    //Use for normal attacks
    public CharacterBattle GetFrontCharacter(string tag)
    {
        if (tag == "Enemy")
        {
            for(int i = 0; i < playerCharacters.Count; i++)
            {
                if (playerCharacters[i].IsAlive)
                {
                    return playerCharacters[i];
                }
            }
            return null;
        } else
        {
            for (int i = 0; i < enemiesCharacters.Count; i++)
            {
                if (enemiesCharacters[i].IsAlive)
                {
                    return enemiesCharacters[i];
                }
            }
            return null;
        }
    }

    //Use for skills
    public CharacterBattle GetMiddleCharacter(string tag)
    {
        if (tag == "Enemy")
        {
            for (int i = 1; i < playerCharacters.Count; i++)
            {
                if (playerCharacters[i].IsAlive)
                {
                    return playerCharacters[i];
                }
            }
            if (playerCharacters[0].IsAlive)
            {
                return playerCharacters[0];
            }
            return null;
        }
        else
        {
            for (int i = 1; i < enemiesCharacters.Count; i++)
            {
                if (enemiesCharacters[i].IsAlive)
                {
                    return enemiesCharacters[i];
                }
            }
            if (enemiesCharacters[0].IsAlive)
            {
                return enemiesCharacters[0];
            }
            return null;
        }
    }

    public CharacterBattle GetLastEnemy(string tag)
    {
        if (tag == "Enemy")
        {
            for (int i = playerCharacters.Count-1; i > -1; i--)
            {
                if (playerCharacters[i].IsAlive)
                {
                    return playerCharacters[i];
                }
            }
            return null;
        }
        else
        {
            for (int i = enemiesCharacters.Count -1; i > 0; i--)
            {
                if (enemiesCharacters[i].IsAlive)
                {
                    return enemiesCharacters[i];
                }
            }
            return null;
        }
    }

    //Use for skills
    public List<CharacterBattle> GetAllEniemies(string tag)
    {
        if (tag == "Enemy")
        {
            return playerCharacters;
        }
        else return enemiesCharacters;
    }

    private void TurnOnEndPanel(bool win)
    {
        if (!panelOn)
        {
            if (win)
            {
                winPanel.SetActive(true);
                winPanel.GetComponent<WinPanel>().WinPanelConstructor();
            } else
            {
                losePanel.SetActive(true);
            }
            panelOn = true;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }

    public int GetEnemiesExpirience()
    {
        int sumOfExpirence = 0;
        foreach (CharacterBattle character in enemiesCharacters)
        {
            sumOfExpirence += character.Hero.expToGiveToThePlayers;
        }
        sumOfExpirence = (int)(sumOfExpirence / playerCharacters.Count);
        return sumOfExpirence;
    }
    public List<CharacterBattle> GetAllPlayersCharacters()
    {
        return playerCharacters;
    }
}
