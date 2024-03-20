using RIPD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.DataServices
{
  public interface IRunDataService
  {
    Task AddRunAsync(User user, Run run);
    Task<Run> GetRunAsync(User user, int id);
    Task<List<Run>> GetAllRunsByUserAsync(User user);
  }
}
