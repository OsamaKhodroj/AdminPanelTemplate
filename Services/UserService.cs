using Domains.Dtos;
using Domains.Entities;
using Domains.Enums;
using Domains.Interfaces;
using Infrastructure;
using System.Net.Http.Headers;

namespace Services
{
    public class UserService : IUser
    {
        private static List<User> _users;

        public UserService()
        {
            if (_users == null)
            {
                _users = new List<User>();
            }
        }

        public OpStatus Add(User user, out string errorMessageParam)
        {
            try
            {
                var checkIsExists = _users.Where(q => q.EmailAddress == user.EmailAddress)
                    .Any();

                if (checkIsExists)
                {
                    errorMessageParam = "";
                    return OpStatus.UserAlreadyExists;
                }
                else
                {
                    var userCount = _users.Count;
                    if (userCount <= 0)
                    {
                        user.Id = 1;
                    }
                    else
                    {
                        var maxUserId = _users.Max(q => q.Id);
                        user.Id = maxUserId + 1; // Auto increment 
                    }
                    user.UserStatus = UserStatus.Active;
                    user.UserType = UserType.User;

                    var isPasswordComplex = Tools.ValidatePassword(user.Password, out string errorMessage);
                    if (!isPasswordComplex)
                    {
                        errorMessageParam = errorMessage;
                        return OpStatus.Warning;
                    }

                    user.Password = Cryptography.Hash(user.Password);
                    _users.Add(user);
                    errorMessageParam = "";
                    return OpStatus.Success;
                }
            }
            catch (Exception)
            {
                errorMessageParam = "";
                return OpStatus.Error;
            }
        }

        public DashboardStatitics DashboardStatitics()
        {
            var result = _users.Where(q => q.UserStatus == UserStatus.Active);

            var DashboardStatitics = new DashboardStatitics()
            {
                TotalNumberOfUsers = result.Where(q => q.UserType == UserType.User).Count(),
                TotalNumberOfAdmins = result.Where(q => q.UserType == UserType.Admin).Count(),
            }; 

            return DashboardStatitics;
        }

        public OpStatus Delete(User user)
        {
            try
            {
                user.UserStatus = UserStatus.Inactive;
                return OpStatus.Success;
            }
            catch (Exception)
            {
                return OpStatus.Error;
            }
        }

        public User Get(int id)
        {
            try
            {
                var userInfo = _users.Where(q => q.Id == id)
                    .FirstOrDefault();
                return userInfo;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<GetAllUserListResponse> GetAll()
        {
            try
            {
                var userList = _users
                    .Where(q => q.UserStatus == UserStatus.Active)
                    .Select(q => new GetAllUserListResponse()
                    {
                        Id = q.Id,
                        FullName = $"{q.FirstName} {q.LastName}",
                        EmailAddress = q.EmailAddress,
                        PhoneNumber = q.PhoneNumber,
                        Country = q.Country
                    })
                    .ToList();
                return userList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<User> Search(string query)
        {
            try
            {
                var userList = _users.Where(q => q.UserStatus == UserStatus.Active
                                && (q.FirstName.ToLower() == query || q.LastName.ToLower() == query))
                .ToList();
                return userList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public OpStatus Update(User user)
        {
            try
            {
                var OldUserInfo = _users.Where(q => q.Id == user.Id)
                .FirstOrDefault();

                if (OldUserInfo != null)
                {
                    OldUserInfo.FirstName = user.FirstName;
                    OldUserInfo.LastName = user.LastName;
                    OldUserInfo.EmailAddress = user.EmailAddress;
                    OldUserInfo.PhoneNumber = user.PhoneNumber;
                    OldUserInfo.Address = user.Address;
                    OldUserInfo.Country = user.Country;
                    return OpStatus.Success;
                }
                else
                {
                    return OpStatus.Warning;
                }
            }
            catch (Exception)
            {
                return OpStatus.Error;
            }
        }
    }
}
