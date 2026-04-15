using UnityEngine;
using UnityEngine.UI;

public class MapUIManager : MonoBehaviour
{
    public static MapUIManager Instance;

    public GameObject mapPanel;
    public Image mapImage;
    public Material normalMat;
    public Material blurMat;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        mapPanel.SetActive(false);
        mapImage.material = normalMat;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapPanel.SetActive(!mapPanel.activeSelf);
        }
    }

    public void ApplyBlur()
    {
        mapImage.material = blurMat;
    }
}
