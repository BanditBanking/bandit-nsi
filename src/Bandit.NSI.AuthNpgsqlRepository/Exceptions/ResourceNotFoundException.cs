namespace Bandit.NSI.AuthNpgsqlRepository.Exceptions
{
    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException() { }

        public ResourceNotFoundException(string message) : base(message) { }
    }
}
