using System;


public abstract class Predicate : IPredicate
{
    protected readonly Func<bool> funcEvaluation;

    public Predicate(Func<bool> func)
    {
        funcEvaluation = func;
    }

    public virtual bool Evaluate() => funcEvaluation.Invoke();
}
