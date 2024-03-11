namespace OpenTelemetry.WebService.Product.Repository.Models;

/// <summary>
/// 產品
/// </summary>
public class ProductDataModel
{
    /// <summary>
    /// 流水號
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// 產品編號
    /// </summary>
    public string ProductNo { get; set; }
    
    /// <summary>
    /// 產品名稱
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 產品金額
    /// </summary>
    public double Price { get; set; }
    
    /// <summary>
    /// 建立日期
    /// </summary>
    public DateTime CreateDate { get; set; }
    
    /// <summary>
    /// 更新日期
    /// </summary>
    public DateTime? UpdateDate { get; set; }
}
