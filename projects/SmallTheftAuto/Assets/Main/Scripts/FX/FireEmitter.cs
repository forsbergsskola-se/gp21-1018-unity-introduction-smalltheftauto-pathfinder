using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEmitter : MonoBehaviour
{
    public GameObject particlePrefab;
    public ParticleSystem theParticle;
    private GameObject myParticle;
    public bool emitParticle = false;
    private Vector3 emitLocation;
    public Light light;
    
    
    void Start()
    {
        GetComponent<ParticleSystem>().Stop();
        GetComponentInChildren<ParticleSystem>().Stop();

        light.enabled = false;
        
        //    SetEmit(new Vector3(0,0,0));
    }


    public void SetEmit(Vector3 position)
    {
        emitLocation = position;
        emitParticle = true;
        GetComponent<ParticleSystem>().Play();
        GetComponentInChildren<ParticleSystem>().Play();
        light.enabled = true;
        GetComponent<ParticleSystem>().transform.position = position;
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
        yield return new WaitForSeconds(30f);
        GetComponent<ParticleSystem>().Stop();
        emitParticle = false;
    }
}
