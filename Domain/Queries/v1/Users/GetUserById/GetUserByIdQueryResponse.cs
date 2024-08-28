namespace Domain.Queries.v1.Users.GetUserById
{
    public class GetUserByIdQueryResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
