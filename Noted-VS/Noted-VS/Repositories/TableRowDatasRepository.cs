using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noted.Data;
using Noted.Entities;
using Noted.ViewModels;

namespace Noted.Repositories
{
    public class TableRowDatasRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TableRowDatasRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TableRowData>> AddAsync([FromBody] TableRowDataVM rowData, CancellationToken cancellationToken = default)
        {
            var list = new List<TableRowData>();
            for (int i = 0; i < rowData.NumberOf; i++)
            {
                var row = new TableRowData()
                {
                    TableRowId = rowData.RowId,
                    Data = ""
                };
                list.Add(row);
                await _dbContext.TableRowDatas.AddAsync(row, cancellationToken);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return list;
        }

        public async Task<TableRowData?> EditAsync([FromBody] TableRowDataEditVM data, CancellationToken cancellationToken = default)
        {
            var tableData = await _dbContext.TableRowDatas.FindAsync(new object?[] { data.DataId }, cancellationToken: cancellationToken);
            if (tableData != null)
            {
                tableData.Data = data.Data;
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return tableData;
        }

        public async Task DeleteAsync(int rowId, CancellationToken cancellationToken = default)
        {
            var tableData = await _dbContext.TableRowDatas.Where(tb => tb.TableRowId == rowId).ToListAsync();
            if (tableData != null)
            {
                foreach (var item in tableData)
                {
                    _dbContext.TableRowDatas.Remove(item);
                }
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
