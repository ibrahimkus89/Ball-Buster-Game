using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] GameManager _GameManager;
    List<Collider2D> colliders = new List<Collider2D>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(number.ToString()))
        {
            ApplyForce();
        }
    }
    void ApplyForce()
    {
        var contactFilter2D = new ContactFilter2D
        {
            useTriggers = true
        };

        Physics2D.OverlapBox(transform.position, transform.localScale * 2, 20f,contactFilter2D,colliders);

        _GameManager.BombEffect(transform.position);
        gameObject.SetActive(false);

        foreach (var item in colliders)
        {
            if (item.gameObject.CompareTag("Box"))
            {
                item.GetComponent<Box>().PlayEffect();
            }
            else
                item.gameObject.GetComponent<Rigidbody2D>().AddForce(90 * new Vector2(0, 6), ForceMode2D.Force);

        }

    }
    }
    

