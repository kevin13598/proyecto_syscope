using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Interfaces
{
    public interface ISalarioRepositorio
    {
        List<Salario> ListarSalario();

        void InsertarSalario(Salario sal);

        void ActualizarSalario(Salario sal);

        void EliminarSalario(int id);

        Salario BuscarSalario(int id);

        Salario BuscarSalarioEjecutivo(int cedula);
    }
}
