namespace Symbotic.GrainInterfaces
{
    using System.Threading.Tasks;
    using Orleans;

    /// <summary>
    /// Grain interface IHello
    /// </summary>
    public interface IHello : IGrainWithGuidKey
    {
        Task<string> SayHello(string greeting);
    }
}
