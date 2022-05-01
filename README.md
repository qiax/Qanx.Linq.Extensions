
# Qanx.Linq.Extensions
[中文文档 Chinese version](https://github.com/qiax/Qanx.Linq.Extensions/blob/main/README_CN.md)  

System.Linq Extension, You can use LINQ where in a fluent way, and we also provide an extension of paging query.

If you have a query condition and you are not sure it will have value, You can use `Whereif`, It will judge whether the value of the query condition is empty. If the value of the query condition is not empty or null, It will process your query according to the query criteria you refer to.
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
LINQ based query is so simple, elegant and fluent. You can also use our paging function, It is extended based on IOrderedqueryable, You can use it like this:
```csharp
int pageIndex = 1;
int pageSize = 10;
var result = await query.ToPagedListAsync(pageIndex, pageSize);
```
We also provide a non asynchronous way:
```csharp
var result = query.ToPagedList(pageIndex, pageSize);
```
If you need to deal with the results of paging queries in a special way, You can do this(The premise is that you must introduce namespaces`Qanx.Linq.Extensions`):
```csharp
var result = query.ToPagedList(pageIndex, pageSize)
    .ForEach(item =>
    {
        item.Name = "prefix_" + item.Name;
        item.Code = item.Code.ToString();
        item.HoldingDateTimeStr = item.HoldingDateTime.ToString("yyyy-MM-dd HH:mm");
    });
```
You can also use asynchronous methods`ForEachAsync`.