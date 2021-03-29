using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FillTheExpBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textLevel, textExpirience;
    [SerializeField] private Slider slider;
    [SerializeField] private Image imageLevel;
    private int level;
    private int exp;
    private int toNext;

    public void FillBarConstructor(ScriptableCharacter hero, int gainedExp)
    {
        level = hero.level;
        textLevel.text = level.ToString();
        exp = hero.expirence;
        toNext = hero.toNextLevel;
        textExpirience.text = exp + "/" + toNext;
        slider.value = exp / toNext;
        StartCoroutine(FillTheBar(gainedExp));
    }

    IEnumerator FillTheBar(int expirience)
    {
        float timer = expirience;
        while(expirience > 0)
        {
            yield return new WaitForEndOfFrame();
            ++exp;
            textExpirience.text = exp + "/" + toNext;
            slider.value =(float)exp / (float)toNext;
            if (exp >= toNext)
            {
                exp -= toNext;
                toNext = (int)(toNext * 1.5f);
                ++level;
                textLevel.text = level.ToString();
                textExpirience.text = exp + "/" + toNext;
                slider.value = (float)exp / (float)toNext;
            }
            --expirience;
        }
    }
}