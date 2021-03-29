using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerateBodies : MonoBehaviour
{
    [SerializeField] private List<Sprite> headsList = new List<Sprite>();
    [SerializeField] private List<Sprite> beardsList = new List<Sprite>();
    [SerializeField] private List<Sprite> earsList = new List<Sprite>();
    [SerializeField] private List<Sprite> eyebrowsList = new List<Sprite>();
    [SerializeField] private List<Sprite> eyesList = new List<Sprite>();
    [SerializeField] private List<Sprite> hairsList = new List<Sprite>();
    [SerializeField] private List<Sprite> mouthList = new List<Sprite>();
    private List<string> names = new List<string>();

    private void Awake()
    {
        names.Add("Dydan");
        names.Add("Cyrin");
        names.Add("Btorik");
        names.Add("Yoz");
        names.Add("Varlog");
        names.Add("Gothred");
        names.Add("Henca");
        names.Add("Geron");
        names.Add("Fophni");
        names.Add("Orncolb");
        names.Add("Hintiz");
        names.Add("Jaltin");
        names.Add("Linto");
        names.Add("Haggalder");
        names.Add("Crix");
        names.Add("Rizxer");
        names.Add("Hyou");
        names.Add("Timian");
        names.Add("Baskez");
        names.Add("Pondas");
        names.Add("Lexyu");
        names.Add("Estron");
        names.Add("Zaamaar");
        names.Add("Fragg");
        names.Add("Zike");
        names.Add("Jeffrick");
        names.Add("Narek");
        names.Add("Woulfen");
        names.Add("Tordan");
        names.Add("Qhit");
        names.Add("Waris");
        names.Add("Enaham");
        names.Add("Ragran");
        names.Add("Tattrin");
        names.Add("Yadim");
        names.Add("Uriu");
        names.Add("Intis");
        names.Add("Olve");
        names.Add("Pealan");
        names.Add("Ahron");
        names.Add("Shamael");
        names.Add("Danrik");
        names.Add("Fangar");
        names.Add("Goux");
        names.Add("Haxar");
        names.Add("Jawke");
        names.Add("Kalten");
        names.Add("Lalak");
    }
    public Sprite GetRandomBodyPart(string bodyPart)
    {
        switch (bodyPart)
        {
            case "Head":
                return headsList[Random.Range(0, headsList.Count)];
            case "Beard":
                return beardsList[Random.Range(0, beardsList.Count)];
            case "Ears":
                return earsList[Random.Range(0, earsList.Count)];
            case "Eyebrows":
                return eyebrowsList[Random.Range(0, eyebrowsList.Count)];
            case "Eyes":
                return eyesList[Random.Range(0, eyesList.Count)];
            case "Hairs":
                return hairsList[Random.Range(0, hairsList.Count)];
            case "Mouth":
                return mouthList[Random.Range(0, mouthList.Count)];
            default:
                return null;
        }
    }

    public string GetRandomName()
    {
        int randomNumber = Random.Range(0, name.Length - 1);
        string randomName = names[randomNumber];
        names.RemoveAt(randomNumber);
        return randomName;
    }

}
