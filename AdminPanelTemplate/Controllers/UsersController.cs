using Domains.Dtos;
using Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace AdminPanelTemplate.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            var response = new AddUserResponse();
            response.Message = string.Empty;
            response.Status = Domains.Enums.OpStatus.None;

            return View(response);
        }

        [HttpPost]
        public IActionResult AddUser(AddUserRequest request)
        {
            var user = new User();
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.EmailAddress = request.EmailAddress;
            user.Password = request.Password;
            user.PhoneNumber = request.PhoneNumber;
            user.Address = request.Address;
            user.Country = request.Country;

            var userServices = new UserService();
            var result = userServices.Add(user, out string userMessage);

            string message = string.Empty;

            if (result == Domains.Enums.OpStatus.Success)
            {
                message = "User Added Sucessfully";
            }
            else if (result == Domains.Enums.OpStatus.UserAlreadyExists)
            {
                message = "User Email Already Exists";
            }
            else
            {
                message = $"Failed to Add User - {userMessage}";
            }

            var response = new AddUserResponse();
            response.Message = message;
            response.Status = result;


            return View("Add", response);
        }




        [HttpGet]
        public IActionResult Manage()
        {
            var userService = new UserService();
            var result = userService.GetAll();

            return View(result);
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
                throw new Exception("Invalid User Id");

            var userService = new UserService();
            var result = userService.Get(id);

            var response = new EditUserResponse();
            response.User = result;
            response.Message = string.Empty;
            response.Status = Domains.Enums.OpStatus.None;

            return View(response);
        }

        [HttpPost]
        public IActionResult EditSubmit(User request)
        {
            var service = new UserService();
            var result = service.Update(request);


            string message = string.Empty;

            if (result == Domains.Enums.OpStatus.Success)
            {
                message = "User Updated Sucessfully";
            }
            else if (result == Domains.Enums.OpStatus.UserAlreadyExists)
            {
                message = "User Email Already Exists";
            }
            else
            {
                message = $"Failed to Add User";
            }

            var response = new EditUserResponse();
            response.Message = message;
            response.Status = result;
            response.User = service.Get(request.Id);
            return View("Edit", response);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return View();

            var userService = new UserService();
            var user = userService.Get(id);
            userService.Delete(user);


            return Redirect("Manage");
        }
    }
}
