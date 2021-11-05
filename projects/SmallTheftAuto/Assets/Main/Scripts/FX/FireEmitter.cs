using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireEmitter : MonoBehaviour
{
    public GameObject particlePrefab;
    public ParticleSystem theParticle;
    private GameObject myParticle;
    public bool emitParticle = false;
    private Vector3 emitLocation;
    public Light light;
    int partsPerFrame = 20;
    private GameObject DamageCausingVolume;
    public AnimationCurve fireCurve;
    public GameObject painVolume;
    
    void Start()
    {
        ParticleSystem.MinMaxCurve curve = new ParticleSystem.MinMaxCurve();

        light.enabled = false;  
        CarDamageScript.OnParticleEmitt += SetBurst;
        theParticle = GetComponent<ParticleSystem>();
    //    GetComponent<ParticleSystem>().Stop();
    //    GetComponentInChildren<ParticleSystem>().Stop();

    //    light.enabled = false;
        
        //    SetEmit(new Vector3(0,0,0));
    }
    
    
    

    public void SetEmit(Vector3 position, int numberParticles)
    {
        theParticle.Emit(numberParticles);
        emitLocation = position;
        emitParticle = true;
    //    theParticle.Play(true);
    //    light.enabled = true;
        theParticle.transform.position = position;
    }
    
  
    
    void Update()
    {
        if (emitParticle)
        {
            
        //    theParticle.Emit(100);
            
            
        //    ParticleSystem.Particle[] particles_original = new ParticleSystem.Particle[theParticle.particleCount];
        //    theParticle.GetParticles (particles_original);
            
        //    ParticleSystem.Particle[] particles_new = new ParticleSystem.Particle[partsPerFrame];
        //    Vector3 spawnPos;
            

           StartCoroutine(StopEmit());
        }
    }

    //new ParticleSystem.Burst(2.0f, 100),
    //new ParticleSystem.Burst(4.0f, 100)

    private void CreatePainVolume(Vector3 position)
    {
        DamageCausingVolume = Instantiate(painVolume);
        DamageCausingVolume.transform.position = position;
    }
    
    private void SetBurst(Vector3 position,  bool isRandom)
    {
      var em = theParticle.emission;
      em.enabled = true;

      if (isRandom)
      {
          position += new Vector3(Randomize._random.Next(-2, 2), 0, Randomize._random.Next(-1, 1));
      }
        CreatePainVolume(position);
    //  em.rateOverTime = 20.0f;
      ParticleSystem.MinMaxCurve aCurve = new ParticleSystem.MinMaxCurve();
      aCurve.curve = fireCurve;
      em.rateOverTime = new ParticleSystem.MinMaxCurve(50.0f, fireCurve);
      em.SetBursts(
          new ParticleSystem.Burst[]{
              new ParticleSystem.Burst(5, aCurve , 1, 0.001f),
          });
      theParticle.transform.position = position;
      theParticle.Play();

      StartCoroutine(StopEmit());
    }
    
    private IEnumerator StopEmit()
    {
        yield return new WaitForSeconds(30f);
        GetComponent<ParticleSystem>().Stop();
        GetComponentInChildren<ParticleSystem>().Stop();
        Destroy(DamageCausingVolume);
        emitParticle = false;
        light.enabled = false;
    }
}
