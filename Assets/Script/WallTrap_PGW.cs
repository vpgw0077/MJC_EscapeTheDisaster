using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap_PGW : MonoBehaviour
{
    [Header("AudioComponent")]
    [SerializeField] private AudioSource theAudioSource = null;
    [SerializeField] private AudioClip theAudioClip = null;

    [Space]
    [SerializeField] private Transform origin = null;
    [SerializeField] private Transform after = null;

    [SerializeField] private float propPushPower = 50f;
    [SerializeField] private float playerPushPower = 1000f;
    [SerializeField] private float activateSpeed = 100f;
    [SerializeField] private float deactivateSpeed = 10f;
    [SerializeField] private float knockBackDuration = 0f;

    private Rigidbody playerRigidbody = null;
    private Rigidbody propRigidbody = null;

    private Vector3 randomTorque = Vector3.zero;
    private bool detectPlayer = false;
    private bool detectProps = false;
    private bool isActivate = false; // 충돌 판정이 가능한지 판단
    private bool isReady = true; // 함정이 작동 될 준비가 됐는지 판단

    private void FixedUpdate()
    {
        if (detectPlayer)
        {
            playerRigidbody.AddForce(gameObject.transform.forward * playerPushPower, ForceMode.Impulse);

        }
        else if (detectProps)
        {
            propRigidbody.AddForce(gameObject.transform.forward * propPushPower, ForceMode.Impulse);
            propRigidbody.AddTorque(randomTorque, ForceMode.Impulse);

        }
    }
    public IEnumerator ActivateTrap()
    {
        if (!isReady) yield break;

        isActivate = true;
        isReady = false;
        theAudioSource.PlayOneShot(theAudioClip);
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

        if (collision.CompareTag("Player"))
        {
            playerRigidbody = collision.transform.GetComponent<Rigidbody>();
            IState_PGW<CharacterMove_PGW.playerState> state = collision.GetComponent<IState_PGW<CharacterMove_PGW.playerState>>();
            StartCoroutine(state.ChangeState(CharacterMove_PGW.playerState.OutOfControl, 0));
            detectPlayer = true;
        }
        else
        {
            propRigidbody = collision.transform.GetComponent<Rigidbody>();
            randomTorque.x = Random.Range(0, 180);
            randomTorque.y = Random.Range(0, 180);
            randomTorque.z = Random.Range(0, 180);
            detectProps = true;


        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IState_PGW<CharacterMove_PGW.playerState> state = other.GetComponent<IState_PGW<CharacterMove_PGW.playerState>>();
            StartCoroutine(state.ChangeState(CharacterMove_PGW.playerState.Controlable, knockBackDuration));
            detectPlayer = false;
        }
        else
        {
            detectProps = false;
        }
    }

}
