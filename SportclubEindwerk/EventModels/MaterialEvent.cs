using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportClub.Data.EntityModels;

namespace SportClub.UI.EventModels
{
  public class MaterialEvent
    {

        public Club Club { get; private set; }

        public MaterialEvent(Club club)
        {

            Club = club;
        }




    }
}
