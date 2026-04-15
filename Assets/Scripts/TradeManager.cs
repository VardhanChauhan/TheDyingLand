using UnityEngine;

public class TradeManager : MonoBehaviour
{
    public static TradeManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void TradeMemory()
    {
        var mm = MemoryManager.Instance;
        if (mm == null) return;

        mm.LoseRandomMemory();

        // 🔹 subtle feedback only
        if (ScreenFeedback.Instance != null)
        {
            ScreenFeedback.Instance.Show("Something slips away...");
        }
    }

    public void RefuseTrade()
    {
        var gm = GameManager.Instance;
        if (gm != null)
        {
            gm.reputation -= 1;
        }

        if (ScreenFeedback.Instance != null)
        {
            ScreenFeedback.Instance.Show("You chose to remember.");
        }
    }
}
