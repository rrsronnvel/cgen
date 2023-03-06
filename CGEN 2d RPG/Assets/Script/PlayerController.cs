using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    [SerializeField] GameObject controlButton;

    public float moveSpeed;

    private bool isMoving;

    private Vector2 input;

    float dirX, dirY;

    public Button interactButton;

    public Animator animator;

    
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;

    

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        interactButton.onClick.AddListener(Interact);
    }


    public void HandleUpdate()
    {
        controlButton.SetActive(true);
        if (!isMoving)
        {
            //comment this if want to change to button
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            
            //Debug.Log("This is input.x " + input.x);
          //  Debug.Log("This is input.y " + input.y);

    
            dirX = CrossPlatformInputManager.GetAxis("Horizontal");
            dirY = CrossPlatformInputManager.GetAxis("Vertical");

            //for button just switch it to this instead of input.x&y 
           // input.x = dirX;
           // input.y = dirY;


            //stop diagonal movement
            // if (input.x != 0) input.y = 0 ;
          
            if (input != Vector2.zero )
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
        if (Input.GetButtonDown("InteractButton"))
        {
            Interact();
        }
    }

    void Interact()
    {
        //facing
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        //facing
        // Debug.Log(facingDir);
        // Debug.Log(interactPos);
        // Debug.DrawLine(transform.position, interactPos, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);

        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
      
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer) != null)
        {
            return false;
        }
        return true;
    }
}
