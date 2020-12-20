using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<CharacterMove_PGW>().HP -= 20f * Time.deltaTime; // 임시

        }
        else if(other.transform.tag == "Virus")
        {
            Destroy(other.gameObject);
        }
    }
}
