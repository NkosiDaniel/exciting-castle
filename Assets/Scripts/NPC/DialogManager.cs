using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.Events;
using TMPro;
using UnityEditor.Search;
using UnityEngine.UI;
using UnityEngine.Animations;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogParent;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private Button option1Button;
    [SerializeField] private Button option2Button;
    [SerializeField] private float typingSpeed = 0.05f;

    private List<dialogString> dialogList;
    private RigidbodyConstraints2D originalConstraints;

    [Header("Player")]
    [SerializeField] private TopDownController playerController;
    private Transform playerCamera;
    private int currentDialogIndex = 0;

    private void Start()
    {
        originalConstraints = playerController.GetComponent<Rigidbody2D>().constraints;
        dialogParent.SetActive(false);
    }

    public void DialogStart(List<dialogString> textToPrint)
    {
        dialogParent.SetActive(true);
        playerController.enabled = false;
        playerController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

        dialogList = textToPrint;
        currentDialogIndex = 0;

        DisableButtons();
        StartCoroutine(PrintDialog());

    }

    private void DisableButtons()
    {
        option1Button.interactable = false;
        option2Button.interactable = false;

        option1Button.GetComponentInChildren<TMP_Text>().text = "";
        option2Button.GetComponentInChildren<TMP_Text>().text = "";
    }

    private bool optionSelected = false;

    private IEnumerator PrintDialog()
    {
        while (currentDialogIndex < dialogList.Count)
        {
            dialogString line = dialogList[currentDialogIndex];

            line.startDialogEvent?.Invoke();

            if (line.isQuestion)
            {
                yield return StartCoroutine(TypeText(line.text));

                option1Button.interactable = true;
                option2Button.interactable = true;

                option1Button.GetComponentInChildren<TMP_Text>().text = line.answerOption1;
                option2Button.GetComponentInChildren<TMP_Text>().text = line.answerOption2;

                option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump));
                option2Button.onClick.AddListener(() => HandleOptionSelected(line.option2IndexJump));

                yield return new WaitUntil(() => optionSelected);

            }
            else
            {
                yield return StartCoroutine(TypeText(line.text));
            }
            line.startDialogEvent?.Invoke();

            optionSelected = false;
        }
        DialogStop();
    }

    private void HandleOptionSelected(int indexJump) 
    {
        optionSelected = true;
        DisableButtons();

        currentDialogIndex = indexJump;
    }

    private IEnumerator TypeText(string text) 
    {
        dialogText.text = "";
        foreach(char letter in text.ToCharArray()) 
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if(!dialogList[currentDialogIndex].isQuestion) 
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        if(dialogList[currentDialogIndex].isEnd)
          DialogStop();  

          currentDialogIndex++;
    }

    private void DialogStop() 
    {
        StopAllCoroutines();
        dialogText.text = " ";
        dialogParent.SetActive(false);

        playerController.enabled = true;
        playerController.GetComponent<Rigidbody2D>().constraints = originalConstraints;
    }
}
