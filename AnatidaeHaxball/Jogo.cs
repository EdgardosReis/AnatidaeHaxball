//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnatidaeHaxball
{
    using System;
    using System.Collections.Generic;
    
    public partial class Jogo
    {
        public Jogo()
        {
            this.Estatistica = new HashSet<Estatistica>();
        }
    
        public int competicaoID { get; set; }
        public int jogoID { get; set; }
        public Nullable<short> golosCasa { get; set; }
        public Nullable<short> golosFora { get; set; }
        public Nullable<System.DateTime> data { get; set; }
        public string replay { get; set; }
        public Nullable<int> EquipaCasa { get; set; }
        public Nullable<int> EquipaFora { get; set; }
    
        public virtual Competicao Competicao { get; set; }
        public virtual Equipa Equipa { get; set; }
        public virtual Equipa Equipa1 { get; set; }
        public virtual ICollection<Estatistica> Estatistica { get; set; }
    }
}
