using System.Collections.Generic;
using System.Threading.Tasks;

namespace ETLProcess.Interface.IRepository
{
    public interface IProductionRepo
    {
        Task<Dictionary<string,int>> Add(List<string> ProductionNames);
    }
}