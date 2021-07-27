using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavigation : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private CharacterSelectionController selectionController;
    public Vector3 targetPosition;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        selectionController = FindObjectOfType<CharacterSelectionController>();
        StartCoroutine(SetStroll());
    }

    public void Update()
    {
        if(selectionController.character == gameObject)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(transform.position);
        }
    }

    IEnumerator SetStroll()
    {
        targetPosition = transform.position;
        while (true)
        {
            if ((selectionController.character==null||selectionController.character != gameObject)
                && (Mathf.Abs(targetPosition.x - transform.position.x) < 0.01f
                && Mathf.Abs(targetPosition.z - transform.position.z) < 0.01f))
            {
                navMeshAgent.isStopped = false;
                targetPosition = new Vector3(transform.position.x + Random.Range(-10f, 10f),
                    0, transform.position.z + Random.Range(-10f, 10f));
                navMeshAgent.SetDestination(targetPosition);
            }
            else
            {
                navMeshAgent.isStopped = true;
            }

            yield return new WaitForSeconds(3);
        }
    }
}
