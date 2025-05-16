namespace Energycom.Ingestion.Entities;

public class Reading
{
    public required Guid Id { get; set; }
    
    public required string RawJson { get; set; }
    
    public required bool Parsed { get; set; }
    
    public required  int MeterId { get; set; }
    
    public required Meter Meter { get; set; }
    
    public string Unit { get; set; }
    public decimal Value { get; set; }
    
    public DateTime ReadingDate { get; set; }
}