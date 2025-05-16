namespace Energycom.Ingestion.Models;

public record ReadingModelV1(Guid ReadingId, string ReadingRawJson, string MeterMeterNumber, DateTime ReadingReadingDate, int GroupId, string ReadingUnit, decimal ReadingValue);