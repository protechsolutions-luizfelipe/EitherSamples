using Either;
using System;
using System.Linq;

namespace Samples
{
    class Program
    {
        static void SuccessCase(SampleRepository sampleRepository)
        {
            var novaEntitySample = sampleRepository.SuccessSample();

            // Encadeando processos sobre a novaEntitySample
            // caso ela fosse um caso de Failure, o TFailure seria mantido
            // e as instruções passadas para o Bind não seriam executadas.
            var maisUmaEntity = novaEntitySample.Bind(entitySample =>
                new Success<FailureObjectSample[], EntitySample>(new EntitySample()
                {
                    Id = entitySample.Id + 1,
                    Nome = $"{entitySample.Nome} SubEntity",
                    Status = $"{entitySample.Status} SubEntity"
                }));

            // Processando o campo Status para marcá-lo como Done
            // apenas no caso de maisUmaEntity seja um caso de sucesso
            maisUmaEntity.BiMap(entitySample => entitySample,
                entitySample => entitySample.Status = entitySample.Status
                    .Replace("InProgress", "Done"));

            // "Renderizando" o resultado final, aqui damos instruções sobre
            // o que deve ser feito nos casos de sucesso e falha
            // Nesse caso, estou transformando em uma string tanto os objetos
            // que representam uma falha quanto os que representam sucesso
            // usando instruções específicas para cada caso.
            var strResult = maisUmaEntity.FromEither(failures => failures
                .Select(d => $"{d.PropertyName}: {d.ValidationMessage}")
                .Aggregate((l, n) => $"{l}\n{n}"),
                entitySuccess => $"{entitySuccess.Id} | {entitySuccess.Nome} | " +
                $"{entitySuccess.Status}");

            Console.WriteLine($"Success Case:\n{strResult}");
        }

        static void FailureCase(SampleRepository sampleRepository)
        {
            var novaEntitySample = sampleRepository.FailureSample();

            // Neste caso como novaEntitySample é um caso de Failure
            // a criação do caso Success nem vai acontecer
            var maisUmaEntity = novaEntitySample.Bind(entitySample =>
                new Success<FailureObjectSample[], EntitySample>(new EntitySample()
                {
                    Id = entitySample.Id + 1,
                    Nome = $"{entitySample.Nome} SubEntity",
                    Status = $"{entitySample.Status} SubEntity"
                }));

            // Processando o campo ValidationMessage para deixá-lo um UpperCase
            // apenas no caso de maisUmaEntity seja um caso de falha
            maisUmaEntity.BiMap(failureObjectSamples =>
            {
                foreach (var failure in failureObjectSamples)
                    failure.ValidationMessage = failure.ValidationMessage.ToUpper();
                return failureObjectSamples;
            }, entitySample => entitySample);

            // "Renderizando" o resultado final, aqui damos instruções sobre
            // o que deve ser feito nos casos de sucesso e falha
            // Nesse caso, estou transformando em uma string tanto os objetos
            // que representam uma falha quanto os que representam sucesso
            // usando instruções específicas para cada caso.
            var strResult = maisUmaEntity.FromEither(failures => failures
                .Select(d => $"{d.PropertyName}: {d.ValidationMessage}")
                .Aggregate((l, n) => $"{l}\n{n}"),
                entitySuccess => $"{entitySuccess.Id} | {entitySuccess.Nome} | " +
                $"{entitySuccess.Status}");

            Console.WriteLine($"Failure Case:\n{strResult}");
        }

        static void Main(string[] args)
        {
            // Exemplo de uso em algo como um controller
            var sampleRepository = new SampleRepository();

            // Exemplos

            SuccessCase(sampleRepository);
            FailureCase(sampleRepository);

            Console.ReadLine();
        }
    }
}