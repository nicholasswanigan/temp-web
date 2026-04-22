namespace SidApi.Models
{
    public class UserResponse
    {
        public int Id {get;set;}
        public string Type {get;set;}=string.Empty;
        public string name {get;set;}=string.Empty;
        public string? FirstName {get;set;}
        public string? LastName{get;set;}
        public string Email{get;set;}=string.Empty;
        public string? Phone {get;set;}

    }
}