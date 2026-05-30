using System.Collections;
using TMPro;
using UnityEngine;

public enum DialogueSpeaker
{
    PatoJr,
    SrP
}

public class DialogueCanvas : MonoBehaviour
{
    [SerializeField] private GameObject patoJrDialogueGroup;
    [SerializeField] private GameObject srPDialogueGroup;
    [SerializeField] private TMP_Text patoJrDialogueText;
    [SerializeField] private TMP_Text srPDialogueText;

    private Coroutine hideCoroutine;

    private void Awake()
    {
        ResolveReferences();
        HideDialogue();
    }

    private void Reset()
    {
        ResolveReferences();
    }

    public void ShowDialogue(DialogueSpeaker speaker, string text, float durationSeconds)
    {
        ResolveReferences();
        HideDialogue();

        GameObject dialogueGroup = GetDialogueGroup(speaker);
        TMP_Text dialogueText = GetDialogueText(speaker);

        if (dialogueText != null)
        {
            dialogueText.text = text;
        }

        if (dialogueGroup != null)
        {
            dialogueGroup.SetActive(true);
        }

        if (durationSeconds > 0)
        {
            hideCoroutine = StartCoroutine(HideAfterDelay(durationSeconds));
        }
    }

    public void HideDialogue()
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }

        if (patoJrDialogueGroup != null)
        {
            patoJrDialogueGroup.SetActive(false);
        }

        if (srPDialogueGroup != null)
        {
            srPDialogueGroup.SetActive(false);
        }
    }

    private GameObject GetDialogueGroup(DialogueSpeaker speaker)
    {
        return speaker == DialogueSpeaker.PatoJr ? patoJrDialogueGroup : srPDialogueGroup;
    }

    private TMP_Text GetDialogueText(DialogueSpeaker speaker)
    {
        return speaker == DialogueSpeaker.PatoJr ? patoJrDialogueText : srPDialogueText;
    }

    private void ResolveReferences()
    {
        if (patoJrDialogueGroup == null)
        {
            patoJrDialogueGroup = FindChildGameObject("PatoJrDialogueGroup");
        }

        if (srPDialogueGroup == null)
        {
            srPDialogueGroup = FindChildGameObject("SrPDialogueGroup");
        }

        if (patoJrDialogueText == null)
        {
            patoJrDialogueText = FindDialogueText(patoJrDialogueGroup, "DialogueTextPJR");
        }

        if (srPDialogueText == null)
        {
            srPDialogueText = FindDialogueText(srPDialogueGroup, "DialogueTextSrP");
        }
    }

    private GameObject FindChildGameObject(string childName)
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child.name == childName)
            {
                return child.gameObject;
            }
        }

        return null;
    }

    private TMP_Text FindDialogueText(GameObject dialogueGroup, string textObjectName)
    {
        if (dialogueGroup == null)
        {
            return null;
        }

        TMP_Text[] textComponents = dialogueGroup.GetComponentsInChildren<TMP_Text>(true);

        foreach (TMP_Text textComponent in textComponents)
        {
            if (textComponent.name == textObjectName)
            {
                return textComponent;
            }
        }

        return textComponents.Length > 0 ? textComponents[0] : null;
    }

    private IEnumerator HideAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        HideDialogue();
    }
}
