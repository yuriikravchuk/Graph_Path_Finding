using pathFinding;

public class PathFinder : ICellsForSearchHandler
{
    private CellPresenter _start, _end;
    private readonly IPathFindingAlgorithmProvider _algorithmProvider;

    public PathFinder(IPathFindingAlgorithmProvider algorithmProvider) => _algorithmProvider = algorithmProvider;

    public SearchingResults GetResults()
    {
        if(_start == null || _end == null)
            throw new System.InvalidOperationException();

        PathFindingAlgorithm algorithm = _algorithmProvider.CurrentAlgorithm;

        return algorithm.GetSearchingResults(_start, _end);
    }

    public void SetCells(CellPresenter start, CellPresenter end)
    {
        _start = start;
        _end = end;
    }
}
