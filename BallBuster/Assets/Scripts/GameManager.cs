using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("---Level Settings")]
    public Sprite[] spriteObjects;
    [SerializeField] private GameObject[] Balls;
    [SerializeField] private TextMeshProUGUI ktsText;
    private int ktsys;
    private int poolIndex;

    [Header("---Ball Shooting System")]
    [SerializeField] private GameObject BallThrower;
    [SerializeField] private GameObject BallSocket;
    [SerializeField] private GameObject NextBall;
    private GameObject SelectedBall;



    void Start()
    {
        ktsys = Balls.Length;
        BringTheBall(true);
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
}
