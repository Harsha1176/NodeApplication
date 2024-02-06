namespace NodeViewApplication.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual List<City> City { get; set; }
    }
}
