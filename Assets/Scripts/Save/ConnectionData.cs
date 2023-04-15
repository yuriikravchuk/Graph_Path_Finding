using System;
using static Connection;

[System.Serializable]
public struct ConnectionData
{
    public readonly ConnectionType ConnectionType;
    public readonly int StartCellNumber, EndCellNumber;
    public readonly int Weight;

    public ConnectionData(ConnectionType connectionType, int startCellNumber, int endCellNumber, int weight)
    {
        ConnectionType = connectionType;
        StartCellNumber = startCellNumber;
        EndCellNumber = endCellNumber;
        Weight = weight;
    }

    public int GetOtherCellNumber(int number)
    {
        if (number == StartCellNumber)
            return EndCellNumber;
        else if (number == EndCellNumber)
            return StartCellNumber;
        else
            throw new InvalidOperationException();
    }
}