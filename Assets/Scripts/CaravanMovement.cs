using UnityEngine;

public class CaravanMovement : MonoBehaviour
{
    public static CaravanMovement Instance;

    public float normalSpeed = 3f;
    public float slowSpeed = 1.2f;

    float currentSpeed;
    public bool isMoving = true;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentSpeed = normalSpeed;
    }

    void Update()
    {
        if (!isMoving) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0f, v);

        if (move.magnitude > 0.01f)
        {
            transform.Translate(move.normalized * currentSpeed * Time.deltaTime, Space.World);

            // 🧭 Direction face kare
            Quaternion rot = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 10f * Time.deltaTime);
        }
    }

    void LateUpdate()
    {
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = new Vector3(
                transform.position.x,
                hit.point.y,
                transform.position.z
            );
        }
    }

    // 🛑 Stop at dialogue
    public void StopAtNode(int stopNumber)
    {
        isMoving = false;
        DialogueManager.Instance.StartDialogue(stopNumber);
    }

    // ▶️ Resume after dialogue
    public void ResumeJourney()
    {
        isMoving = true;
        currentSpeed = normalSpeed;
    }

    // 🐢 Slow effect (memory trade etc.)
    public void SlowDown()
    {
        currentSpeed = slowSpeed;
    }
}
