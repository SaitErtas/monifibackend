using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Queries.GetNetworkUsers;

public class GetNetworkUsersQueryResponse
{
    public GetNetworkUsersQueryResponse(User user, List<User> meFirstNetworkUsers, List<User> networkUsers)
    {
        var clearMeFirstNetworkUsers = meFirstNetworkUsers.Select(x => new GetUser(x.Email, x.ReferanceCode, x.FullName, "Member",x.Id)).ToList();
        var clearNetworkUsers = networkUsers.Select(x => new GetUser(x.Email.CapitalizeFirstAndHideText(), x.ReferanceCode, x.FullName.CapitalizeFirstAndHideText(), "Sub",x.Id)).ToList();
        var clearUser = new GetUser(user.Email, user.ReferanceCode, user.FullName, "Leader",user.Id);
        NetworkUsers.Add(clearUser);
        NetworkUsers.AddRange(clearMeFirstNetworkUsers);
        NetworkUsers.AddRange(clearNetworkUsers);
        Count = NetworkUsers.Count();
    }
    public List<GetUser> NetworkUsers { get; private set; } = new();
    public int Count { get; private set; } = new();
}
public class GetUser
{
    public GetUser(string email, string referenceCode, string fullName, string status,int userId)
    {
        Email = email;
        ReferenceCode = referenceCode;
        FullName = fullName;
        Status = status;
        UserId = userId;
        Id = userId;
    }

    public string Email { get; set; }
    public string ReferenceCode { get; set; }
    public string FullName { get; set; }
    public string Status { get; set; }
    public int UserId { get; set; }
    public int Id { get; set; }
}