

namespace HCS.Business.RequestModel.MedicalRecordRequestModel
{
    public class MedicalRecordAddModel
    {
        public string ExamReason { get; set; } = string.Empty;

        public List<int> CategoryIds { get; set; } = null!;

        public int PatientId { get; set; }

        public List<int> DoctorIds { get; set; } = null!;

        public int? PreviousMedicalRecordId { get; set; }
    }
}