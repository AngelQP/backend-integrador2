using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.UsersGet
{
    public class UsersGetDTO
    {
        public UsersGetDTO(IEnumerable<UserDTO> users, int startAt, int maxResult, int total)
        {
            Users = users;
            StartAt = startAt;
            MaxResult = maxResult;
            Total = total;
        }

        public IEnumerable<UserDTO> Users { get; }
        public int StartAt { get; }
        public int MaxResult { get; }
        public int Total { get; }
    }
}
