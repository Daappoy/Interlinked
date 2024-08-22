using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button resume;
    public Button backToMenu;
    public Sprite resumeHoverSprite; // The sprite to display when hovering
    public Sprite backToMenuHoverSprite;
    public Sprite originalResumeSprite;
    public Sprite originalBackToMenuSprite;

    void Start(){
        // Store the original sprites
        originalResumeSprite = resume.image.sprite;
        originalBackToMenuSprite = backToMenu.image.sprite;
    }

    
    public void OnPointerEnter(PointerEventData eventData){
        if (eventData.pointerEnter == resume.gameObject){
            resume.image.sprite = resumeHoverSprite;
        } else if (eventData.pointerEnter == backToMenu.gameObject){
            backToMenu.image.sprite = backToMenuHoverSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData){
        if (eventData.pointerCurrentRaycast.gameObject == resume.gameObject){
            resume.image.sprite = originalResumeSprite;
        }
        else if (eventData.pointerCurrentRaycast.gameObject == backToMenu.gameObject){
            backToMenu.image.sprite = originalBackToMenuSprite;
        }
    }
}