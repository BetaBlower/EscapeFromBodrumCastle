namespace EscapeFromBodrumCastle.Entities
{
    public class Player
    {
        public string? Name;
        public List<Item>? Inventory { get; set; }
        public List<Note>? Notes { get; set; }
        public bool Visibility = true;
        public int[]? Position { get; set; }
        public int? MapLevel { get; set; }



    }
}
