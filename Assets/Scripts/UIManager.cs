using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button nextButton;
    public Button prevButton;
    public GameObject[] pages = new GameObject[3];
    public Stack<int> prevStack = new Stack<int>(capacity: 100){};
    public Stack<int> nextStack = new Stack<int>(capacity: 100){};

    void Awake()
    {
        prevStack.Push(0);
        CheckButtonsAwake();
    }

    public void TraverseToPage(int index)
    {
        index--;
        Debug.Log($"Page index: {index}");
    
        int prev = -1;

        if (prevStack.Count > 0)
        {
            nextStack.Clear();
            prev = prevStack.Peek();
        }
    
        LoadPage(index, prev);
        prevStack.Push(index);

        CheckButtonsAwake();
    }

    void LoadPage(int index, int unloadIndex)
    {
        if (unloadIndex != -1)
        {
            pages[unloadIndex].SetActive(false);
            Debug.Log($"Prev stack: {unloadIndex}");
        }
        pages[index].SetActive(true);
    }

    public void NavBack()
    {
        Debug.Log($"Prev stack size: {prevStack.Count}");
        if (prevStack.Count <= 1) return;

        int current = prevStack.Pop();
        nextStack.Push(current);

        LoadPage(index: prevStack.Peek(), unloadIndex: current);

        CheckButtonsAwake();
    }

    public void NavForward()
    {
        if (nextStack.Count == 0) return;

        int next = nextStack.Pop();
        LoadPage(index: next, unloadIndex: prevStack.Peek());
        prevStack.Push(next);

        CheckButtonsAwake();
    }

    void CheckButtonsAwake()
    {
        nextButton.interactable = (nextStack.Count == 0) ? 
        false : true;

        prevButton.interactable = (prevStack.Count <= 1) ? 
        false : true;
    }
}
