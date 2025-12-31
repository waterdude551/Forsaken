public abstract class State 
{  protected StateMachine context;

   public State(StateMachine currentContext)
   {
      context = currentContext;
   }
   public abstract void EnterState();
   public abstract void UpdateState();
   public abstract void ExitState();
   public abstract void CheckSwitchStates();

   void UpdateStates(){}
   public void SwitchState(State newState)
   {
      ExitState();
      newState.EnterState();
      context.CurrentState = newState;
   }

}
