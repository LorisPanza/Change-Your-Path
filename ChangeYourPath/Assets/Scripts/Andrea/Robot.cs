using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Robot : MonoBehaviour
{
    private bool flag=true;
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
    public Dialogue dialogue;
    private Coroutine talk, countdown;
    private bool started = false, startedGame = false;
    public bool isQuestGiver;
    public RobotQuest robotQuest;
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
            //moveCharacter(coordinates);

            float x = movement.x;
            float y = movement.y;
            //Reset Move Delta
            moveDelta = Vector3.zero;
            moveDelta = new Vector3(x, y, 0);

            hit = Physics2D.BoxCast(movePoint.transform.position, boxCollider.size/3f, 0,
            new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime * moveSpeed), LayerMask.GetMask("Obstacle"));

            if (hit.collider == null)
                transform.Translate(0, moveDelta.y * Time.deltaTime * moveSpeed, 0);

            hit = Physics2D.BoxCast(movePoint.transform.position, boxCollider.size/3f, 0,
                new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime * moveSpeed), LayerMask.GetMask("Obstacle"));

            if (hit.collider == null)
                transform.Translate(moveDelta.x * Time.deltaTime * moveSpeed, 0, 0);

            animator.SetFloat("Horizontal", x);
            animator.SetFloat("Vertical", y);
            animator.SetFloat("Speed", moveDelta.sqrMagnitude);
        } else {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", 0);
        }
         ///////////////////////////////////////

        

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
            if (!robotQuest.isActive && !startedGame && flag) {
                talk = StartCoroutine(Talk());
                flag = false;
                Debug.Log(name);
                SimpleEventManager.StartListening("StartQuest", StartQuest);
            }
        }
        //Debug.Log(other);
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
            
            if (started)
            {
                AudioSource background=audioManager.GetSound("Background").source;
                background.Play();
                AudioSource dialoguebackground = audioManager.GetSound("DialogueBackground").source;
                dialoguebackground.Stop();
                //audioManager.Play("Background");
                AudioSource voiceSrc = audioManager.GetSound("RobotVoice").source;
                voiceSrc.Stop();
            }
            StopCoroutine(talk);
            StopCoroutine(Talk());
            started = false;
            FindObjectOfType<DialogueManager>().HideBox();
            flag = true;


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
            //Debug.Log("Sto parlando");
            if (Input.GetKeyDown(KeyCode.Space) && !startedGame && GameObject.Find("GameManager").GetComponent<GameManager>().getMode() == 1)
            {
                if (!started)
                {
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
        AudioSource background = audioManager.GetSound("Background").source;
        background.Stop();
        background = audioManager.GetSound("DialogueBackground").source;
        background.Stop();
        startedGame = true;
        StopCoroutine(talk);
        StopCoroutine(Talk());
        started = false;
        countdownCanvas.SetActive(true);
        
        if (countdown != null)
        {
            StopCoroutine(countdown);
            StopCoroutine(CountdownToStart());
        }
        countdown = StartCoroutine(CountdownToStart());
    }

    public void RestartMiniGame() {
        startedGame = false;
        StopCoroutine(countdown);
        StopCoroutine(CountdownToStart());
        countdownDisplay.text = "3";
        countdownCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        AudioSource robotGame = audioManager.GetSound("RobotGame").source;
        robotGame.Stop();
        rb.transform.position = new Vector3(mp1.transform.position.x, mp1.transform.position.y + 6, 0);
        moveSpeed = 0f;
        started = false;
        robotQuest.isActive = false;
        Kvothe.position = new Vector3(mp1.transform.position.x, mp1.transform.position.y +1, 0);
        audioManager.Play("Lose");
        StartCoroutine(GameOverDisappear(false));
    }

    IEnumerator CountdownToStart() {
        int countdownTime = 3;
        while (countdownTime > 0) {
            Debug.Log(countdownTime);
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);
            AudioSource background = audioManager.GetSound("Background").source;
            background.Stop();
            audioManager.Play("Countdown");
            countdownTime--;
        }
        //Debug.Log(countdownTime);

        countdownDisplay.text = "GO!";
        audioManager.Play("RobotGame");
        robotQuest.isActive = true;
        countdownTime = 3;
        moveSpeed = 2f;

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
        SimpleEventManager.TriggerEvent("RobotQuestCompleted");
        AudioSource robotGame = audioManager.GetSound("RobotGame").source;
        robotGame.Stop();
        audioManager.Play("Victory");
        subtitleCanvas.text = ""; 
        gameOverCanvas.SetActive(true);
        StartCoroutine(GameOverDisappear(true));
    }
}
