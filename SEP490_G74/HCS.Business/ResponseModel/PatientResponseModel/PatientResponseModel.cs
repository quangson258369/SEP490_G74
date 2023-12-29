namespace HCS.Business.ResponseModel.PatientResponseModel;

public class PatientResponseModel
{
    public string ServiceDetailName { get; set; } = null!;

    public byte? Height { get; set; }

    public byte? Weight { get; set; }

    public string? BloodGroup { get; set; }

    public byte? BloodPressure { get; set; }

    public string? Allergieshistory { get; set; }
}