using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] GameObject controlButton;
    [SerializeField] Text dialogText;
    [SerializeField] Text nameText;

    [SerializeField]
    int lettersPerSecond;

    public event Action OnShowDialog;
    public event Action OnHideDialog;
    public static DialogManager Instance
    {
        get; private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    Dialog dialog;
    int currentLine = 0;
    bool isTyping;
    

    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();

       
        this.dialog = dialog;
        controlButton.SetActive(false);
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0], dialog.Name));
    }

    public void HandleUpdate()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0)) && !isTyping)
        {
            ++currentLine;
            if (currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine], dialog.Name));
            }
            else
            {
                dialogBox.SetActive(false);
                currentLine = 0;
                OnHideDialog?.Invoke();
                controlButton.SetActive(true);
            }
        }
    }

    public IEnumerator TypeDialog(string line, string name)
    {
        isTyping = true;
        dialogText.text = "";
        nameText.text = name;
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }
}
