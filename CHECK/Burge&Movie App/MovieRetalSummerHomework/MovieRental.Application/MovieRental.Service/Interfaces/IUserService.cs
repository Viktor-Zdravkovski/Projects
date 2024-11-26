using MovieRental.DataBase.Interfaces;
using MovieRental.Domain;
using MovieRental.Dtos.Dto;

namespace MovieRental.Service.Interfaces
{
    public interface IUserService
    {
        //UserDto GetUserById(int id);

        UserDto GetCurrentUser(string cardNumber);

        User Login(string cardNumber, string email);

        User Register(User user);

        bool IsAccountValid(string cardNumber, string email);

        User RegisterValidation(string cardNumber, string email, int age, string fullName);


    }
}