using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public GameObject msgPrefab;
    public ScrollRect scrollRect;
    public TMP_InputField inputField;
    public Transform content;
    public int maxMsgs = 10;
    public Queue<GameObject> msgQueue = new Queue<GameObject>(capacity: 100);
    
    public void SendMsg()
    {
        string msg = inputField.text;
        inputField.text = "";
        if (string.IsNullOrEmpty(msg)) return;

        CreateMsg(msg);
    }

    void CreateMsg(string msg)
    {
        GameObject newMsg = Instantiate(msgPrefab, content);

        TMP_Text tmp = newMsg.GetComponent<TMP_Text>();
        tmp.text = msg;

        msgQueue.Enqueue(newMsg);
        if (msgQueue.Count > maxMsgs)
        {
            GameObject oldMsg = msgQueue.Dequeue();
            Destroy(oldMsg);
        }
    }
}
