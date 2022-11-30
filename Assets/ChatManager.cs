using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class ChatManager : NetworkBehaviour
{
    public readonly SyncList<string> messages = new SyncList<string>();
    public GameObject messagePrefab;
    public Transform messageHolder;
    private List<GameObject> oldMessages = new List<GameObject>();
    public TMP_InputField input;
    public VerticalLayoutGroup layoutGroup;
    private bool selected = false;

    private void Start()
    {
        messages.Callback += DisplayMessages;
        input.onSelect.AddListener(OnSelect);
        input.onDeselect.AddListener(OnDeSelect);
    }

    [Command(requiresAuthority = false)]
    public void SendMessage(string text)
    {
        messages.Add(text);
    }

    public void SendInput()
    {
        SendMessage(input.text);
    }

    public void DisplayMessages(SyncList<string>.Operation op, int index, string oldItem, string newItem)
    {
        //oldMessages.Clear();

        GameObject temp = Instantiate(messagePrefab, messageHolder);
        temp.GetComponent<TextMeshProUGUI>().text = newItem;
        oldMessages.Add(temp);
    }

    public void OnSelect(string text)
    {
        selected = true;
    }

    public void OnDeSelect(string text)
    {
        selected = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (selected)
            {
                input.onSubmit.Invoke(input.text);
                input.text = "";
                input.onDeselect.Invoke(input.text);
            }
            else
            {
                input.Select();
            }
        }
    }
}