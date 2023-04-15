public interface ISaveProvider<T>
{
    T TryGetSave(string name, string extension, string folder);
    void UpdateSave(T save, string name, string extension, string folder);
}