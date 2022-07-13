using MonifiBackend.Core.Domain.Utility;
using MonifiBackend.UserModule.Domain.Users;

namespace MonifiBackend.UserModule.Application.Users.Queries.GetNetworkUsers;

public class GetNetworkUsersQueryResponse
{
    public GetNetworkUsersQueryResponse(User user, List<User> meFirstNetworkUsers, List<User> networkUsers)
    {
        var clearMeFirstNetworkUsers = meFirstNetworkUsers.Select(x => new GetUser(x.Email, x.ReferanceCode, x.FullName, "Member")).ToList();
        var clearNetworkUsers = networkUsers.Select(x => new GetUser(x.Email.CapitalizeFirstAndHideText(), x.ReferanceCode, x.FullName.CapitalizeFirstAndHideText(), "Sub")).ToList();
        var clearUser = new GetUser(user.Email, user.ReferanceCode, user.FullName, "Leader");
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
    public GetUser(string email, string referanceCode, string fullName, string status)
    {
        Email = email;
        ReferanceCode = referanceCode;
        FullName = fullName;
        Status = status;
    }

    public string Email { get; set; }
    public string ReferanceCode { get; set; }
    public string FullName { get; set; }
    public string Status { get; set; }
}