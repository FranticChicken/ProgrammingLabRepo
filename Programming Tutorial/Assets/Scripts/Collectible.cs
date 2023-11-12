using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectible : MonoBehaviour
{
    [SerializeField] public Player player; 
    
    //static int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        //get score text transform
        

        //playerScore = player.ReturnScore();

        //text says score is 0 at beginning 
        

    }

   


    void OnTriggerEnter(Collider coin)
        {
        if (coin.transform.tag == "Player")
            {
            Destroy(gameObject);
            Debug.Log("destroyed game object");
            
            player.AddOneToScore();
            player.ReprintScore();
            }
        }

    


    // Update is called once per frame
    void Update()
    {

    }
}
