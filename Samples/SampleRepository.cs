using Either;

namespace Samples
{
    public class SampleRepository
    {
        public Either<FailureObjectSample[], EntitySample> SuccessSample()
        {
            // Acessar algum repositorio rest ou web service
            return new Success<FailureObjectSample[], EntitySample>(new EntitySample()
            {
                Id = 1,
                Nome = "Sample Name",
                Status = "InProgress"
            });
        }

        public Either<FailureObjectSample[], EntitySample> FailureSample()
        {
            // Tentou acessar algum repositorio rest ou web service e recebeu algum erro

            // No caso de ter sido Forbidden
            return new Failure<FailureObjectSample[], EntitySample>(new FailureObjectSample[]
            {
                new FailureObjectSample()
                {
                    PropertyName = nameof(EntitySample.Nome),
                    ValidationMessage = $"Esse cliente não tem permissão pra acessar {nameof(EntitySample.Nome)}"
                }
            });
        }
    }
}
