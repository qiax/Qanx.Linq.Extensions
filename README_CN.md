
# Qanx.Linq.Extensions
对 System.Linq 的扩展, 你可以以流利的方式来使用我们的扩展，我们提供了判空查询方式、分页查询等功能。

如果您有一个查询条件，但不确定它是否有值，那么你可以使用`Whereif`。它会判断查询条件的值是否为空，如果查询条件的值不为空或null，它将根据你指定的查询条件处理您的查询。
```csharp
bool? isClearance = null;
var query = _dbContext.Tradings.AsNoTracking()
    .WhereIf(isClearance, d => d.IsClearance)
    .Select(d => new TradingDto()
    {
        Id = d.Id,
        Name = d.Name,
        Code = d.Code,
        HoldingDateTime = d.HoldingDateTime
    }).OrderByDescending(d => d.Id);
```
基于LINQ的查询非常简单、优雅、流畅。您也可以使用我们的分页功能，它是基于IOrderedqueryable扩展的，您可以这样使用它：
```csharp
int pageIndex = 1;
int pageSize = 10;
var result = await query.ToPagedListAsync(pageIndex, pageSize);
```
We also provide a non asynchronous way:
```csharp
var result = query.ToPagedList(pageIndex, pageSize);
```
如果你需要对分页查询的结果进行特别的处理，你可以这样做（前提是你要先引入命名空间`Qanx.Linq.Extensions`）：
```csharp
var result = query.ToPagedList(pageIndex, pageSize)
    .ForEach(item =>
    {
        item.Name = "prefix_" + item.Name;
        item.Code = item.Code.ToString();
        item.HoldingDateTimeStr = item.HoldingDateTime.ToString("yyyy-MM-dd HH:mm");
    });
```
你也可是使用异步方法`ForEachAsync`。