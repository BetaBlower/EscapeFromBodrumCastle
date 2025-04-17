namespace EscapeFromBodrumCastle.Entities
{
    public class Item
    {
        public required int ID { get; set; }
        public required string Name { get; set; }
        public required string ShortName { get; set; }
        public string? Description { get; set; }
    }
}