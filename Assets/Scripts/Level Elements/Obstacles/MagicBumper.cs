using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagicBumper : MonoBehaviour
{
    [SerializeField]
    private Color targetColor;
    [SerializeField]
    private float changeDuration = .2f;

    private SpriteRenderer spriteRenderer;
    private List<MagicBarrier> magicBarriers;
    private Color originalColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        RegisterBarriers();
    }

    private void RegisterBarriers()
    {
        magicBarriers = new List<MagicBarrier>();
        GameObject[] barrierObjects = GameObject.FindGameObjectsWithTag("MagicBarrier");

        foreach(GameObject barrierObj in barrierObjects)
        {
            MagicBarrier barrier = barrierObj.GetComponent<MagicBarrier>();
            magicBarriers.Add(barrier);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(spriteRenderer.DOColor(targetColor, changeDuration));
        mySequence.Append(spriteRenderer.DOColor(originalColor, changeDuration));

        foreach(MagicBarrier barrier in magicBarriers)
        {
            barrier.TakeHit();
        }
    }
}
