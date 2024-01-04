namespace HCS.Business.ResponseModel.ServiceTypeResponseModel;

public class ServiceTypeResponseModel
{
    public string ServiceTypeName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public int ServiceTypeId { get; set; }
}

public class ServiceResponseModel
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = string.Empty;

    public decimal Price { get; set; }
}