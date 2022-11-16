using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    [SerializeField]
    private Hand currentHand;

    private bool _isAttack = false;
    private bool _isSwing = false;

    private RaycastHit hitInfo;

    // Update is called once per frame
    private void Update()
    {
        TryAttack();
    }

    private void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!_isAttack)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        _isAttack = true;
        currentHand.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentHand._attackDelayA);
        _isSwing = true;

        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentHand._attackDelayB);
        _isSwing = false;

        yield return new WaitForSeconds(currentHand._attackDelay - currentHand._attackDelayA - currentHand._attackDelayB);
        _isAttack = false;
    }

    IEnumerator HitCoroutine()
    {
        while (_isSwing)
        {
            if (CheckObject())
            {
                _isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }

            yield return null;
        }
    }

    private bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentHand._range))
        {
            return true;
        }
        return false;
    }
}
