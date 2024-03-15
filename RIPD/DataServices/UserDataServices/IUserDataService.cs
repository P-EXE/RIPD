using RIPD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.DataServices
{
  /// <summary>
  /// <para>
  /// Interface for a UserDataService
  /// </para>
  /// <para>
  /// Should be used for API or Local implementations
  /// </para>
  /// </summary>
  /// <remarks>
  /// Author: Paul
  /// </remarks>
  internal interface IUserDataService
  {
    /// <summary>
    /// Create a User
    /// </summary>
    /// <param name="user">A User</param>
    /// <returns>Nothing</returns>
    public Task CreateAsync(User user);

    /// <summary>
    /// Get a single User via an ID.
    /// </summary>
    /// <param name="id">A unique ID of a User</param>
    /// <returns>A User</returns>
    public Task<User> GetOneAsync(int id);

    /// <summary>
    /// Get a singular User via a singular, matchable, Property-Value pair.
    /// </summary>
    /// <example>
    /// ("email", "email@domain.top")
    /// </example>
    /// <param name="unique">Common Property, unique Value</param>
    /// <returns>A User</returns>
    public Task<User> GetOneAsync((string, string) unique);

    /// <summary>
    /// Get multiple Users via matchable Property-Value pairs.
    /// </summary>
    /// <example>
    /// { "name", "User Name" },
    /// { "foo", "bar" }
    /// </example>
    /// <param name="queryParams">Dictionary of common Properties and common Values</param>
    /// <returns>A List of Users</returns>
    public Task<List<User>> GetMultipleAsync(Dictionary<string,string> queryParams);
  }
}
