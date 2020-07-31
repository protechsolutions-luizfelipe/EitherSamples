using System;

namespace Either
{
    public class Success<TFailure, TSuccess> : Either<TFailure, TSuccess>
    {
        public TSuccess Value { get; }

        public Success(TSuccess success)
        {
            Value = success;
        }
        public override Either<TFailure2, TSuccess2> BiMap<TFailure2, TSuccess2>(
            Func<TFailure, TFailure2> ifLeft, Func<TSuccess, TSuccess2> ifRight)
        {
            return new Success<TFailure2, TSuccess2>(ifRight(Value));
        }

        public override Either<TFailure, TSuccess2> Bind<TSuccess2>(
            Func<TSuccess, Either<TFailure, TSuccess2>> f)
        {
            return f(Value);
        }

        public override TSuccess2 FromEither<TSuccess2>(
            Func<TFailure, TSuccess2> ifLeft, Func<TSuccess, TSuccess2> ifRight)
        {
            return ifRight(Value);
        }
    }
}
