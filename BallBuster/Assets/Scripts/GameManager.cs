using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;


[Serializable]
public class Targets
{
    public Sprite TargetImage;
    public int BallValue;
    public GameObject MissonCompleted;
    public string TargetType;
}

[Serializable]
public class Targets_UI
{
    public GameObject Target;
    public  Image TargetImage;
    public TextMeshProUGUI TargetValueText;
    public GameObject MissonCompleted;

}

public class GameManager : MonoBehaviour
{
    public Sprite[] spriteObjects;
    [SerializeField] private GameObject[] Balls;
    [SerializeField] private TextMeshProUGUI ktsText;
    private int ktsys;
    private int poolIndex;

    [Header("---Other Objects")] 
    [SerializeField] private ParticleSystem bombEffect;
    [SerializeField] private ParticleSystem[] boxExplosionEffects;
    private int BoxExplosionEffectIndex;






    [Header("---Ball Shooting System")]
    [SerializeField] private GameObject BallThrower;
    [SerializeField] private GameObject BallSocket;
    [SerializeField] private GameObject NextBall;
    private GameObject SelectedBall;

    [Header("---Mission Process")]
    [SerializeField] private List<Targets_UI> Targets_UI;
    [SerializeField] private List<Targets> Targets;

    void Start()
    {
        ktsys = Balls.Length;
        BringTheBall(true);

        for (int i = 0; i < Targets.Count; i++)
        {
            Targets_UI[i].Target.SetActive(true);
            Targets_UI[i].TargetImage.sprite = Targets[i].TargetImage;
            Targets_UI[i].TargetValueText.text = Targets[i].BallValue.ToString();

        }
    }

    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction);

            if (hit.collider!=null)
            {
                if (hit.collider.gameObject.CompareTag("GameGround"))
                { 
                    Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    BallThrower.transform.position = Vector2.MoveTowards(BallThrower.transform.position,new Vector2(MousePosition.x,BallThrower.transform.position.y),30 * Time.deltaTime);
                }
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            SelectedBall.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            SelectedBall.transform.parent = null;
            SelectedBall.GetComponent<Ball>().changeThePrimaryStatus();
            BringTheBall(false);

        }
    }

    void BringTheBall(bool firstKrlm)
    {
        if (firstKrlm)
        {
            Balls[poolIndex].transform.SetParent(BallThrower.transform);
            Balls[poolIndex].transform.position = BallSocket.transform.position;
            Balls[poolIndex].SetActive(true);
            SelectedBall = Balls[poolIndex];

            poolIndex++;
            Balls[poolIndex].transform.position = NextBall.transform.position;
            Balls[poolIndex].SetActive(true);
            ktsText.text = ktsys.ToString();
        }
        else
        {
            if (Balls.Length!=0)
            {
                Balls[poolIndex].transform.SetParent(BallThrower.transform);
                Balls[poolIndex].transform.position = BallSocket.transform.position;
                Balls[poolIndex].SetActive(true);
                SelectedBall = Balls[poolIndex];

                ktsys--;
                ktsText.text = ktsys.ToString();

                if (poolIndex == Balls.Length-1)
                {
                    Debug.Log("It is Over");
                }
                else
                {
                    poolIndex++;
                    Balls[poolIndex].transform.position = NextBall.transform.position;
                    Balls[poolIndex].SetActive(true);
                }
            }
        }

       

    }


    public void BombEffect(Vector2 position)
    {
        bombEffect.gameObject.transform.position = position;
        bombEffect.gameObject.SetActive(true);
        bombEffect.Play();
    }

    public void BoxPrcEffect(Vector2 position)
    {
        

        boxExplosionEffects[BoxExplosionEffectIndex].gameObject.transform.position = position;
        boxExplosionEffects[BoxExplosionEffectIndex].gameObject.SetActive(true);
        bombEffect.Play();
        BoxExplosionEffectIndex++;

        if (BoxExplosionEffectIndex==boxExplosionEffects.Length-1)
        {
            BoxExplosionEffectIndex = 0;
        }
        else
        {
            BoxExplosionEffectIndex++;
        }
    }
}
