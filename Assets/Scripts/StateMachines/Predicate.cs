using System;


public class Predicate : IPredicate
{
    protected readonly Func<bool> funcEvaluation;

    public Predicate(Func<bool> func)
    {
        funcEvaluation = func;
    }

    public bool Evaluate() => funcEvaluation.Invoke();
}
