namespace Service
{
    public interface IMailService
    {
        public void Send(string to, string subject, string body);
    }
}
