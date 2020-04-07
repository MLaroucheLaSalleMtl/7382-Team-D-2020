
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Block_Behavior : MonoBehaviour
{

    private enum ColliderChoice { Collision, Trigger };

    [Header("Set Block visibility")]
    [SerializeField] private bool startVisible;

    [Tooltip("Number of times before the trap gets triggered")]
    [SerializeField] private int triggerCounter;
    [SerializeField] private float trapActivationDelay;
    [SerializeField] private ColliderChoice colliderType;
    private int counter;

    [HideInInspector] private SpriteRenderer sprite;

    private BoxCollider2D boxColl;

    public UnityEvent OnTrapActivation;

    private void Awake()
    {
        boxColl = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (colliderType.Equals(ColliderChoice.Trigger)) boxColl.isTrigger = true;
        else boxColl.isTrigger = false;

        if (!startVisible)
        {
            sprite.enabled = false;
        }
        else
        {
            sprite.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (counter == triggerCounter)
        {
            Invoke("ActivateTrap", trapActivationDelay);
        }
        else
        {
            counter++;
        }
        
    }

    private void ActivateTrap()
    {
        OnTrapActivation?.Invoke();

        if (startVisible)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            sprite.enabled = true;
            this.gameObject.SetActive(true);
        }
    }

    //Can make a trigger for this too;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (counter == triggerCounter)
        {
            Invoke("ActivateTrap", trapActivationDelay);
            boxColl.isTrigger = false;
        }
        else
        {
            counter++;
        }
    }
}
