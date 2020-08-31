using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Authorization.Handlers
{
    public class AuthorHandler: AuthorizationHandler<AuthorRequirement>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorHandler(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return;
            }

            var questionIdAsObj = _httpContextAccessor.HttpContext.Request.RouteValues["id"];
            int questionId = Convert.ToInt32(questionIdAsObj);

            string userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var question = await _dbContext.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
            if (question == null)
            {
                context.Succeed(requirement);
                return;
            }
            
            if (question.UserId != userId)
            {
                context.Fail();
                return;
            }
            
            context.Succeed(requirement);
        }
    }
}