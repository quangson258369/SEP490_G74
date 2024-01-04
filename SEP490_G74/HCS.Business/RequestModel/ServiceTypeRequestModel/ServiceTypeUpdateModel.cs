namespace HCS.Business.RequestModel.ServiceTypeRequestModel;

public class ServiceTypeUpdateModel
{
    public string ServiceTypeName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
}