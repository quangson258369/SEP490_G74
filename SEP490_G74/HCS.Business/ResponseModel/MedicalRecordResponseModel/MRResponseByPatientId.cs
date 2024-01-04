namespace HCS.Business.ResponseModel.MedicalRecordResponseModel;

public class MrResponseByPatientId
{
    public int MedicalRecordId { get; set; }
    public DateTime MedicalRecordDate { get; set; } = DateTime.Now;
    public string CategoryName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int PatientId { get; set; }
}