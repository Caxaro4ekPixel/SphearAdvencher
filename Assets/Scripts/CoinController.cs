using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public void OnDestroyCoin()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
