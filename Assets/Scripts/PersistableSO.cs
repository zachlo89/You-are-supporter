using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Android;


/// <summary>
/// TO DO separete character classes skills and skills tree between separate characters, add function to save them
/// </summary>
public class PersistableSO : MonoBehaviour
{
    [Header("Meta")]
    public string bodySpritesToPersist;
    [Header("Scriptable Objects")]
    public ScriptableSpritesList allBodySprites;

    [Header("Meta")]
    public string persistListOfAllItems;
    [Header("Scriptable Objects")]
    public ScriptableItemManager allItem;

    [Header("Meta")]
    public string persisterNameLevels;
    [Header("Scriptable Objects")]
    public List<Level> levelsToPesist = new List<Level>();

    [Header("Meta")]
    public string persisterNameVariables;
    [Header("Scriptable Objects")]
    public List<ScriptableInt> variablesToPesist = new List<ScriptableInt>();

    [Header("Meta")]
    public string persisterNameCharacters;
    [Header("Scriptable Objects")]
    public List<ScriptableCharacter> charactersToPesist = new List<ScriptableCharacter>();

    [Header("Meta")]
    public string persisterNameInventory;
    [Header("Scriptable Objects")]
    public ScriptableItemManager inventoryToPesist;

    [Header("Meta")]
    public string persisterNameQuests;
    [Header("Scriptable Objects")]
    public List<QuestsScriptable> questsToPesist = new List<QuestsScriptable>();

    [Header("Meta")]
    public string persisterNameCharactersTeam;
    [Header("Scriptable Objects")]
    public Team teamToPesist;

    [Header("Meta")]
    public string persistenPlayerSkills;
    [Header("Scriptable Objects")]
    public List<PlayerScriptableSkill> playerSkillsToPesist = new List<PlayerScriptableSkill>();

    [Header("Meta")]
    public string persistenEquipments;
    [Header("Scriptable Objects")]
    public List<Equipment> equipmentsToPesist = new List<Equipment>();

    [Header("Meta")]
    public string persistenSkills;
    [Header("Scriptable Objects")]
    public List<CharacterSkill> skillsToPesist = new List<CharacterSkill>();

    [Header("Meta")]
    public string persistenAllCharacters;
    [Header("Scriptable Objects")]
    public ListOfHeroes allCharacters;

    private string path = "";
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        path = Application.persistentDataPath;
            
    }

    public void StartAgain()
    {
        ResetAllCharacter();
        ResetSprites();
        ResetAllItems();
        ResetLevels();
        ResetVariables();
        ResetCharacters();
        ResetInventory();
        ResetQuests();
        ResetTeam();
        ResetPlayerSkills();
        ResetEquipment();
        ResetSkills();
    }


    void Update()
    {
        // Make sure user is on Android platform
        if (Application.platform == RuntimePlatform.Android)
        {

            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Menu) || Input.GetKey(KeyCode.Home))
            {

                // Quit the application
                SaveAll();
            }

        }
    }


    public void SaveAll()
    {
        SaveVariables();
        SaveSprites();
        SaveAllItems();
        SaveAllCharacters();
        SavePlayerSkills();
        SaveSkills();
        SaveEquipment();
        SaveLevels();
        SaveCharacters();
        SaveInventory();
        SaveQuest();
        SaveTeam();
    }


    public void LoadAll()
    {
        LoadVariables();
        LoadSprites();
        LoadAllItems();
        LoadAllCharacters();
        LoadPlayerSkills();
        LoadSkills();
        LoadEquipment();
        LoadLevels();
        LoadCharacters();
        LoadInventory();
        LoadQuests();
        LoadTeam();
    }
    private void SaveLevels()
    {
        for (int i = 0; i < levelsToPesist.Count; i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterNameLevels, i));
            var json = JsonUtility.ToJson(levelsToPesist[i]);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    private void LoadLevels()
    {
        for (int i = 0; i < levelsToPesist.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterNameLevels, i))){
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterNameLevels, i), FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), levelsToPesist[i]);
                file.Close();
            }
            else
            {
                Debug.Log("File not found");
            }
            
        }
    }

    private void ResetLevels()
    {
        for (int i = 0; i < levelsToPesist.Count; i++)
        {
            if (File.Exists(path + string.Format("/{0}_{1}.pso", persisterNameLevels, i)))
            {
                File.Delete(path + string.Format("/{0}_{1}.pso", persisterNameLevels, i));
                Debug.Log("Deleting old savings");
            }
            else
            {
                Debug.Log("File not found");
            }
        }
    }

    private void SaveVariables()
    {
        for (int i = 0; i < variablesToPesist.Count; i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterNameVariables, i));
            var json = JsonUtility.ToJson(variablesToPesist[i]);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    private void LoadVariables()
    {
        for (int i = 0; i < variablesToPesist.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterNameVariables, i)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterNameVariables, i), FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), variablesToPesist[i]);
                file.Close();
            }
            else
            {
                Debug.Log("File not found");
            }

        }
    }

    private void ResetVariables()
    {
        for (int i = 0; i < variablesToPesist.Count; i++)
        {
            if (File.Exists(path + string.Format("/{0}_{1}.pso", persisterNameVariables, i)))
            {
                File.Delete(path + string.Format("/{0}_{1}.pso", persisterNameVariables, i));
                Debug.Log("Deleting old savings");
            }
            else
            {
                Debug.Log("File not found");
            }
        }
    }

    private void SaveCharacters()
    {
        for (int i = 0; i < charactersToPesist.Count; i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterNameCharacters, i));
            var json = JsonUtility.ToJson(charactersToPesist[i]);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    private void LoadCharacters()
    {
        for (int i = 0; i < charactersToPesist.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterNameCharacters, i)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterNameCharacters, i), FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), charactersToPesist[i]);
                file.Close();
            }
            else
            {
                Debug.Log("File not found");
            }

        }
    }

    private void ResetCharacters()
    {
        for (int i = 0; i < charactersToPesist.Count; i++)
        {
            if (File.Exists(path + string.Format("/{0}_{1}.pso", charactersToPesist, i)))
            {
                File.Delete(path + string.Format("/{0}_{1}.pso", charactersToPesist, i));
                Debug.Log("Deleting old savings");
            }
            else
            {
                Debug.Log("File not found");
            }
        }
    }

    private void SaveInventory()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path + string.Format("/{0}_{1}.pso", persisterNameInventory, inventoryToPesist));
        var json = JsonUtility.ToJson(inventoryToPesist);
        bf.Serialize(file, json);
        file.Close();
    }

    private void LoadInventory()
    {
        if (File.Exists(path + string.Format("/{0}_{1}.pso", persisterNameInventory, inventoryToPesist)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path + string.Format("/{0}_{1}.pso", persisterNameInventory, inventoryToPesist), FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), inventoryToPesist);
            file.Close();
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    private void ResetInventory()
    {
        if (File.Exists(path + string.Format("/{0}_{1}.pso", persisterNameInventory, inventoryToPesist)))
        {
            File.Delete(path + string.Format("/{0}_{1}.pso", persisterNameInventory, inventoryToPesist));
            Debug.Log("Deleting old savings");
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    private void SaveQuest()
    {
        for (int i = 0; i < questsToPesist.Count; i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterNameQuests, i));
            var json = JsonUtility.ToJson(questsToPesist[i]);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    private void LoadQuests()
    {
        for (int i = 0; i < questsToPesist.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterNameQuests, i)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterNameQuests, i), FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), questsToPesist[i]);
                file.Close();
            }
            else
            {
                Debug.Log("File not found");
            }

        }
    }

    private void ResetQuests()
    {
        for (int i = 0; i < questsToPesist.Count; i++)
        {
            if (File.Exists(path + string.Format("/{0}_{1}.pso", persisterNameQuests, i)))
            {
                File.Delete(path + string.Format("/{0}_{1}.pso", persisterNameQuests, i));
                Debug.Log("Deleting old savings");
            }
            else
            {
                Debug.Log("File not found");
            }
        }
    }

    private void SaveTeam()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterNameCharactersTeam, teamToPesist));
        var json = JsonUtility.ToJson(teamToPesist);
        bf.Serialize(file, json);
        file.Close();
    }

    private void LoadTeam()
    {
        if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterNameCharactersTeam, teamToPesist)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterNameCharactersTeam, teamToPesist), FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), teamToPesist);
            file.Close();
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    private void ResetTeam()
    {
        if (File.Exists(path + string.Format("/{0}_{1}.pso", persisterNameCharactersTeam, teamToPesist)))
        {
            File.Delete(path + string.Format("/{0}_{1}.pso", persisterNameCharactersTeam, teamToPesist));
            Debug.Log("Deleting old savings");
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    private void SavePlayerSkills()
    {
        for (int i = 0; i < playerSkillsToPesist.Count; i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persistenPlayerSkills, i));
            var json = JsonUtility.ToJson(playerSkillsToPesist[i]);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    private void LoadPlayerSkills()
    {
        for (int i = 0; i < playerSkillsToPesist.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persistenPlayerSkills, i)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persistenPlayerSkills, i), FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), playerSkillsToPesist[i]);
                file.Close();
            }
            else
            {
                Debug.Log("File not found");
            }

        }
    }

    private void ResetPlayerSkills()
    {
        for (int i = 0; i < playerSkillsToPesist.Count; i++)
        {
            if (File.Exists(path + string.Format("/{0}_{1}.pso", persistenPlayerSkills, i)))
            {
                File.Delete(path + string.Format("/{0}_{1}.pso", persistenPlayerSkills, i));
                Debug.Log("Deleting old savings");
            }
            else
            {
                Debug.Log("File not found");
            }
        }
    }

    private void SaveEquipment()
    {
        for (int i = 0; i < equipmentsToPesist.Count; i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persistenEquipments, i));
            var json = JsonUtility.ToJson(equipmentsToPesist[i]);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    private void LoadEquipment()
    {
        for (int i = 0; i < equipmentsToPesist.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persistenEquipments, i)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persistenEquipments, i), FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), equipmentsToPesist[i]);
                file.Close();
            }
            else
            {
                Debug.Log("File not found");
            }

        }
    }

    private void ResetEquipment()
    {
        for (int i = 0; i < equipmentsToPesist.Count; i++)
        {
            if (File.Exists(path + string.Format("/{0}_{1}.pso", persistenEquipments, i)))
            {
                File.Delete(path + string.Format("/{0}_{1}.pso", persistenEquipments, i));
                Debug.Log("Deleting old savings");
            }
            else
            {
                Debug.Log("File not found");
            }
        }
    }

    private void SaveSkills()
    {
        for (int i = 0; i < skillsToPesist.Count; i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persistenSkills, i));
            var json = JsonUtility.ToJson(skillsToPesist[i]);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    private void LoadSkills()
    {
        for (int i = 0; i < skillsToPesist.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persistenSkills, i)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persistenSkills, i), FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), skillsToPesist[i]);
                file.Close();
            }
            else
            {
                Debug.Log("File not found");
            }

        }
    }

    private void ResetSkills()
    {
        for (int i = 0; i < skillsToPesist.Count; i++)
        {
            if (File.Exists(path + string.Format("/{0}_{1}.pso", persistenSkills, i)))
            {
                File.Delete(path + string.Format("/{0}_{1}.pso", persistenSkills, i));
                Debug.Log("Deleting old savings");
            }
            else
            {
                Debug.Log("File not found");
            }
        }
    }

    private void SaveAllCharacters()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persistenAllCharacters, allCharacters));
        var json = JsonUtility.ToJson(allCharacters);
        bf.Serialize(file, json);
        file.Close();
    }

    private void LoadAllCharacters()
    {
        if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persistenAllCharacters, allCharacters)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persistenAllCharacters, allCharacters), FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), allCharacters);
            file.Close();
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    private void ResetAllCharacter()
    {
        if (File.Exists(path + string.Format("/{0}_{1}.pso", persistenAllCharacters, allCharacters)))
        {
            File.Delete(path + string.Format("/{0}_{1}.pso", persistenAllCharacters, allCharacters));
            Debug.Log("Deleting old savings");
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    private void SaveAllItems()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path + string.Format("/{0}_{1}.pso", persistListOfAllItems, allItem));
        var json = JsonUtility.ToJson(allItem);
        bf.Serialize(file, json);
        file.Close();
    }

    private void LoadAllItems()
    {
        if (File.Exists(path + string.Format("/{0}_{1}.pso", persistListOfAllItems, allItem)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path + string.Format("/{0}_{1}.pso", persistListOfAllItems, allItem), FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), allItem);
            file.Close();
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    private void ResetAllItems()
    {
        if (File.Exists(path + string.Format("/{0}_{1}.pso", persistListOfAllItems, allItem)))
        {
            File.Delete(path + string.Format("/{0}_{1}.pso", persistListOfAllItems, allItem));
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    private void SaveSprites()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path + string.Format("/{0}_{1}.pso", bodySpritesToPersist, allBodySprites));
        var json = JsonUtility.ToJson(allBodySprites);
        bf.Serialize(file, json);
        file.Close();
    }

    private void LoadSprites()
    {
        if (File.Exists(path + string.Format("/{0}_{1}.pso", bodySpritesToPersist, allBodySprites)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path + string.Format("/{0}_{1}.pso", bodySpritesToPersist, allBodySprites), FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), allBodySprites);
            file.Close();
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    private void ResetSprites()
    {
        if (File.Exists(path + string.Format("/{0}_{1}.pso", bodySpritesToPersist, allBodySprites)))
        {
            File.Delete(path + string.Format("/{0}_{1}.pso", bodySpritesToPersist, allBodySprites));
            Debug.Log("Deleting old savings");
        }
        else
        {
            Debug.Log("File not found");
        }
    }


}
