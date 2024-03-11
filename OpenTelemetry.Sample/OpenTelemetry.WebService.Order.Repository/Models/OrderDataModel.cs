namespace OpenTelemetry.WebService.Order.Repository.Models;

/// <summary>
/// 訂單
/// </summary>
public class OrderDataModel
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
