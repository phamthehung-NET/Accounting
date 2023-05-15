namespace Accounting.Model.DTO
{
    public class Header
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string OrderId { get; set; }

        public bool Ordering
        {
            get
            {
                return !string.IsNullOrEmpty(OrderId);
            }
        }
    }
}
