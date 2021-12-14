namespace DAL;

public abstract class StoreIndex
{
    protected static List<uint> _listOfUsedIDs = new();

    public uint ID
    {
        get => _id;
        set => _id = !_listOfUsedIDs.Contains(value) ? value : throw new Exception();
    }

    private uint _id;

    public StoreIndex()
    {
        uint lastID = _listOfUsedIDs.Count == 0 ? 0 : _listOfUsedIDs.Last();

        for (; true; lastID++)
            if (!_listOfUsedIDs.Contains(lastID))
                break;

        _id = lastID;
        _listOfUsedIDs.Add(_id);
    }

    ~StoreIndex()
    {
        _listOfUsedIDs.Remove(_id);
    }
}