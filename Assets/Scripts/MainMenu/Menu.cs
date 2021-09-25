using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
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

    public void MenuButton()
    {
        if (_menuPanel.activeSelf)
        {
            image.sprite = menuImage;
            HideAllInfoPanel();
            _menuPanel.SetActive(false);
        }
        else
        {
            image.sprite = cancelImage;
            HideAllInfoPanel();
            _menuPanel.SetActive(true);
        }
    }

    public void ResumeButton()
    {
        image.sprite = menuImage;
        HideAllInfoPanel();
        _menuPanel.SetActive(false);
    }
    
    public void NewGameButton()
    {
        HideAllInfoPanel();
        _newGamePanel.SetActive(true);
    }
    
    public void LoadGameButton()
    {
        HideAllInfoPanel();
        _loadGamePanel.SetActive(true);
        CreateLoadContent();
    }
    
    public void SettingButton()
    {
        HideAllInfoPanel();
        _settinsPanel.SetActive(true);
        
    }

    public void QuitButton()
    {
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
    
    private void HideAllInfoPanel()
    {
        _newGamePanel.SetActive(false);
        _loadGamePanel.SetActive(false);
        _settinsPanel.SetActive(false);
        _quitGamePanel.SetActive(false);
    }

    public void OverButtons()
    {
        
    }
}
