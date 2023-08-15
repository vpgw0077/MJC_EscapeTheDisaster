using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLeukocyte_PGW : MonoBehaviour
{
    [SerializeField] private GameObject leukocyte;
    [SerializeField] private Transform spawnPosition;

    private bool isTriggerEnd;


    private IEnumerator SpawnLeukocyte()
    {
        isTriggerEnd = true;
        yield return new WaitForSeconds(3f);
        Instantiate(leukocyte, spawnPosition.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !isTriggerEnd)
        {
            StartCoroutine("SpawnLeukocyte");
        }
    }

}
