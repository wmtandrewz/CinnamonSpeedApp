using System;
using System.Threading.Tasks;

namespace SpeedTest.Interfaces
{
    public interface ILogout
    {
        Task<bool> Logout();
    }
}
