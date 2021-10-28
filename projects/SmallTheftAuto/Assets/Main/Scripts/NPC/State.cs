using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class State
{
   public enum STATE
   {
      Idle, Patrol, Pursue, Attack, Rest
   }

   public enum EVENT
   {
      Enter, Update, Exit
   }

   public STATE Name;
   protected EVENT Stage;
   protected GameObject Npc;
   protected Animator Anim;
   protected Transform Player;
   protected State NextState;
   protected NavMeshAgent Agent;
   protected WayPointManager wpManager;

   private float shootDistance = 7.0f;
   
   public State(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
   {
      Npc = npc;
      Stage = EVENT.Enter;
      Agent = agent;
      Anim = anim;
      Player = player;
      wpManager = GameObject.FindWithTag("wpManager").GetComponent<WayPointManager>();

   }

   public virtual void Enter()
   {
      Stage = EVENT.Update;
   }
   public virtual void Update()
   {
      Stage = EVENT.Update;
   }
   public virtual void Exit()
   {
      Stage = EVENT.Exit;
   }

   public State Process()
   {
      switch (Stage)
      {
         case EVENT.Enter:
            Enter();
            break;
         case EVENT.Update:
            Update();
            break;
         case EVENT.Exit:
            Exit();
            return NextState;
      }

      return this;
   }
   
}

public class Idle : State
{
   public Idle(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
      : base(npc, agent, anim, player)
   {
      Name = STATE.Idle;
   }

   public override void Enter()
   {
      base.Enter();
   }
   public override void Update()
   {
      if (Random.Range(0, 100) < 10)
      {
         NextState = new Patrol(Npc, Agent, Anim, Player);
         Stage = EVENT.Exit;
      }
   }
   public override void Exit()
   {
      base.Exit();
   }
}


public class Patrol : State
{
   private int currentIndex = -1;
   public Patrol(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
      : base(npc, agent, anim, player)
   {
      Name = STATE.Patrol;
      Agent.speed = 2;
      Agent.isStopped = false;
   }

   public override void Enter()
   {
      currentIndex = 0;
      Agent.SetDestination(wpManager.GetLocationOfPoint(currentIndex));
      base.Enter();
   }
   public override void Update()
   {
      if (Agent.remainingDistance < 0.2f)
      {
         if (currentIndex >= wpManager.CurrentNumberPoints - 1)
         {
            currentIndex = -1;
         }
         
         else
         {
            currentIndex++;
            Agent.SetDestination(wpManager.GetLocationOfPoint(currentIndex));
         }
      }
   }
   public override void Exit()
   {
      base.Exit();
   }
}


public class Pursue : State
{
   public Pursue(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
      : base(npc, agent, anim, player)
   {
      Name = STATE.Patrol;
   }

   public override void Enter()
   {
      base.Enter();
   }
   public override void Update()
   {
     
      base.Update();
   }
   public override void Exit()
   {
      base.Exit();
   }
}

public class Attack : State
{
   public Attack(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
      : base(npc, agent, anim, player)
   {
      Name = STATE.Patrol;
   }

   public override void Enter()
   {
      base.Enter();
   }
   public override void Update()
   {
     
      base.Update();
   }
   public override void Exit()
   {
      base.Exit();
   }
}