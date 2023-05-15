namespace Accounting.Model.DTO
{
    public class Header
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool Ordering { get; set; } = false;

        public bool IsHtml { get; set; } = false;
    }
}
