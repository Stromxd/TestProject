using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float flyspeed = 0.2f;
    public float jumpForce = 5f;
    public float detectionRange = 5f;
    public LayerMask obstacleLayer;
    public LayerMask powerupLayer;
    public LayerMask coinLayer;
    public bool Flying = false;
    public float raycastDistance = 1f;
    private Rigidbody rb;
    private bool isJumping = false;
    private GameObject currentTarget;
    public GameObject wings;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Move the player forward automatically
        //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Flying")
        {
            Destroy(other.gameObject);
            StartCoroutine(FlyingPlayer());
        }
    }
    private void FixedUpdate()
    {
        // Move the player forward automatically
        if (!Flying)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else if (rb.position.y < 20)
        {
            rb.MovePosition(rb.position + new Vector3(0, 4f, 6)*Time.deltaTime);
            //transform.Translate(Vector3.up * flyspeed * Time.deltaTime);
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        //transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime);

        // Detect obstacles using a raycast
        RaycastHit obstacleHit;
        if (Physics.Raycast(transform.position, transform.forward, out obstacleHit, raycastDistance, obstacleLayer))
        {
            // Visualize the obstacle raycast in Scene view
            Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.red);

            if (!isJumping)
            {
                // Jump to avoid the obstacle
                Jump();
            }
        }
        else
        {
            // No obstacle detected, visualize the obstacle raycast in Scene view
            Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green);

            if (currentTarget == null)
            {
                // Find the nearest power-up or coin within the detection range
                Collider[] powerupsAndCoins = Physics.OverlapSphere(transform.position, detectionRange, powerupLayer | coinLayer);
                if (powerupsAndCoins.Length > 0)
                {
                    // Find the nearest power-up or coin
                    GameObject nearestObject = GetNearestObject(powerupsAndCoins);

                    // Set the current target
                    currentTarget = nearestObject;
                }
            }

            if (currentTarget != null)
            {
                // Calculate the direction to the target
                Vector3 direction = currentTarget.transform.position - transform.position;
                direction.y = 0f;
                direction.Normalize();

                // Check if the target is behind the player
                Vector3 forward = transform.forward;
                forward.y = 0f;
                bool isTargetBehind = Vector3.Dot(direction, forward) < 0f;

                // Move towards the target along the global forward axis
                if (!isTargetBehind)
                {
                    transform.position += direction * moveSpeed * Time.fixedDeltaTime;
                }
                else
                {
                    currentTarget = null; // Clear the target if it is behind the player
                }

                // Check if the target is within the detection range
                if (currentTarget != null)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);
                    if (distanceToTarget <= detectionRange)
                    {
                        // Remove the current target once reached
                        currentTarget = null;
                    }
                }
            }
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        isJumping = true;
        Invoke(nameof(ResetJump), 1f); // Reset jump after 1 second
    }

    private void ResetJump()
    {
        isJumping = false;
    }

    private GameObject GetNearestObject(Collider[] objects)
    {
        GameObject nearestObject = null;
        float nearestDistance = Mathf.Infinity;
        foreach (Collider obj in objects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestObject = obj.gameObject;
            }
        }
        return nearestObject;
    }
    IEnumerator FlyingPlayer()
    {
        this.gameObject.GetComponent<Animator>().enabled = false;
        Rigidbody pRB = rb.GetComponent<Rigidbody>();
        pRB.isKinematic = true;
        pRB.useGravity = false;

        Flying = true;
        wings.SetActive(true);
        yield return new WaitForSeconds(10f);
        this.gameObject.GetComponent<Animator>().enabled = true;
        wings.SetActive(false);
        pRB.isKinematic = false;
        pRB.useGravity = true;
        Flying = false;
    }
}
