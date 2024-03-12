namespace Domain.Models.ValueObjects;

public struct RequestNumber
{
    public int Year { get; init; }
    public int Index { get; init; }

    public RequestNumber(int year, int index) 
    {
        Year = year;
        Index = index;
    }

    public override string ToString() => $"{Year}/{Index:0000}";
}
