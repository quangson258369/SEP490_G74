namespace HCS.Business.RequestModel.ServiceTypeRequestModel;

public class ServiceTypeAddModel
{
    public string ServiceTypeName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
}