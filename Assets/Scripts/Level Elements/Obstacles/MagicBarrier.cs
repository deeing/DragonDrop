using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagicBarrier : MonoBehaviour
{
    [SerializeField]
    private int numHits = 5;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Color targetColor;
    [SerializeField]
    private float changeDuration = .2f;

    private int hp;
    private Color originalColor;

    private void Awake()
    {
        hp = numHits;
        originalColor = spriteRenderer.color;
    }

    public void TakeHit()
    {
        hp--;

        if (hp <= 0)
        {
            Disappear();
        } else
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(spriteRenderer.DOColor(targetColor, changeDuration));
            mySequence.Append(spriteRenderer.DOColor(originalColor, changeDuration));
        }
    }

    private void Disappear()
    {
        spriteRenderer.DOFade(0f, 1f).OnComplete(Disable);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
