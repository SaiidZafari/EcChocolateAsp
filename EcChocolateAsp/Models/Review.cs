namespace EcChocolateAsp.Models
{
    public class Review
    {
        public Review(Product product, string userName, string title, string body, int score)
        {
            Product = product;
            UserName = userName;
            Title = title;
            Body = body;
            Score = score;
        }

        public Review(int id, Product product, string userName, string title, string body, int score)
            :this(product, userName, title, body, score)
        {
            Id = id;
        }

        public int Id { get; protected set; }
        public Product Product { get; protected set; }
        public string UserName { get; protected set; }
        public string Title { get; protected set; }
        public string Body { get; protected set; }
        public int Score { get; protected set; }
    }
}