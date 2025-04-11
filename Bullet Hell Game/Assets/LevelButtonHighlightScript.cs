using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelButtonHighlightScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color HighlightColor;
    public Color NormalColor;
    public float transitionSpeed;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeLight(GetComponentInChildren<SpriteRenderer>(), true));
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeLight(GetComponentInChildren<SpriteRenderer>(), false));
    }
    public IEnumerator ChangeLight(SpriteRenderer sr, bool lighten)
    {
        Color target = lighten ? HighlightColor : NormalColor;
        while (sr.color != target)
        {
            sr.color = Color.Lerp(sr.color, target, transitionSpeed);
            yield return null;
        }
    }
    

    
}
