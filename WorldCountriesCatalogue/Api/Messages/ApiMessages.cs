namespace WorldCountriesCatalogue.Api.Messages
{
    public class ApiMessages
    {
        // StringMessage - record (запись) строкового сообщения
        public record StringMessage(string Message);

        public record ErrorMessage(string Type, string Message);
    }
}
