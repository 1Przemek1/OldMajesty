using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AUnit : IObject 
{
    public float moveSpeed = 1f;
    public List<AudioClip> moveSounds;

    public Animator anim;

    private int currentMoveSoundPos = 0;

    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    public void triggerWalkAnimation()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Idle"))
            anim.SetTrigger("moving");

        Vector3 newPos = new Vector3(transform.position.x, 0.1f, transform.position.z);
        transform.position = newPos;
    }

    public void triggerIdleAnimation()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Walk"))
            anim.SetTrigger("moving");

        Vector3 newPos = new Vector3(transform.position.x, 0.1f, transform.position.z);
        transform.position = newPos;
    }

    public void moveToDestination(Vector3 destination)
    {
        StopAllCoroutines();
        StartCoroutine(moveCoroutine(destination));
    }

    IEnumerator moveCoroutine(Vector3 destination)
    {
        while (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

            yield return null;
        }

        if (Vector3.Distance(transform.position, destination) <= 0.1f)
            triggerIdleAnimation();
    }

    public void rotateToDirection(Vector3 vec)
    {
        Vector3 lookPos = vec - transform.position;
        float angle = Mathf.Atan2(lookPos.x, lookPos.z) * Mathf.Rad2Deg;

        //  Debug.Log(angle);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    public void playMoveSound()
    {
        playSound(moveSounds[currentMoveSoundPos]);
        currentMoveSoundPos++;
        if (currentMoveSoundPos >= moveSounds.Count)
            currentMoveSoundPos = 0;
    }
}
