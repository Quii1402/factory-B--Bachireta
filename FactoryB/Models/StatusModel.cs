namespace FactoryB.Models
{
    public class StatusModel
    {
        public int id_status { get; set; }
        public string status_name { get; set; }
        public string description { get; set; }
        public string last_modify_by { get; set; }
        public DateTime? last_modify_at { get; set; }
    }
}
