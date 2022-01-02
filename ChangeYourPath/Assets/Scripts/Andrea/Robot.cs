using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Robot : MonoBehaviour
{
    public Animator animator;
    private BoxCollider2D boxCollider;
    public GameObject movePoint;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    public Transform Kvothe;
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 movement;
    public GameObject gameOverCanvas;
    public GameObject robotDialogueCanvas;
    public Dialogue dialogue;
    private Coroutine talk;
    private bool started = false;
    public bool isQuestGiver;
    public RobotQuest robotQuest;
    public int countdownTime;
    public GameObject countdownCanvas;
    public TMP_Text countdownDisplay, titleCanvas, subtitleCanvas;
    public AudioManager audioManager;
    public GameObject mp1;

    public GameObject mapCollectable;
    // Start is called before the first frame update
    void Start()
    {   
        boxCollider = movePoint.GetComponent<BoxCollider2D>();
        rb = this.GetComponent<Rigidbody2D>();
        Debug.Log("ROBOT SVEGLIO");

        if (!robotQuest.isComplete)
        {
            this.gameObject.transform.position = new Vector3(mp1.transform.position.x, mp1.transform.position.y + 6, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Kvothe.position - transform.position;
        direction.Normalize();
        movement = direction;
    }
    private void FixedUpdate() {
        if (moveSpeed != 0) {
            Vector2 coordinates = getNormalizedMovement(movement);
            moveCharacter(coordinates);

            float x = movement.x;
            float y = movement.y;
            //Reset Move Delta
            moveDelta = Vector3.zero;
            moveDelta = new Vector3(x, y, 0);

            animator.SetFloat("Horizontal", x);
            animator.SetFloat("Vertical", y);
            animator.SetFloat("Speed", moveDelta.sqrMagnitude);
        } else {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", 0);
        }
    }

    Vector2 getNormalizedMovement(Vector2 movement){
        if (movement.x > 0.2f && movement.x < 1f) movement.x = 1f;
        else if (movement.x > -1f && movement.x < -0.2f) movement.x = -1f;
        else if (movement.x > -0.2f && movement.x < 0.2f) movement.x = 0f;

        if (movement.y > 0.2f && movement.y < 1f) movement.y = 1f;
        else if (movement.y > -1f && movement.y < -0.2f) movement.y = -1f;
        else if (movement.y > -0.2f && movement.y < 0.2f) movement.y = 0f;

        return movement;
    }
    void moveCharacter(Vector2 direction){
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other){
        //Debug.Log(other.CompareTag("Player"));
        if (other.CompareTag("Player")) {
            if (!robotQuest.isActive) {
                talk = StartCoroutine(Talk());
                robotDialogueCanvas.SetActive(true);
                //Debug.Log(name);
                SimpleEventManager.StartListening("StartQuest", StartQuest);
            } else {
                RestartMiniGame();
            }
        }
        Debug.Log(other);
    }

    void StartQuest()
    {
        if (isQuestGiver)
        {
            if (FindObjectOfType<DialogueManager>().DialogueEnded())
            {
                startMiniGame();
                SimpleEventManager.StopListening("StartQuest", StartQuest);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            robotDialogueCanvas.SetActive(false);
            StopCoroutine(talk);
            started = false;
            FindObjectOfType<DialogueManager>().HideBox();

            AudioSource voiceSrc = audioManager.GetSound("Voice").source;
            voiceSrc.Stop();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    IEnumerator Talk()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!started)
                {
                    robotDialogueCanvas.SetActive(false);
                    TriggerDialogue();
                    started = true;
                }
                else
                {
                    FindObjectOfType<DialogueManager>().DisplayNextSentence();
                }
                yield return null;
            }
            yield return null;
        }
    }

    void startMiniGame() {
        countdownCanvas.SetActive(true);
        AudioSource background = audioManager.GetSound("Background").source;
        background.Stop();
        background = audioManager.GetSound("DialogueBackground").source;
        background.Stop();
        StartCoroutine(CountdownToStart());
    }

    void RestartMiniGame() {
        gameOverCanvas.SetActive(true);
        AudioSource robotGame = audioManager.GetSound("RobotGame").source;
        robotGame.Stop();
        rb.transform.position = new Vector3(mp1.transform.position.x, mp1.transform.position.y + 6, 0);
        moveSpeed = 0f;
        started = false;
        robotQuest.isActive = false;
        Kvothe.position = new Vector3(mp1.transform.position.x, mp1.transform.position.y + 1, 0);
        audioManager.Play("Lose");
        StartCoroutine(GameOverDisappear(false));
    }

    IEnumerator CountdownToStart() {
        while (countdownTime > 0) {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);
            AudioSource background = audioManager.GetSound("Background").source;
            background.Stop();
            audioManager.Play("Countdown");
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        audioManager.Play("RobotGame");
        robotQuest.isActive = true;
        countdownTime = 3;
        moveSpeed = 2f;

        //GameController.instance.BeginGame();
        yield return new WaitForSeconds(1f);
        countdownCanvas.SetActive(false);
    }

    IEnumerator GameOverDisappear(bool final)  //  <-  its a standalone method
    {
        yield return new WaitForSeconds(1f);
        audioManager.Play("Background");
        gameOverCanvas.SetActive(false);
        if(final) {
            this.gameObject.SetActive(false);
        }
    }

    public void missionComplete() {
        robotQuest.Complete();
        mapCollectable.SetActive(true);
        titleCanvas.text = "YOU WIN!";
        AudioSource robotGame = audioManager.GetSound("RobotGame").source;
        robotGame.Stop();
        audioManager.Play("Victory");
        subtitleCanvas.text = ""; 
        gameOverCanvas.SetActive(true);
        StartCoroutine(GameOverDisappear(true));
    }
}
