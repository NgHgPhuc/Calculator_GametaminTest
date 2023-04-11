using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HistoryExpressionPanel : MonoBehaviour
{
    Queue<string> HistoryExpression = new Queue<string>();
    public Transform ShowHistoryObject; //tranform UI

    public static HistoryExpressionPanel Instance { get; private set; }

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        gameObject.SetActive(false);
    }

    public void ClickOnBackground()
    {
        animator.Play("Close");
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }
    public void TurnOn()
    {
        gameObject.SetActive(true);
        animator.Play("Open");
    }

    public void AddHistoryExpression(string expression,string Result)
    {
        HistoryExpression.Enqueue(expression +" = "+ Result);
        if (HistoryExpression.Count == 9)
            HistoryExpression.Dequeue();

        int count = 0;
        foreach(Transform child in ShowHistoryObject)
        {
            count += 1;
            if (HistoryExpression.Count >= count)
            {
                child.gameObject.SetActive(true);
                string dequeue = HistoryExpression.Dequeue();
                child.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(dequeue);
                HistoryExpression.Enqueue(dequeue);

                continue;
            }
            child.gameObject.SetActive(false);
        }

    }
}
