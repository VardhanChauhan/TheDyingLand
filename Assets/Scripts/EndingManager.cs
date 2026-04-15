using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;

    [Header("UI")]
    public GameObject endingPanel;
    public TMP_Text endingText;

    [Header("Ending Icons")]
    public Image iconShadow;
    public Image iconStar;
    public Image iconBroken;
    public Image iconSingle;
    public Image iconHub;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void CheckEnding()
    {
        if (endingPanel == null || endingText == null)
        {
            Debug.LogError("EndingManager: UI not assigned!");
            return;
        }

        endingPanel.SetActive(true);
        DisableAllIcons();

        var gm = GameManager.Instance;
        var mm = MemoryManager.Instance;

        if (gm == null || mm == null)
            return;

        bool hasStrength = mm.HasMemory("Strength");
        bool hasHealing = mm.HasMemory("Healing");

        // 5️⃣ GOLDEN MERCHANT
        if (gm.crewCount >= 3 && gm.reputation >= 1 && mm.memoryCount >= 2)
        {
            Show(iconHub, "At the crossroads of the end, you built a new beginning.");
            return;
        }

        // 4️⃣ MARTYR
        if (gm.crewCount > 0 && (!hasStrength || !hasHealing))
        {
            Show(iconStar, "Your memories faded so that theirs could begin.");
            return;
        }

        // 1️⃣ EMPTY VESSEL
        if (gm.crewCount > 0 && mm.memoryCount <= 1)
        {
            Show(iconShadow, "The world survived, but you are a stranger to it.");
            return;
        }

        // 2️⃣ LONE WANDERER
        if (gm.crewCount == 0 && mm.memoryCount >= 3)
        {
            Show(iconSingle, "You remember everything, but there is no one left.");
            return;
        }

        // 3️⃣ BROKEN COMPASS
        Show(iconBroken, "The destination was forgotten, but the journey became home.");
    }


    void Show(Image icon, string text)
    {
        if (icon != null)
            icon.gameObject.SetActive(true);

        endingText.text = text;
    }

    void DisableAllIcons()
    {
        if (iconShadow != null) iconShadow.gameObject.SetActive(false);
        if (iconStar != null) iconStar.gameObject.SetActive(false);
        if (iconBroken != null) iconBroken.gameObject.SetActive(false);
        if (iconSingle != null) iconSingle.gameObject.SetActive(false);
        if (iconHub != null) iconHub.gameObject.SetActive(false);
    }
}
