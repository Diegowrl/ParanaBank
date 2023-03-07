namespace ParanaBank.Domain.Entity
{
    public class Client
    {
        public Guid? Id { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
