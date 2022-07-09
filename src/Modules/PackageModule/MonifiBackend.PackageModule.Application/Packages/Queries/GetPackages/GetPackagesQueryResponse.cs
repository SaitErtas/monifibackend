using MonifiBackend.PackageModule.Domain.Packages;

namespace MonifiBackend.PackageModule.Application.Packages.Queries.GetPackages;

public class GetPackagesQueryResponse
{
    public GetPackagesQueryResponse(List<Package> packages)
    {
        Packages = packages.Select(x => new GetPackageQueryResponse(x.Id, x.Name, x.Duration, x.Commission)).ToList();
    }

    public List<GetPackageQueryResponse> Packages { get; private set; }
}
public class GetPackageQueryResponse
{
    public GetPackageQueryResponse(int id, string name, int duration, decimal commission)
    {
        Id = id;
        Name = name;
        Duration = duration;
        Commission = commission;
    }
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Duration { get; private set; }
    public decimal Commission { get; private set; }
}
