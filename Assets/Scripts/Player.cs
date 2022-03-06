using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float force = 10f;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] SpriteRenderer spriteColor;
    [SerializeField] Color[] playerColor;
    [SerializeField] Collider2D baseCollider;
    [SerializeField] GameObject thankyouPanel;
    [SerializeField] GameObject effect;
    [SerializeField] AudioSource audioSource;
    string color;

    int index;

    // Start is called before the first frame update
    void Start()
    {
        ColorAtStart();
        thankyouPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        JumbMethod();
    }
     
    public void JumbMethod()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            audioSource.Play();
            rigidBody.velocity = Vector2.up * force;
            baseCollider.isTrigger = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "change")
        {
            Debug.Log("change");
            ColorAtStart();
            Destroy(other.gameObject);
            return;
        }

        if (other.tag == "OffTrigger")
        {
            baseCollider.isTrigger = false;
            return;
        }

        if (other.tag == "EndGame")
        {
            thankyouPanel.SetActive(true);
            spriteColor.color = new Color(0, 0, 0, 0);
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            return;
        }

        if (other.tag != color)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            spriteColor.color = new Color(0, 0, 0, 0);
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(Waiting());
        }

    }

    public void ColorAtStart()
    {
        index = Random.Range(0, 4);

        switch (index)
        {
            case 0:
                color = "blue";
                spriteColor.color = playerColor[0];
                break;
            case 1:
                color = "yellow";
                spriteColor.color = playerColor[1];
                break;
            case 2:
                color = "purple";
                spriteColor.color = playerColor[2];
                break;
            case 3:
                color = "pink";
                spriteColor.color = playerColor[3];
                break;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1f);
        RestartGame();
    }
}
