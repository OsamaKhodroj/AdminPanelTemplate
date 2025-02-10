using Domains.Dtos;
using Domains.Entities;
using Domains.Enums;

namespace Domains.Interfaces
{
    public interface IUser
    {
        OpStatus Add(User user, out string errorMessage);
        OpStatus Update(User user);
        OpStatus Delete(User user);
        User Get(int id);
        List<GetAllUserListResponse> GetAll();
        List<User> Search(string query);
        DashboardStatitics DashboardStatitics();
    }
}
