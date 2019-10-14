using AutoMapper;

/// <summary>
/// @author Ankit
/// @date - 10/14/2019 10:56:07 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Interfaces.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHaveCustomMapping
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        void CreateMappings(Profile configuration);
    }
}
