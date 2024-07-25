namespace Common.Query.Filter;

public class BaseFilter
{
    public int EntityCount { get; private set; }//چندتا انتیی وجود داره چندتا کامنت وجود داره

    public int CurrentPage { get; private set; }//میگه رو کدوم صفحه هستیم

    public int PageCount { get; private set; }//تعداد کل رو میگه مثلا 108 تا صفحه داریم

    public int StartPage { get; private set; }//از چه صفحه ای شروع بشه 

    public int EndPage { get; private set; }//

    public int Take { get; private set; }//چندتا تو صفحه نمایش بده


    public void GeneratePaging(IQueryable<Object> data, int take, int currentPage)
    {
        var entityCount = data.Count();
        var pageCount = (int)Math.Ceiling(entityCount / (double)take);
        PageCount = pageCount;
        CurrentPage = currentPage;
        EndPage = (currentPage + 5 > pageCount) ? pageCount : currentPage + 5;
        EntityCount = entityCount;
        Take = take;
        StartPage = (currentPage - 4 <= 0) ? 1 : currentPage - 4;
    }

    public void GeneratePaging(int count, int take, int currentPage)
    {
        var entityCount = count;
        var pageCount = (int)Math.Ceiling(entityCount / (double)take);
        PageCount = pageCount;
        CurrentPage = currentPage;
        EndPage = (currentPage + 5 > pageCount) ? pageCount : currentPage + 5;
        EntityCount = entityCount;
        Take = take;
        StartPage = (currentPage - 4 <= 0) ? 1 : currentPage - 4;
    }


}
public class BaseFilterParam
{
    public int PageId { get; set; } = 1;

    public int Take { get; set; } = 10;

}
public class BaseFilter<TData, TParam> : BaseFilter where TParam : BaseFilterParam     where TData : BaseDto
{
    public List<TData> Data { get; set; }

    public TParam FilterParam { get; set; }

}

