using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private List<CharacterBattle> playerCharacters = new List<CharacterBattle>();
    private List<CharacterBattle> enemiesCharacters = new List<CharacterBattle>();

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

    public List<CharacterBattle> GetAllEniemies(string tag)
    {
        if (tag == "Enemy")
        {
            return playerCharacters;
        }
        else return enemiesCharacters;
    }
}
