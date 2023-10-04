using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap_PGW : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private Transform after;

    [SerializeField] private float propPushPower = 50f;
    [SerializeField] private float playerPushPower = 1000f;
    [SerializeField] private float activateSpeed = 100f;
    [SerializeField] private float deactivateSpeed = 10f;

    private Vector3 randomTorque;

    private bool isActivate = false; // 충돌 판정이 가능한지 판단
    private bool isReady = true; // 함정이 작동 될 준비가 됐는지 판단

    public IEnumerator ActivateTrap()
    {
        if (!isReady) yield break;

        isActivate = true;
        isReady = false;

        while (gameObject.transform.position != after.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, after.position, activateSpeed * Time.deltaTime);
            yield return null;

        }
        isActivate = false;
        yield return new WaitForSeconds(3f);
        StartCoroutine(DeActivateTrap());
    }

    private IEnumerator DeActivateTrap()
    {
        while (gameObject.transform.position != origin.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, origin.position, deactivateSpeed * Time.deltaTime);
            yield return null;

        }
        isReady = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!isActivate) return;
        Rigidbody rigidbody = collision.transform.GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            if (collision.CompareTag("Player"))
            {
                rigidbody.AddForce(gameObject.transform.forward * playerPushPower  * Time.deltaTime, ForceMode.VelocityChange);

            }
            else
            {
                randomTorque.x = Random.Range(0, 180);
                randomTorque.y = Random.Range(0, 180);
                randomTorque.z = Random.Range(0, 180);
                rigidbody.AddForce(gameObject.transform.forward * propPushPower, ForceMode.Impulse);
                rigidbody.AddTorque(randomTorque, ForceMode.Impulse);

            }
        }


    }


}
