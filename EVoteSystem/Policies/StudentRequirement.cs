using Microsoft.AspNetCore.Authorization;

namespace EVoteSystem.Policies
{
    public enum LoggedInUserType
    {
        Student,
        Candidate,
        Nothing,
    }
    public class StudentRequirement : IAuthorizationRequirement
    {
        public LoggedInUserType LoggedInAs { get; }

        public StudentRequirement(LoggedInUserType loggedInAs)
        {
            LoggedInAs = loggedInAs;
        }
    }

    public static class PolicyBuilderExtension
    {
        public static AuthorizationPolicyBuilder RequireUserType(this AuthorizationPolicyBuilder builder,
            LoggedInUserType type)
        {
            builder.AddRequirements(new StudentRequirement(type));
            return builder;
        }
    }
}