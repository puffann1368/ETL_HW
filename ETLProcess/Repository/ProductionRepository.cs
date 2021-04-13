using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ETLProcess.Model;
using ETLProcess.Interface.IRepository;
using Microsoft.Extensions.Logging;

namespace ETLProcess.Repository
{
    public class ProductionRepository: IProductionRepo
    {
        private readonly ILogger<ProductionRepository> _logger;

        private readonly DbContextOptions<ETLDbContext> _options;

        public ProductionRepository(ILogger<ProductionRepository> logger,DbContextOptions<ETLDbContext> options)
        {
            _logger = logger;
            _options = options;
        }

        public async Task<Dictionary<string, int>> Add(List<string> ProductionNames){
            try{
                    using(var context = new ETLDbContext(_options)){
                        var list = await 
                                    (from po in context.Productions 
                                    select po).ToListAsync();   

                        var MappingNeedAddList = from o in ProductionNames
                                          join p in list
                                          on o equals p.ProductionName into groupjoin 
                                          from a in groupjoin.DefaultIfEmpty()
                                          where a == null
                                          select o ;
                        
                        _logger.LogInformation($"count of productions need to add:{MappingNeedAddList.Count()}");
                        
                        if(MappingNeedAddList.Count()>0){
                            foreach(var p in MappingNeedAddList){
                                Production prod = new Production();
                                prod.ProductionName = p;
                                context.Productions.Add(prod);
                            }
                            await context.SaveChangesAsync();
                            
                            var after_list = await 
                                (from po in context.Productions 
                                select po).ToListAsync();   
                            return list.ToDictionary(data => data.ProductionName,data=>data.ProductionID);
                        }else{
                            return list.ToDictionary(data => data.ProductionName,data=>data.ProductionID);
                        }
                    }
            }catch(Exception ex){
                _logger.LogError($"Add Production got errors:{ex.Message}");
                throw ex;
            }
        }

       

    }
}