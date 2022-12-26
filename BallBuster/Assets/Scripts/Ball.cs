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

    private bool primary;

    void Start()
    {
        numberText.text = number.ToString();
    }

    void SetStatus()
    {
        primary = true;
    }


    public void changeThePrimaryStatus()
    {
        Invoke("SetStatus",2f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(number.ToString()) && primary)
        {
            BrlEffect.Play();
            collision.gameObject.SetActive(false);
            number += number;
            gameObject.tag = number.ToString();
            numberText.text = number.ToString();
            // sprite change

            switch (number)
            {
               
                 case 4:
                    _renderer.sprite = _GameManager.spriteObjects[1];
                    break;

                 case 8:
                     _renderer.sprite = _GameManager.spriteObjects[2];
                     break;
                 case 16:
                     _renderer.sprite = _GameManager.spriteObjects[3];
                     break;
                 case 32:
                     _renderer.sprite = _GameManager.spriteObjects[4];
                     break;
                 case 64:
                     _renderer.sprite = _GameManager.spriteObjects[5];
                     break;
                 case 128:
                     _renderer.sprite = _GameManager.spriteObjects[6];
                     break;
                 case 256:
                     _renderer.sprite = _GameManager.spriteObjects[7];
                     break;
                 case 512:
                case 1024:
                case 2048:
                     _renderer.sprite = _GameManager.spriteObjects[8];
                     break;
                 
            }

            primary =false;
            Invoke("SetStatus",2f);
        }
    }
}
