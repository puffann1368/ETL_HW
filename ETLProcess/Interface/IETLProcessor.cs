using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ETLProcess.Interface{
    public interface IETLProcessor{
        Task Run(string SourcePath,CancellationToken StoppingToken);
        void CollectDataInfo();
        void ExtractData();
        Task Transformation();

    }
}