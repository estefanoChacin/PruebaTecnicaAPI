
namespace PruebaAPI.MODEL
{
    public class Products
    {
        public int idProduct { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Stock { get; set; }
        public decimal? Price { get; set; }
        public DateTime  DatedCreate { get; set; }
        public virtual Category? Categoria { get; set; }
    }
}
