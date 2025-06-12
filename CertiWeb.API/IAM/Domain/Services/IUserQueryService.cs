using CertiWeb.API.IAM.Domain.Model.Aggregates;
using CertiWeb.API.IAM.Domain.Model.Queries;

namespace CertiWeb.API.IAM.Domain.Services;


public interface IUserQueryService
{

    Task<User?> Handle(GetUserByIdQuery query);


    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    

    Task<User?> Handle(GetUserByUsernameQuery query);
}