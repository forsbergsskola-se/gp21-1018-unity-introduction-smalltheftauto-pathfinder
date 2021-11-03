using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroyCorpse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }
}
