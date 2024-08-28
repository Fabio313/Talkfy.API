namespace Domain.Queries.v1.Users.GetUsersByFilter
{
    public class GetUserByFilterQueryResponse
    {
        public IEnumerable<UsersResponse> Users { get; set; }
    }

    public class UsersResponse
    { 
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
