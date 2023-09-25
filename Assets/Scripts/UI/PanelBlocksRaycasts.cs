using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PanelBlocksRaycasts : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _canvasGroup.blocksRaycasts = true;
    }

    private void OnDisable()
    {
        _canvasGroup.blocksRaycasts = false;
    }
}
