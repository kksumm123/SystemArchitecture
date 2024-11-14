using Cysharp.Threading.Tasks;

public class FactoryManager : MonoSingleton<FactoryManager>
{
    public readonly FactoryMap Map = new();

    public async UniTask Initialize()
    {
        await Map.Initialize(transform);
    }
}