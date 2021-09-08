using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower;
    public int itemCount;
    bool isJump;
    public GameManagerLogic manager;

    Rigidbody rigid;
    AudioSource audio;

    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true; 
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse); // �⺻���� �̵�
    }

   

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            isJump = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            //PlayerBall player = other.GetComponent<PlayerBall>();
            itemCount++;
            audio.Play();
            other.gameObject.SetActive(false);
            manager.GetItem(itemCount);
               
        }else if (other.tag == "Finish") //find ����� �ʿ���� ������ ����ȴ�.
        {
          if(itemCount == manager.totalItemCount)
            {
                // Game Clear && Next Stage
                if (manager.stage == 3) SceneManager.LoadScene(0); 
               
                //���Ӿ����� ������ ���� ���� ������ ������ �����ϸ� ���� Ŭ����
                //Game Clear
                else SceneManager.LoadScene(manager.stage + 1); 
            }
            else 
            {
                //���� �������� �� ������ ���ߴٸ� ����� 
                //Restart!
                SceneManager.LoadScene(manager.stage);
            }
        }
    }

}
