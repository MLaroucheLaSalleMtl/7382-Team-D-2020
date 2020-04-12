using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class SpeedBoost : MonoBehaviour
{

    [SerializeField] private Sprite[] animSprites = null;
    private SpriteRenderer render = null;
    private int animSpritesPointer = 0;
    [SerializeField] private float force = 5f;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();

        StartCoroutine(IEnumPlayAnim());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().AddForce(transform.right * force, ForceMode2D.Impulse);
            GetComponent<AudioSource>().Play();
        }
    }

    private IEnumerator IEnumPlayAnim()
    {
        while (true)
        {
            render.sprite = animSprites[animSpritesPointer];
            yield return new WaitForSeconds(0.15f);

            if (animSpritesPointer == animSprites.Length - 1) animSpritesPointer = 0;
            else animSpritesPointer++;
        }
    }
}
