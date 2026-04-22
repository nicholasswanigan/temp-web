using SidApi.Data.Entities;

namespace SidApi.Models;

public class GetUsersResponseModel
{
    public bool Status {get;set;}
    public int StatusCode{get;set;}
    public string Message{get;set;}
    public List<User> Users{get;set;}
}