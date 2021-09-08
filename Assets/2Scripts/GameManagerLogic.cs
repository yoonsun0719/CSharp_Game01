using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour
{
    public int totalItemCount;
    public int stage;
    public Text stageCountText; // 스테이지 카운트 (UI)
    public Text playerCountText;

    public void GetItem(int count)
    {
        playerCountText.text = count.ToString();
    }

    private void Awake()
    {
        stageCountText.text = "/ " + totalItemCount;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene(stage);
    }
}
