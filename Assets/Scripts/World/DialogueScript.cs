using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
   public RectTransform panelRect;
   [SerializeField] private TextMeshProUGUI panelText;
   [SerializeField] private Image panelImage;
   public Vector2 hiddenPosition;
   public Vector2 visiblePosition;

   public float slideDuration = 0.75f;

   void Awake()
    {
        panelRect.anchoredPosition = hiddenPosition;
    }

    public void SlideUp()
    {
        panelRect.DOKill();

        panelRect.DOAnchorPos(visiblePosition, slideDuration).SetEase(Ease.Linear);

    }

    public void SlideDown()
    {
        panelRect.DOKill();

        panelRect.DOAnchorPos(hiddenPosition, slideDuration).SetEase(Ease.Linear);
    }

    public void SetDialogue(string dialogue)
    {
        panelText.text = dialogue;
    }

    public void ClearDialogue()
    {
        panelText.text = null;
    }

}
