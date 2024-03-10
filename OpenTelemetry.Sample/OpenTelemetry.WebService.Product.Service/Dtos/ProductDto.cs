namespace OpenTelemetry.WebService.Product.Service.Dtos;

/// <summary>
/// 產品 Dto
/// </summary>
public class ProductDto
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
