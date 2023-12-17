namespace Noted.Entities
{
    public class TableRow
    {
        public int Id { get; set; }

        public Table Table { get; set; }
        public int TableId { get; set; }

        public ICollection<TableRowData> TableRowDatas { get; set; }
    }
}
