using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ScrollButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private enum ScrollButtonDirection { UP, DOWN }

    [SerializeField] private ScrollButtonDirection direction;
    [SerializeField] private float stepSize;
    [SerializeField] private float scrollFrequency;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private ScrollElementsContainer scrollElementsContainer;

    private float signedStepSize;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        signedStepSize = direction == ScrollButtonDirection.DOWN ? stepSize * -1 : stepSize;
        scrollbar.onValueChanged.AddListener(val => HandleScrollViewChanged());
        scrollElementsContainer.OnContainerChildrenChanged += HandleScrollViewChanged;
    }

    /// <summary>
    /// Handle if the scroll view child count is changed.
    /// </summary>
    private void HandleScrollViewChanged()
    {
        if(Mathf.Approximately(scrollbar.size, 1f))
        {
            SetButtonState(false);
        }
        else
        {
            HandleScrollValueChanged(scrollbar.value);
        }
    }

    /// <summary>
    /// Handle the scroll bar value changed.
    /// </summary>
    /// <param name="value"></param>
    private void HandleScrollValueChanged(float value)
    {
        value = value.RoundDecimalPlaces(2);
        if(direction == ScrollButtonDirection.DOWN && Mathf.Approximately(value, 0f))
        {
            SetButtonState(false);
        }
        else if(direction == ScrollButtonDirection.UP && Mathf.Approximately(value, 1f))
        {
            SetButtonState(false);
        }
        else
        {
            SetButtonState(true);
        }
    }

    /// <summary>
    /// Set the scroll button interactable state.
    /// </summary>
    /// <param name="enabled"></param>
    private void SetButtonState(bool enabled)
    {
        button.interactable = enabled;
        if(!enabled)
        {
            CancelInvoke("ScrollContent");
            StopAllCoroutines();
        }
    }

    /// <summary>
    /// Keep on scrolling as long as the user holds down the button.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if(button.IsInteractable())
        {
            InvokeRepeating("ScrollContent", 0f, scrollFrequency);
        }
    }

    /// <summary>
    /// Immediately stop the scrolling as the user leaves the button.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("ScrollContent");
        StopAllCoroutines();
    }

    /// <summary>
    /// Start scrolling the content.
    /// </summary>
    private void ScrollContent()
    {
        StopAllCoroutines();
        StartCoroutine(SmoothScrolling(scrollbar.value,
            Mathf.Clamp01(scrollbar.value + (signedStepSize * scrollbar.size.RoundDecimalPlaces(2))),
            scrollFrequency));
    }

    /// <summary>
    /// Smoothly scroll the content.
    /// </summary>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <param name="totalTime"></param>
    /// <returns></returns>
    private IEnumerator SmoothScrolling(float minValue, float maxValue, float totalTime)
    {
        float time = 0f;
        float normalizedTime = 0f;
        totalTime = Mathf.Abs(maxValue - minValue) * totalTime;
        while(time <= totalTime)
        {
            normalizedTime = time / totalTime;
            scrollbar.value = Mathf.Lerp(minValue, maxValue, normalizedTime);
            time += totalTime * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnDestroy()
    {
        scrollbar.onValueChanged.RemoveAllListeners();
        scrollElementsContainer.OnContainerChildrenChanged -= HandleScrollViewChanged;
    }
}
