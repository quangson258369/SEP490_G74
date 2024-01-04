namespace HCS.Business.RequestModel.PrescriptionRequestModel;

public class PrescriptionUpdateModel
{
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public string Diagnose { get; set; } = string.Empty;
}