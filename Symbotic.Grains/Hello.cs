namespace Symbotic.Grains
{
    using System.Threading.Tasks;
    using GrainInterfaces;
    using Orleans;

    /// <summary>
    /// Grain implementation class Grain1.
    /// </summary>
    public class Hello : Grain, IHello
    {
        public Task<string> SayHello(string greeting)
        {
            return Task.FromResult("You said: '" + greeting + "', I say: Hello!");
        }
    }
}
