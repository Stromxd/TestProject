using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScore : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public bool DoubleScore = false;
    public AudioSource CoinAudio;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fruit" && DoubleScore == false)
        {
            //Add count or give points etc etc.
            CoinAudio.Play();
            score = score + 1;
            scoreText.text = ((int)score).ToString();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "DoubleScore" || DoubleScore == true)
        {
            DoubleScore = true;
            CoinAudio.Play();
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            StartCoroutine(ActivateDoubleCoin());
            if (other.gameObject.tag == "Fruit" && DoubleScore == true)
            {
                CoinAudio.Play();
                score = score + 2;
                scoreText.text = ((int)score).ToString();
            }
        }
    }
    IEnumerator ActivateDoubleCoin()
    {
        yield return new WaitForSeconds(15f);
        DoubleScore = false;
    }
}