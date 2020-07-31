using System;

namespace Either
{
    public class Failure<TFailure, TSuccess> : Either<TFailure, TSuccess>
    {
        public TFailure Value { get; }

        public Failure(TFailure failure)
        {
            Value = failure;
        }
        public override Either<TFailure2, TSuccess2> BiMap<TFailure2, TSuccess2>(
            Func<TFailure, TFailure2> ifLeft, Func<TSuccess, TSuccess2> ifRight)
        {
            return new Failure<TFailure2, TSuccess2>(ifLeft(Value));
        }

        public override Either<TFailure, TSuccess2> Bind<TSuccess2>(
            Func<TSuccess, Either<TFailure, TSuccess2>> f)
        {
            return new Failure<TFailure, TSuccess2>(Value);
        }

        public override TSuccess2 FromEither<TSuccess2>(
            Func<TFailure, TSuccess2> ifLeft, Func<TSuccess, TSuccess2> ifRight)
        {
            return ifLeft(Value);
        }
    }
}
