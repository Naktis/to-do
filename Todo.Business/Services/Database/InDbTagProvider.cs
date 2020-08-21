using Todo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Todo.Business.Services.Database
{
    public class InDbTagProvider : IDataProviderAsync<TagVo>
    {
        private readonly Data.Context.AppContext context;
        private readonly IMapper mapper;
        public InDbTagProvider(Data.Context.AppContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task Add(TagVo item)
        {
            context.Add(entity: mapper.Map<TagDao>(item));
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var tag = await context.Tags.FindAsync(id);
            context.Tags.Remove(tag);
            await context.SaveChangesAsync();
        }

        public async Task Edit(TagVo changes)
        {
            context.Update(entity: mapper.Map<TagDao>(changes));
            await context.SaveChangesAsync();
        }

        public async Task<TagVo> Get(int id)
        {
            var tag = await context.Tags.FindAsync(id);
            return mapper.Map<TagVo>(tag);
        }

        public async Task<List<TagVo>> GetAll()
        {
            var tags = await context.Tags.ToListAsync();
            return mapper.Map<List<TagVo>>(tags);
        }

        public bool Exists(int id)
        {
            return context.Tags.Any(e => e.ID == id);
        }
    }
}
