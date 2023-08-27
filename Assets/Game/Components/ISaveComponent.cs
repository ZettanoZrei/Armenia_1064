interface ISaveComponent<TData>
{
    TData GetData();
    void LoadData(TData data);
}

