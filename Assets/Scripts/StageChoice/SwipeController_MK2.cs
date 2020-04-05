using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController_MK2 : ViewController, IBeginDragHandler, IEndDragHandler
{
    private Rect currentViewRect;
    private ScrollRect cachedScrollRect;
    public ScrollRect CachedScrollRect
    {
        get
        {
            if(cachedScrollRect == null)
            {
                cachedScrollRect = GetComponent<ScrollRect>();
            }
            return cachedScrollRect;
        }
    }

    private bool isAnimating = false;
    private Vector2 destPosition;
    private Vector2 initialPosition;
    private AnimationCurve animationCurve;
    private int prevPageIndex = 0;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("isBegin");
        isAnimating = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GridLayoutGroup grid = CachedScrollRect.content.GetComponent<GridLayoutGroup>();

        CachedScrollRect.StopMovement();

        float pageWidth = -(grid.cellSize.x + grid.spacing.x);

        int pageIndex = Mathf.RoundToInt((CachedScrollRect.content.anchoredPosition.x) / pageWidth);

        if(pageIndex == prevPageIndex && Mathf.Abs(eventData.delta.x) >= 3)
        {
            CachedScrollRect.content.anchoredPosition += new Vector2(eventData.delta.x, 0.0f);
            pageIndex += (int)Mathf.Sign(-eventData.delta.x);
        }

        if(pageIndex <-1)
        {
            pageIndex = -1;
        }
        else if (pageIndex > grid.transform.childCount -2)
        {
            pageIndex = grid.transform.childCount - 2;
        }

        prevPageIndex = pageIndex;

        float destX = pageIndex * pageWidth;
        destPosition = new Vector2(destX, CachedScrollRect.content.anchoredPosition.y);

        initialPosition = CachedScrollRect.content.anchoredPosition;

        Keyframe keyframe1 = new Keyframe(Time.time, 0.0f, 0.0f, 1.0f);
        Keyframe keyframe2 = new Keyframe(Time.time + 0.3f, 1.0f, 0.0f, 0.0f);
        animationCurve = new AnimationCurve(keyframe1, keyframe2);

        isAnimating = true;
    }

    void Start()
    {
        UpdateView();
    }

    void Update()
    {
        if(CachedRectTransform.rect.width != currentViewRect.width || CachedRectTransform.rect.height != currentViewRect.height)
        {
            UpdateView();
        }
    }

    private void UpdateView()
    {
        currentViewRect = CachedRectTransform.rect;

        GridLayoutGroup grid = CachedScrollRect.content.GetComponent<GridLayoutGroup>();
        int paddingH = Mathf.RoundToInt((currentViewRect.width - grid.cellSize.x) / 2.0f);
        int paddingV = Mathf.RoundToInt((currentViewRect.height - grid.cellSize.y) / 2.0f);
        grid.padding = new RectOffset(paddingH, paddingH, paddingV, paddingV);
    }

    void LateUpdate()
    {
        if(isAnimating)
        {
            if(Time.time >= animationCurve.keys[animationCurve.length - 1].time)
            {
                CachedScrollRect.content.anchoredPosition = destPosition;
                isAnimating = false;
                return;
            }
        }
        Vector2 newPosition = initialPosition + (destPosition - initialPosition) * animationCurve.Evaluate(Time.time);
        cachedScrollRect.content.anchoredPosition = newPosition;
    }
}
