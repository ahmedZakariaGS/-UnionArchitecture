using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.OutSourceService
{
    public interface IOutSourceApi
    {
        string Create(string URL, object inputObject);
        string Get(string URL, object inputObject);
        string Get(string URL);

    }
}
