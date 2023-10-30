using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState_PGW<T>
{
    IEnumerator ChangeState(T state, float duration);

}
