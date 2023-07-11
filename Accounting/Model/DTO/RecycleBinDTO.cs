namespace Accounting.Model.DTO
{
    public class RecycleBinDTO
    {
        public int Id { get; set; }

        public int ObjectId { get; set; }

        public string ObjectName { get; set; }

        public int Type { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string LunarCreatedDate { get; set; }

        public string LunarModifiedDate { get; set; }
    }
}
