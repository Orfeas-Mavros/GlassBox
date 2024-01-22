using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Button))]
public class ButtonLogic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color defaultColor;
    public Color hoverColor;
    public Color clickColor;

    public Color defaultOutline;
    public Color hoverOutline;
    public Color clickOutline;

    private SpriteRenderer spriteRenderer;
    private LineRenderer lineRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => StartCoroutine(ButtonClick()));

        DefaultColors();
    }

    private void DefaultColors()
    {
        spriteRenderer.color = defaultColor;
        lineRenderer.startColor = defaultOutline;
        lineRenderer.endColor = defaultOutline;
    }


    public void OnPointerEnter(PointerEventData pointerData)
    {
        Debug.Log("why me");

        spriteRenderer.color = hoverColor;
        lineRenderer.startColor = hoverOutline;
        lineRenderer.endColor = hoverOutline;
    }


    public void OnPointerExit(PointerEventData pointerData)
    {
        DefaultColors();
    }


    IEnumerator ButtonClick()
    {
        Debug.Log("What the hell");

        spriteRenderer.color = clickColor;
        lineRenderer.startColor = clickOutline;
        lineRenderer.endColor = clickOutline;

        yield return new WaitForSeconds(0.05f);

        DefaultColors();
    }
}