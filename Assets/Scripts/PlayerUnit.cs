using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerUnit : MonoBehaviour
{
    //public Animator animator;
    public AIPath aiPath;
    public AIDestinationSetter aIDestinationSetter;
    private GameObject selectedGameObject;
    public Animator animator;

    private bool dying = false;

    // Start is called before the first frame update
    void Start()
    {
        selectedGameObject = transform.Find("Selected").gameObject;
        SetSelected(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dying)
        {
            Vector2 movement = new Vector2(aiPath.desiredVelocity.x, aiPath.desiredVelocity.y);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    public void SetSelected(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }

    public void Move(Transform movementLocation)
    {
        aIDestinationSetter.target = movementLocation;
    }
}
