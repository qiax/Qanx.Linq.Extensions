
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
        Code = d.Code
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