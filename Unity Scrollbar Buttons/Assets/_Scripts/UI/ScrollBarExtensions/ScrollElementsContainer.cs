using System;
using UnityEngine;

public class ScrollElementsContainer : MonoBehaviour
{
    public Action OnContainerChildrenChanged;

    private void OnTransformChildrenChanged()
    {
        if(OnContainerChildrenChanged != null)
        {
            OnContainerChildrenChanged.Invoke();
        }
    }
}
