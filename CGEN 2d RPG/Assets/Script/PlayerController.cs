using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour, IDataPersistence
{

    [SerializeField] GameObject controlButton;

    public float moveSpeed;

    public bool isMoving;

    private Vector2 input;

    float dirX, dirY;

    public Button interactButton;

    public Animator animator;


    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    public LayerMask pickUpNoPushLayer;

    private bool isDestroyed = false;

    public bool isGameOver = false;

    private PickUp pickUp;

    // Add this line
    public bool isKnockback;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        interactButton.onClick.AddListener(Interact);

        pickUp = gameObject.GetComponent<PickUp>();
        pickUp.Direction = new Vector2(0, -1);
    }

    private void OnDestroy()
    {
        isDestroyed = true;
    }


    public void LoadData(GameData data, bool isRestarting)
    {
        this.moveSpeed = data.playerSpeed;
        if (!isRestarting)
        {
            this.transform.position = data.playerPosition;
        }
        else
        {
            this.transform.position = new Vector3(-12.0f, 1.0f, 0f); // Set this to your initial player position

        }
    }





    public void SaveData(ref GameData data)
    {
        if (!isDestroyed)
        {
            data.playerPosition = this.transform.position;
        }
    }



    public void Update()
    {
        if (isGameOver || isKnockback) // Add isKnockback here
        {
            return;
        }

        // controlButton.SetActive(true);
        if (!isMoving)
        {
            //comment this if want to change to button
            input = Vector3.zero;
            // input.x = Input.GetAxisRaw("Horizontal");
            // input.y = Input.GetAxisRaw("Vertical");


            dirX = CrossPlatformInputManager.GetAxis("Horizontal");
            dirY = CrossPlatformInputManager.GetAxis("Vertical");

            //for button just switch it to this instead of input.x&y 
            input.x = dirX;
            input.y = dirY;

            if (input.sqrMagnitude > .1f)
            {
                pickUp.Direction = input.normalized;
            }


            //Debug.Log("This is input.x " + input.x);
            //  Debug.Log("This is input.y " + input.y);




            //stop diagonal movement
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {

                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;



                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));


            }
        }

        animator.SetBool("isMoving", isMoving);
        // || Input.GetMouseButtonDown(0)

        //  Input.GetKeyDown(KeyCode.Z)
        if (Input.GetButtonDown("InteractButton") || Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Hello World!");
            Interact();
        }



    }

    public void Nudge(Vector3 amount)
    {
        this.transform.position += amount;
    }

    void Interact()
    {
        //facing
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        //facing
        // Debug.Log(facingDir);
        // Debug.Log(interactPos);
        Debug.DrawLine(transform.position, interactPos, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(interactPos, 0.7f, interactableLayer);

        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
        GetComponent<PickUp>().Interact();
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            if (isKnockback) // Add this check
            {
                yield break;
            }

            // Check if there's a NudgePlayer object in the way and move it as well
            var hitCollider = Physics2D.OverlapCircle(targetPos, 0.4f, solidObjectsLayer | interactableLayer);
            var interactable = hitCollider?.GetComponent<Interactable>();

            if (interactable is NudgePlayer)
            {
                hitCollider.transform.position = Vector3.MoveTowards(hitCollider.transform.position, hitCollider.transform.position + (targetPos - transform.position), moveSpeed * Time.deltaTime);
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }



    private bool IsWalkable(Vector3 targetPos)
    {
        var hitCollider = Physics2D.OverlapCircle(targetPos, 0.4f, solidObjectsLayer | interactableLayer | pickUpNoPushLayer);

        if (hitCollider != null)
        {
            var interactable = hitCollider.GetComponent<Interactable>();

            if (interactable is NudgePlayer) // Allow moving into NudgePlayer objects
                return true;

            return false;
        }

        return true;
    }

}
