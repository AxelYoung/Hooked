using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Speed player moves
    public float playerSpeed;
    // Speed hook rotates around player
    public float rotSpeed;
    // Speed hook moves
    public float hookSpeed;
    // Speed hook returns
    public float returnSpeed;

    // If hook thrown
    private bool thrownHook;
    // If hook returned
    private bool returning;

    // Hook transform
    private Transform hookTrans;
    // Hook script
    private Hook hookScript;

    // Scorekeep
    private Scorekeeper sk;

    // Start is called before the first frame update
    void Start()
    {
        // Hook transform set to first child
        hookTrans = transform.GetChild(0);
        // Hook script found on hook transform
        hookScript = hookTrans.GetComponent<Hook>();
        // Scorekeeper found on camera
        sk = Camera.main.GetComponent<Scorekeeper>();
    }

    // Update is called once per frame
    void Update()
    {
        // Tap screen to extend hook
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Do input
            InputControl();
        }
        // If hook has returned to or before original position and is returning
        if (hookTrans.localPosition == new Vector3(0, 0.8f) && returning)
        {
            // Hook no longer hitting wall
            hookScript.hitWall = false;
            // No longer returning
            returning = false;
            // No longer thrown
            thrownHook = false;
            // Set position to original position in case overshot
            hookTrans.localPosition = new Vector2(0, 0.8f);
        }
        // If hook has made contact 
        if (hookScript.curPoint != null)
        {
            // Deparent hook as child
            hookTrans.parent = null;
            // If location is at hooked point
            if (transform.position == hookTrans.position)
            {
                // Set transform position to hookpoint position
                transform.position = hookScript.curPoint.position;
                // No hooked point
                hookScript.curPoint = null;
                // Hook not thrown
                thrownHook = false;
                // Reparent hook as child
                hookTrans.parent = transform;
                // Reset hook position
                hookTrans.localPosition = new Vector2(0, 0.8f);
                // Add to score
                sk.AddScore(1);
            }
        }
        // If hook hit wall
        if (hookScript.hitWall)
        {
            // Set returning false
            returning = true;
        }
        // Rope transform controlled
        Rope();
    }

    // All inputs go through here (Editor and App)
    public void InputControl()
    {
        // If hook current point is null
        if (hookScript.curPoint == null)
        {
            // If hook not thrown
            if (!thrownHook)
            {
                // Throw hook
                thrownHook = true;
            }
            // If hook thrown
            else
            {
                // Initiate return
                returning = true;
            }
        }
    }
    // Fixed update (non-performance based)
    void FixedUpdate()
    {
        // If hook not thrown
        if (!thrownHook && hookScript.curPoint == null)
        {
            // Rotate player
            gameObject.transform.rotation = Quaternion.Euler(0, 0, gameObject.transform.eulerAngles.z - rotSpeed);
            // Hook collider non functional
            hookTrans.GetComponent<Collider2D>().enabled = false;
        }
        // Hook thrown 
        if(thrownHook && hookScript.curPoint == null)
        {
            // Move forward by hookspeed
            hookTrans.localPosition += Vector3.up * hookSpeed;
            // Enable hook collider
            hookTrans.GetComponent<Collider2D>().enabled = true;
        }
        // If returning return
        if (returning)
        {
            // Return hook
            ReturnHook();
        }
        // If hooked point exists
        if (hookScript.curPoint != null)
        {
            // Move player
            MovePlayer();
        }
    }

    // Return hook
    private void ReturnHook()
    {
        // Move towards player by hookspeed and return multiplier
        hookTrans.localPosition = Vector2.MoveTowards(hookTrans.localPosition, new Vector2(0, 0.8f), Time.deltaTime * returnSpeed);
    }

    // Move player
    private void MovePlayer()
    {
        // Move towards hooked point by player speed
        transform.position = Vector2.MoveTowards(transform.position, hookTrans.position, Time.deltaTime * playerSpeed);
    }

    // Rope creation
    private void Rope()
    {
        // Set start position of line renderer to player
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        // Set end position of line renderer to hook
        GetComponent<LineRenderer>().SetPosition(1, hookTrans.position);
        // Set the width of line based on distance from hook
        float width = 0.16f / Vector2.Distance(transform.position, hookTrans.position);
        // If width greater than minimum viable amount
        if (width > 0.02f)
        {
            // Set end width to width
            GetComponent<LineRenderer>().endWidth = width;
        }
    }

    // Death protocol
    public void Die()
    {
        // Reset scene TEMPORARY
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
