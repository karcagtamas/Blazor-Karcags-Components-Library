namespace Karcags.Blazor.Common.Models.Interfaces
{
    public interface IListState
    {
        public void Add<T>(T value, int index);

        public T Get<T>(int index);

        public void TryAdd<T>(T value, int index);

        public T TryGet<T>(int index);

        public int Count();

        public string ToString();
    }
}