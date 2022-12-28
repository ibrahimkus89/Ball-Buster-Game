using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] GameManager _GameManager;

    public void PlayEffect()
    {
        _GameManager.BoxPrcEffect(transform.position);
        gameObject.SetActive(false);
    }
}
