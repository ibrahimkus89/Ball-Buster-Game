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



    void Start()
    {
        ktsys = Balls.Length;
        BringTheBall();
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
    }

    void BringTheBall()
    {
        Balls[poolIndex].transform.SetParent(BallThrower.transform);
        Balls[poolIndex].transform.position = BallSocket.transform.position;
        Balls[poolIndex].SetActive(true);
    }
}
