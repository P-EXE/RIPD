using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace RIPDApp.Services;

/// <summary>
/// Abstracts the HttpClient class.
/// Is often injected into other Services which require http functionality and is not designed to be used directly.
/// </summary>
public interface IHttpService
{
  /// <summary>
  /// Injects the AuthHeader for the HttpClient in use if available.
  /// Otherwise takes the current AuthHeader from the Statics Class.
  /// </summary>
  /// <returns>The result of the injection</returns>
  Task<bool> Authorize(AuthenticationHeaderValue authHeader = null);

  /// <summary>
  /// Gets an Object of the specified Type from the given route.
  /// </summary>
  /// <typeparam name="T">The Type of the Object that should be returned</typeparam>
  /// <param name="route">The route from which to get the Object</param>
  /// <param name="caller">The name of the calling Method (does not need to be specified)</param>
  /// <returns>An Object of the specified Type, otherwise null</returns>
  Task<T?> GetAsync<T>(string route, [CallerMemberName] string caller = "");

  /// <summary>
  /// Gets an Object of the specified Type from the given route using the specified Query.
  /// </summary>
  /// <typeparam name="T">The Type of the Object that should be returned</typeparam>
  /// <param name="route">The route from which to get the Object</param>
  /// <param name="queriesDict">The Query parameters in the form of Property : Value</param>
  /// <param name="caller">The name of the calling Method (does not need to be specified)</param>
  /// <returns>An Object of the specified Type, otherwise null</returns>
  Task<T?> GetAsync<T>(string route, Dictionary<string, string> queriesDict, [CallerMemberName] string caller = "");

  /// <summary>
  /// Posts an Object of the specified Type to the given route.
  /// </summary>
  /// <typeparam name="T">The Type of the Object that should be posted</typeparam>
  /// <param name="route">The route to which the Object should be posted</param>
  /// <param name="t">The Object that should be posted</param>
  /// <param name="caller">The name of the calling Method (does not need to be specified)</param>
  /// <returns>The result of the post as successful or unsuccessful</returns>
  Task<bool> PostAsync<T>(string route, T t, [CallerMemberName] string caller = "");

  /// <summary>
  /// Posts an Object of the specified Type to the given route.
  /// Should only be used for debugging.
  /// </summary>
  /// <typeparam name="T">The Type of the Object that should be posted</typeparam>
  /// <param name="route">The route to which the Object should be posted</param>
  /// <param name="t">The Object that should be posted</param>
  /// <param name="caller">The name of the calling Method (does not need to be specified)</param>
  /// <returns>All the information the destination returned</returns>
  Task<HttpResponseMessage?> FullPostAsync<T>(string route, T t, [CallerMemberName] string caller = "");

  /// <summary>
  /// Posts an Object of the specified Type to the given route.
  /// Returns an Object of the specified Type.
  /// </summary>
  /// <typeparam name="T1">The Type of the Object that should be posted</typeparam>
  /// <typeparam name="T2">The Type of the Object that should be returned</typeparam>
  /// <param name="route">The route to which the Object should be posted</param>
  /// <param name="t1">The Object that should be posted</param>
  /// <param name="caller">The name of the calling Method (does not need to be specified)</param>
  /// <returns>The an Object of the desired Type</returns>
  Task<T2?> PostAsync<T1, T2>(string route, T1 t1, [CallerMemberName] string caller = "");
}
