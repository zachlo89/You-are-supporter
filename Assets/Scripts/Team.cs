using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Team")]
public class Team : ScriptableObject
{
    public List<ScriptableCharacter> heroesList = new List<ScriptableCharacter>();
    public int maxTeamSize;
    public int avaliabeTeamSize;
    
    public void AddCharacter(ScriptableCharacter hero, int position)
    {
        if(position <= avaliabeTeamSize && position <= maxTeamSize)
        {
            if(position >= heroesList.Count)
            {
                heroesList.Add(hero);
            } else heroesList[position] = hero;
        }
    }

    public void RemoveCharacter(int position)
    {
        heroesList.RemoveAt(position);
    }

    public void ResetTeam()
    {
        for(int i = heroesList.Count - 1; i >= 0; i--)
        {
            try
            {
                if (!heroesList[i].isMainCharacter)
                {
                    RemoveCharacter(i);
                }
            }
            catch
            {
                Debug.Log("Reset team, missing character");
            }
        }
    }
}
