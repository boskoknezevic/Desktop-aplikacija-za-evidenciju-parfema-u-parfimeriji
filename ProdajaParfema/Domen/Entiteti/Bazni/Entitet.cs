using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen.Entiteti.Bazni
{
    public class Entitet<TId>
    {
        public TId ID { get; protected set; }

        public Entitet()
        {
        }

        public Entitet(TId id)
        {
            ID = id;
        }
    }
}
