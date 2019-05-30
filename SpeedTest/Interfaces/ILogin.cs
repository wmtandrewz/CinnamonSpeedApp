using System;
using System.Threading.Tasks;
using SpeedTest.Models;

namespace SpeedTest.Interfaces
{
    public interface ILogin
    {
        Task<UserModel> LoginUser();
    }
}
