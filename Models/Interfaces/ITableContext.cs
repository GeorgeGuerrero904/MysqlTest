namespace MySqlTest.Models.Interfaces
{
    public interface ITableContext
    {
        List<Table> GetAllInfo();
        bool InsertRow(Table table);
        bool EditRow(Table table);
        Table SearchTable(int id);
    }
}
