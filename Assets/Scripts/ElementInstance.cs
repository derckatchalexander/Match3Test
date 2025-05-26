using UnityEngine;
using UnityEngine.EventSystems;

public class ElementInstance : MonoBehaviour, IPointerClickHandler
{
    public GameElement data;
    public SpriteRenderer icon;
    public SpriteRenderer border;

    public void Setup(GameElement element)
    {
        data = element;
        icon.sprite = data.sprite;
        border.color = data.borderColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        GameManager.Instance.OnElementClicked(this);
    }
}