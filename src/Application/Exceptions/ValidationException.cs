namespace ControleFacil.src.Application.Exceptions
{
    public class ValidationException(IEnumerable<string> errors, string? message = "Ocorreram erros de validação.") : Exception(message)
    {
        public IReadOnlyList<string> Errors { get; } = new List<string>(errors).AsReadOnly();
    }
}