using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    [Header("Dialogues (Stop 1–4)")]
    public DialogueData[] stopDialogues; // MUST be size 4

    [Header("Final Dialogue (Stop 5)")]
    public DialogueData finalDialogue;

    [Header("Timed Trade")]
    public float tradeTimeLimit = 6f;

    private Coroutine tradeTimer;
    private Coroutine typingRoutine;

    private int currentStop;
    private NPCInteraction currentNPC;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // =========================
    // START DIALOGUE
    // =========================
    public void StartDialogue(int stopNumber, NPCInteraction npc = null)
    {
        currentStop = stopNumber;
        currentNPC = npc;

        dialoguePanel.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StopTradeTimer();
        StopTyping();

        // FINAL STOP
        if (stopNumber == 5 && finalDialogue != null)
        {
            PlayDialogue(finalDialogue);
            return;
        }

        // STOPS 1–4
        int index = stopNumber - 1;

        if (index < 0 || index >= stopDialogues.Length)
        {
            dialogueText.text = "The silence judges your hesitation.";
            return;
        }

        PlayDialogue(stopDialogues[index]);

        tradeTimer = StartCoroutine(TradeCountdown());
    }

    // =========================
    // PLAY TEXT + VOICE
    // =========================
    void PlayDialogue(DialogueData data)
    {
        StopTyping();
        typingRoutine = StartCoroutine(TypeTextWithVoice(data));
    }

    IEnumerator TypeTextWithVoice(DialogueData data)
    {
        dialogueText.text = "";

        float clipLength = 2f;

        if (currentNPC != null && data.voiceClip != null)
        {
            currentNPC.Speak(data.voiceClip);
            clipLength = data.voiceClip.length;
        }

        float delay = clipLength / Mathf.Max(1, data.text.Length);

        foreach (char c in data.text)
        {
            dialogueText.text += c;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueText.text = data.text;

                if (currentNPC != null)
                    currentNPC.StopSpeaking();

                yield break;
            }

            yield return new WaitForSecondsRealtime(delay);
        }
    }

    // =========================
    // TIMER
    // =========================
    IEnumerator TradeCountdown()
    {
        float time = tradeTimeLimit;

        while (time > 0f)
        {
            time -= Time.unscaledDeltaTime;
            yield return null;
        }

        RefuseTrade();
    }

    // =========================
    // TRADE MEMORY (RANDOM, HIDDEN)
    // =========================
    public void TradeMemory()
    {
        StopTradeTimer();
        StopTyping();

        if (TradeManager.Instance != null)
            TradeManager.Instance.TradeMemory();

        EndDialogue();
    }

    // =========================
    // REFUSE TRADE
    // =========================
    public void RefuseTrade()
    {
        StopTradeTimer();
        StopTyping();

        if (GameManager.Instance != null)
        {
            int loss = GameManager.Instance.hasTrust ? 1 : 2;
            GameManager.Instance.reputation -= loss;

            if (ScreenFeedback.Instance != null)
                ScreenFeedback.Instance.Show("Reputation -" + loss);
        }

        EndDialogue();
    }

    // =========================
    // END DIALOGUE
    // =========================
    void EndDialogue()
    {
        if (currentNPC != null)
            currentNPC.StopSpeaking();

        currentNPC = null;

        dialoguePanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (currentStop == 5 && EndingManager.Instance != null)
            EndingManager.Instance.CheckEnding();
    }

    void StopTradeTimer()
    {
        if (tradeTimer != null)
        {
            StopCoroutine(tradeTimer);
            tradeTimer = null;
        }
    }

    void StopTyping()
    {
        if (typingRoutine != null)
        {
            StopCoroutine(typingRoutine);
            typingRoutine = null;
        }
    }
}
