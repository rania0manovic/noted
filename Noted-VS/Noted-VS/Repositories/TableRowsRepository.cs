using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noted.Data;
using Noted.Entities;
using Noted.ViewModels;

namespace Noted.Repositories
{
    public class TableRowsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TableRowsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TableRow> AddAsync([FromBody] TableRowVM rowVM, CancellationToken cancellationToken = default)
        {
            var row = new TableRow()
            {
                TableId = rowVM.TableId,
            };
            await _dbContext.TableRows.AddAsync(row, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            for (int i = 0; i < rowVM.Columns; i++)
            {
                var rowData = new TableRowData()
                {
                    TableRowId = row.Id,
                    Data = ""
                };
                await _dbContext.TableRowDatas.AddAsync(rowData, cancellationToken);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return row;
        }
        public async Task DeleteAsync(int rowId, CancellationToken cancellationToken = default)
        {
            var row = await _dbContext.TableRows.FindAsync(new object?[] { rowId }, cancellationToken: cancellationToken);
            if (row != null)
            {
                _dbContext.TableRows.Remove(row);
            }
            var items = await _dbContext.TableRowDatas.Where(tb => tb.TableRowId == rowId).ToListAsync(cancellationToken: cancellationToken);
            if (items != null)
            {
                foreach (var item in items)
                {
                    _dbContext.TableRowDatas.Remove(item);
                }
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
