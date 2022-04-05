using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEL.Auth.DTO;
using DEL.Auth.Facade;
using DEL.Auth.Infrastructure;

namespace DEL.Auth.Facade
{
    public class EnumFacade : BaseFacade
    {
        public List<EnumDto> GetStatus()
        {
            var typeList = Enum.GetValues(typeof(ComplainStatus))
               .Cast<ComplainStatus>()
               .Select(t => new EnumDto
               {
                   Id = ((int)t),
                   Name = t.ToString(),
                   //DisplayName = UiUtil.GetDisplayName(t)
               });
            return typeList.ToList();
        }
        public List<EnumDto> GetIscomplete()
        {
            var typeList = Enum.GetValues(typeof(IsComplete))
               .Cast<IsComplete>()
               .Select(t => new EnumDto
               {
                   Id = ((int)t),
                   Name = t.ToString(),
                   //DisplayName = UiUtil.GetDisplayName(t)
               });
            return typeList.ToList();
        }
    }
        
}
