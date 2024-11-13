public abstract class BaseCanvas<T> where T : BaseUIManager<T>
{
    public abstract void Initialize(T uiManager);
}