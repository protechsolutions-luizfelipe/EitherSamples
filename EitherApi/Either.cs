using System;

namespace Either
{
    public abstract class Either<TFailure, TSuccess>
    {
        // Resultado Final
        public abstract TResultadoFinal FromEither<TResultadoFinal>(
            Func<TFailure, TResultadoFinal> ifLeft, Func<TSuccess, TResultadoFinal> ifRight);
        // Manipular Resultado sem afetar o estado de sucesso ou falha
        public abstract Either<C, D> BiMap<C, D>(Func<TFailure, C> ifLeft, Func<TSuccess, D> ifRight);
        // Encadear Resultados
        public abstract Either<TFailure, C> Bind<C>(Func<TSuccess, Either<TFailure, C>> f);
    }
}
