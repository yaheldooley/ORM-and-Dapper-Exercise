namespace ORM_Dapper
{
	public class Product
	{
		public int ProductID { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int CategoryID { get; set; }
		public decimal OnSale { get; set; }
		public string StockLevel { get; set; }
	}

}
