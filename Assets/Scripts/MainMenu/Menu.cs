using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private RectTransform screenCanvas;
    
    [SerializeField] private Image image;
    [SerializeField] private Sprite menuImage;
    [SerializeField] private Sprite cancelImage;

    [Header("Panels")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _newGamePanel;
    [SerializeField] private GameObject _loadGamePanel;
    [SerializeField] private GameObject _settinsPanel;
    [SerializeField] private GameObject _quitGamePanel;

    [Header("ScrollView")]
    [SerializeField] private RectTransform _loadContent;
    [SerializeField] private GameObject panelPrefab;
    
    [SerializeField] private GameObject congratulationLabel;

    public static Action OnFinishLevel;


    private void Start()
    {
        OnFinishLevel += SeeCongratulationPanel;
    }

    public void MenuButton()
    {
        AudioManager.Instance.PlayPopUpAudio();
        if (_menuPanel.activeSelf)
        {
            AudioManager.Instance.ChangeVolume(PlayerPrefs.GetFloat("Volume"));
            GameManager.Instance._GameState = GameState.Play;
            image.sprite = menuImage;
            HideAllInfoPanel();
            _menuPanel.SetActive(false);
        }
        else
        {
            AudioManager.Instance.ChangeVolume(PlayerPrefs.GetFloat("Volume") - 0.2f);
            GameManager.Instance._GameState = GameState.Pause;
            image.sprite = cancelImage;
            HideAllInfoPanel();
            _menuPanel.SetActive(true);
        }
    }

    public void ResumeButton()
    {
        AudioManager.Instance.ChangeVolume(PlayerPrefs.GetFloat("Volume"));
        GameManager.Instance._GameState = GameState.Play;
        AudioManager.Instance.PlayPopUpAudio();
        image.sprite = menuImage;
        HideAllInfoPanel();
        _menuPanel.SetActive(false);
    }
    
    public void NewGameButton()
    {
        AudioManager.Instance.PlayPopUpAudio();
        HideAllInfoPanel();
        _newGamePanel.SetActive(true);
    }
    
    public void LoadGameButton()
    {
        AudioManager.Instance.PlayPopUpAudio();
        HideAllInfoPanel();
        _loadGamePanel.SetActive(true);
        CreateLoadContent();
    }
    
    public void SettingButton()
    {
        AudioManager.Instance.PlayPopUpAudio();
        HideAllInfoPanel();
        _settinsPanel.SetActive(true);
    }

    public void MainMenuButton()
    {
        image.sprite = menuImage;
        _menuPanel.SetActive(false);
        SceneController.instance.LoadMenu();
    }

    public void QuitButton()
    {
        AudioManager.Instance.PlayPopUpAudio();
        HideAllInfoPanel();
        _quitGamePanel.SetActive(true);
    }

    private List<GameObject> loadPanels = new List<GameObject>();
    private void CreateLoadContent()
    {
        if (loadPanels.Count > 0)
        {
            for (int i = 0; i < loadPanels.Count; i++)
            {
                Destroy(loadPanels[i].gameObject);
            }
            loadPanels.Clear();
        }
        for (int i = 0; i < 10; i++)
        {
            var panel = Instantiate(panelPrefab, _loadContent);
            loadPanels.Add(panel);
        }
    }

    private void SeeCongratulationPanel()
    {
        var panel = Instantiate(congratulationLabel, screenCanvas);
        Destroy(panel, 5f);
    }
    
    private void HideAllInfoPanel()
    {
        _newGamePanel.SetActive(false);
        _loadGamePanel.SetActive(false);
        _settinsPanel.SetActive(false);
        _quitGamePanel.SetActive(false);
    }

    private void OnDestroy()
    {
        OnFinishLevel -= SeeCongratulationPanel;
    }
}
