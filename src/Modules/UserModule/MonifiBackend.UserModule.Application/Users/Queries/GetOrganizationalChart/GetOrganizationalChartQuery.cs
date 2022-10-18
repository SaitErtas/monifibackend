using FluentValidation;
using Microsoft.Extensions.Localization;
using MonifiBackend.Core.Application.Abstractions;
using MonifiBackend.Core.Infrastructure.Localize;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MonifiBackend.UserModule.Application.Users.Queries.GetOrganizationalCharts
{
    public class GetOrganizationalChartQuery : IQuery<GetOrganizationalChartQueryResponse>
    {
        public GetOrganizationalChartQuery(int userId)
        {
            UserId = userId;
        }
        [Required]
        [SwaggerSchema(Nullable = false)]
        public int UserId { get; }

    }
    internal class GetOrganizationQueryValidator : AbstractValidator<GetOrganizationalChartQuery>
    {
        public GetOrganizationQueryValidator(IStringLocalizer<Resource> stringLocalizer)
        {
   

        }
    }
}
