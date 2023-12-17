namespace Noted.Entities
{
    public class TableRowData
    {
        public int Id { get; set; }
        public string Data { get; set; }

        public TableRow TableRow { get; set; }
        public int TableRowId { get; set; }

    }
}
