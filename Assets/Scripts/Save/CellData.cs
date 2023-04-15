using UnityEngine;

[System.Serializable]
public struct CellData
{
    public ConnectionData[]  Connections;

    public Vector3Serializable _position;
    public CellData(Vector3 position, ConnectionData[] connections)
    {
        _position = new Vector3Serializable(position);
        Connections = connections;
    }

    [System.Serializable]
    public struct Vector3Serializable
    {
        public float X, Y, Z;

        public Vector3Serializable(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3Serializable(Vector3 vector)
        {
            X = vector.x;
            Y = vector.y;
            Z = vector.z;
        }

        public Vector3 ToVector3() => new Vector3(X, Y, Z);
    }
}