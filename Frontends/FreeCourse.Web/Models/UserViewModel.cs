namespace FreeCourse.Web.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string City { get; set; }

        public IEnumerable<string> GetUserProps()
        {
            yield return Username;
            yield return Email;
            yield return City;
        }
    }
}
