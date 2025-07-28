using App.Services.Popups;
using UnityEngine;
using UnityEngine.UI;

public class PopupComplete : BasePopup
{
    [SerializeField] private Text _scoreLabel;

    public void Init(int score)
    {
        _scoreLabel.text = score.ToString();
    }
}
