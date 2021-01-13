using System.Threading.Tasks;
using EVoteSystem.Policies.Services;
using Microsoft.AspNetCore.Authorization;

namespace EVoteSystem.Policies
{
    public class StudentRequirementHandler : AuthorizationHandler<StudentRequirement>
    {
        private readonly IStudentService _service;

        public StudentRequirementHandler(IStudentService service)
        {
            _service = service;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, StudentRequirement requirement)
        {
            var type = _service.GetLoggedInUserType(context.User);
            if (type == requirement.LoggedInAs)
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}