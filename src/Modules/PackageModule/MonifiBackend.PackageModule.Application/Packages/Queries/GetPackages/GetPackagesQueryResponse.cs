using MonifiBackend.PackageDetailModule.Domain.PackageDetails;
using MonifiBackend.PackageModule.Domain.Packages;

namespace MonifiBackend.PackageModule.Application.Packages.Queries.GetPackages;

public class GetPackagesQueryResponse
{
    public GetPackagesQueryResponse(List<Package> packages)
    {
        Packages = packages.Select(x => new GetPackageQueryResponse(x.Id, x.Name, x.Details)).ToList();
    }

    public List<GetPackageQueryResponse> Packages { get; private set; }
}
public class GetPackageQueryResponse
{
    public GetPackageQueryResponse(int id, string name, IReadOnlyCollection<PackageDetail> details)
    {
        Id = id;
        Name = name;
        Details = details.Select(s => new GetPackageDetailQueryResponse(s.Id, s.Name, s.Duration, s.Commission)).ToList();
    }
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Duration { get; private set; }
    public int Commission { get; private set; }
    public List<GetPackageDetailQueryResponse> Details { get; private set; }
}
public class GetPackageDetailQueryResponse
{
    public GetPackageDetailQueryResponse(int id, string name, int duration, int commission)
    {
        Id = id;
        Name = name;
        Duration = duration;
        Commission = commission;
    }
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Duration { get; private set; }
    public int Commission { get; private set; }
}
