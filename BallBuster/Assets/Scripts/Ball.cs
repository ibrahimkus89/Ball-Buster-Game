using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Ball : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] GameManager _GameManager;
    [SerializeField] private ParticleSystem BrlEffect;
    [SerializeField] private SpriteRenderer _renderer;



    void Start()
    {
        numberText.text = number.ToString();
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(number.ToString()))
        {
            BrlEffect.Play();
            collision.gameObject.SetActive(false);
            number += number;
            gameObject.tag = number.ToString();
            numberText.text = number.ToString();
            // sprite change
        }
    }
}
