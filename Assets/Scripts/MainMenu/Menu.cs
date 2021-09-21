using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite menuImage;
    [SerializeField] private Sprite cancelImage;

    [SerializeField] private GameObject _menuPanel;
    
    [SerializeField] private GameObject _newGameInfo;
    [SerializeField] private GameObject _loadGameInfo;
    [SerializeField] private GameObject _saveGameInfo;
    [SerializeField] private GameObject _optionInfo;
    [SerializeField] private GameObject _quitGameInfo;

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
    
    public void NewGameButton()
    {
        HideAllInfoPanel();
        _newGameInfo.SetActive(true);
    }
    
    public void LoadGameButton()
    {
        HideAllInfoPanel();
        _loadGameInfo.SetActive(true);
    }
    
    public void SaveGameButton()
    {
        HideAllInfoPanel();
        _saveGameInfo.SetActive(true);
    }
    
    public void OptionButton()
    {
        HideAllInfoPanel();
        _optionInfo.SetActive(true);
    }
    
    public void QuitButton()
    {
        HideAllInfoPanel();
        _quitGameInfo.SetActive(true);
    }
    
    private void HideAllInfoPanel()
    {
        _newGameInfo.SetActive(false);
        _loadGameInfo.SetActive(false);
        _saveGameInfo.SetActive(false);
        _optionInfo.SetActive(false);
        _quitGameInfo.SetActive(false);
    }
}
