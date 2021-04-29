using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSubSelectMenu : MonoBehaviour
{
    private GameObject popupPanel;
    private GameManager gameManager;
    private Level level;
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    [SerializeField] private Sprite dimIcon;
    [SerializeField] private GameObject pathUp, pathRight, pathLeft, pathDown;
    [SerializeField] private GameObject focus;
    [SerializeField] private TextMeshProUGUI levelValue;
    [SerializeField] private List<GameObject> stars = new List<GameObject>();
    [SerializeField] private GameObject goldStage;
    [SerializeField] private GameObject goldLock;
    [SerializeField] private GameObject gemStage;
    [SerializeField] private Button goldButton;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void SetPopupPanel(GameObject popupPanel)
    {
        this.popupPanel = popupPanel;
    }
    public void UpdateSubStageMenuUI(Level level, int index, bool hasPrevious, bool hasNext)
    {
        this.level = level;
        button.onClick.AddListener(() => OpenPopupPanel());
        if (!level.isAvaliable)
        {
            image.sprite = dimIcon;
            button.interactable = false;
        }
        if(!level.isPassed && level.isAvaliable)
        {
            focus.SetActive(true);
        }
        levelValue.text = index.ToString();
        if (level.isPassed)
        {
            for(int i = 0; i < level.stars; i++)
            {
                stars[i].SetActive(true);
            }
        }
        if (hasPrevious)
        {
            pathLeft.SetActive(true);
        }
        if (hasNext)
        {
            pathRight.SetActive(true);
        }
        if (level.hasGoldLevel)
        {
            pathUp.SetActive(true);
            goldStage.SetActive(true);
            if (!level.isPassed)
            {
                goldLock.SetActive(true);
                goldButton.interactable = false;
            }
            else goldLock.SetActive(false);
        }
    }

    private void OpenPopupPanel()
    {
        popupPanel.SetActive(true);
        popupPanel.GetComponent<LevelPopupPanel>().Constructor(level);
        gameManager.SetCurrentLevel(level);
    }
}
