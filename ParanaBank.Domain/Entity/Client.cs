namespace ParanaBank.Domain.Entity
{
    public class Client
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdateAt { get; set; }

        public void CreateUser()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public void UpdateUser()
        {
            UpdateAt = DateTime.Now;
        }
    }
}
