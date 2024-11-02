using Social.Net.Core.Domains.Common;

namespace Social.Net.Core;

public class PagedList<T> where T : BaseEntity
{
    public IList<T> Data { get; set; }

    public int PageIndex { get; set; }

    public int TotalCount { get; set; }

    public int MaximumPageItemCount { get; set; }

    public int Count => Data.Count;

    public int NumberOfPages
    {
        get
        {
            var quotient = TotalCount / MaximumPageItemCount;
            var reminder = TotalCount % MaximumPageItemCount;

            return quotient + (reminder > 0 ? 1 : 0);
        }
    }

    public bool HasPrevious => PageIndex > 0;

    public bool HasNext => PageIndex * MaximumPageItemCount + Count < TotalCount;
}