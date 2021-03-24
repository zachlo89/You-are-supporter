using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI loadingText;

    private void Start()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        float duration = 5f;
        float current = 0;
        
        //Uncomment it when game is ready, now use it simple for imitatitng loading scene
        /*
        while (!asyncLoad.isDone)
        {
            slider.value = asyncLoad.progress;
            loadingText.text = ((float)asyncLoad.progress / 100f).ToString();
            yield return null;
        }
        */
        while(current < duration)
        {
            slider.value = current / duration; ;
            loadingText.text = "Loading... " + ((int)(current * 20)).ToString() + "%";
            current += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2);

    }
}
