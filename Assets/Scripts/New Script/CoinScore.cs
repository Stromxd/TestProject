using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoinScore : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public bool DoubleScore = false;
    public GameObject player;
    public Rigidbody rb;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fruit" && DoubleScore == false)
        {
            //Add count or give points etc etc.
            score = score + 1;
            scoreText.text = ((int)score).ToString();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "DoubleScore" || DoubleScore == true)
        {
            DoubleScore = true;
            Destroy(other.gameObject);
            StartCoroutine(ActivateDoubleCoin());
            if (other.gameObject.tag == "Fruit" && DoubleScore == true)
            {
                score = score + 2;
                scoreText.text = ((int)score).ToString();
            }
        }
    }
    IEnumerator ActivateDoubleCoin()
    {
        yield return new WaitForSeconds(6f);
        DoubleScore = false;
    }
    IEnumerator ActivateFlying()
    {
        yield return new WaitForSeconds(4f);
        //Flying = false;
    }
}