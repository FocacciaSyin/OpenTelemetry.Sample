namespace OpenTelemetry.WebService.Order.Service.Dtos;

/// <summary>
/// 訂單Dto
/// </summary>
public class OrderDto
{
    /// <summary>
    /// 流水號
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// 訂單編號
    /// </summary>
    public string OrderNo { get; set; }
    
    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// 建立日期
    /// </summary>
    public DateTime CreateDate { get; set; }
    
    /// <summary>
    /// 更新日期
    /// </summary>
    public DateTime? UpdateDate { get; set; }
}
