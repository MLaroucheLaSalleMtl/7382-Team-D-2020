﻿

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Block_Behavior : MonoBehaviour
{

    private enum ColliderChoice { Collision, Trigger };

    [Header("Set Block visibility")]
    [SerializeField] private bool startVisible = true;

    [Tooltip("Number of times before the trap gets triggered")]
    [SerializeField] private int triggerCounter = 0;
    [SerializeField] private float trapActivationDelay = 0f;
    [SerializeField] private ColliderChoice colliderType = ColliderChoice.Collision;

    private SpriteRenderer sprite = null;
    private BoxCollider2D boxColl = null;

    private int counter = 0;

    public UnityEvent OnTrapActivation;

    private void Awake()
    {
        if(boxColl) GetComponent<BoxCollider2D>();
        if(sprite) GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (boxColl) SetCollderTrigger();
        if (sprite) SetSpriteEnable();
    }

    private void SetSpriteEnable()
    {
        if (!startVisible)
        {
            sprite.enabled = false;
        }
        else
        {
            sprite.enabled = true;
        }
    }

    private void SetCollderTrigger()
    {
        if (colliderType == ColliderChoice.Trigger) boxColl.isTrigger = true;
        else boxColl.isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (counter == triggerCounter)
        {
            Invoke(nameof(ActivateTrap), trapActivationDelay);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (counter == triggerCounter)
        {
            boxColl.isTrigger = false;
            Invoke(nameof(ActivateTrap), trapActivationDelay);
        }
        else
        {
            counter++;
        }
    }
}
