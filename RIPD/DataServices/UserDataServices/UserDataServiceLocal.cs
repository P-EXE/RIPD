using Microsoft.EntityFrameworkCore;
using RIPD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.DataServices
{
  internal class UserDataServiceLocal : IUserDataServiceLocal
  {
    private readonly LocalDBContext _localDBContext;
    public UserDataServiceLocal(LocalDBContext localDBContext)
    {
      _localDBContext = localDBContext;
    }
    public async Task CreateAsync(User user)
    {
      _localDBContext.AddAsync(user);
    }
    public async Task<User> GetFirstAsync()
    {
      return await _localDBContext.Users.FirstAsync();
    }
  }
}
