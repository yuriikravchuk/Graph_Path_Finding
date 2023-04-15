using pathFinding;
using System.Collections.Generic;

public class SearchingResults
{
    public readonly IReadOnlyList<Transition> SearchingPath;
    public readonly IReadOnlyList<Transition> Path;
    public readonly float SearchingDuration;

    public SearchingResults(IReadOnlyList<Transition> searchingPath, IReadOnlyList<Transition> path, float searchingDuration)
    {
        SearchingPath = searchingPath;
        Path = path;
        SearchingDuration = searchingDuration;
    }
}