using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmmiter : MonoBehaviour
{
    public GameObject particlePrefab;
    public ParticleSystem theParticle;
    private GameObject myParticle;
    public bool emitParticle = false;
    private Vector3 emitLocation;
    
    void Start()
    {
        GetComponent<ParticleSystem>().Stop();
        SetEmit(new Vector3(0,0,0));
    }


    public void SetEmit(Vector3 position)
    {
        emitLocation = position;
        emitParticle = true;
        GetComponent<ParticleSystem>().transform.position = GameObject.FindWithTag("ThePlayer").transform.position;
    }
    
    void Update()
    {
        if (emitParticle)
        {
            GetComponent<ParticleSystem>().Play();
            GetComponent<ParticleSystem>().Emit(1);
            StartCoroutine(StopEmit());
        }
    }

    private IEnumerator StopEmit()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<ParticleSystem>().Stop();
        emitParticle = false;
    }
}
