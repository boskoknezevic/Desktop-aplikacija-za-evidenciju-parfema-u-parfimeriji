using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen.Entiteti.Bazni;

namespace Domen.Entiteti
{
    public class Parfem : Entitet <Guid>
    {
        public Parfem(Guid id, string ime, string brend, string pol) : base(id)
        {
            Ime = ime;
            Brend = brend;
            Pol = pol;
        }

        private Parfem()
        {
        }

        public string Ime { get; set; }
        public string Brend { get; set; }
        public string Pol { get; set; }
    }
}
