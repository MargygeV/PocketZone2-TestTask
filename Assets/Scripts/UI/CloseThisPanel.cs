using UnityEngine;
using UnityEngine.UI;

public class CloseThisPanel : MonoBehaviour
{
    [SerializeField] private Button _closePanelButton;

    private void OnEnable()
    {
        _closePanelButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnDisable()
    {
        _closePanelButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }
}
