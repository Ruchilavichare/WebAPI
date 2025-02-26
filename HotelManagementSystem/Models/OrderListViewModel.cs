namespace HotelManagementSystem.Models
{
    public class OrderListViewModel
    {
        public List<Order> Orders { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalOrders { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalOrders / PageSize);
    }
}
