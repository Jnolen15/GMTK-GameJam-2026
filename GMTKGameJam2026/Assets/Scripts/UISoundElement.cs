using UnityEngine;
using UnityEngine.EventSystems;

public class UISoundElement : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    // ============== Refrences / Variables ==============
    [SerializeField] private bool _playHoverSfx = true;

    // ============== Function ==============
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_playHoverSfx) return;

        UISoundPlayer.OnPlayUIHover?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UISoundPlayer.OnPlayUIClick?.Invoke();
    }
}
