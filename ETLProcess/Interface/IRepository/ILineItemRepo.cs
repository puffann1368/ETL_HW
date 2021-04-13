using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ETLProcess.Component;

namespace ETLProcess.Interface.IRepository
{
    public interface ILineItemRepo
    {
        Task BulkInsert(List<LineItemDto> lineItemDtos,DateTime Date, string AccountID);
    }
}