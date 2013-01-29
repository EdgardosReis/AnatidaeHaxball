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
        public string replay { get; set; }
        public byte nrMaos { get; set; }
        public string faseDaCompeticao { get; set; }
        public Nullable<byte> golosCasaMao1 { get; set; }
        public Nullable<byte> golosForaMao1 { get; set; }
        public Nullable<byte> golosCasaMao2 { get; set; }
        public Nullable<byte> golosForaMao2 { get; set; }
        public string replayAux { get; set; }
        public string replayMao2 { get; set; }
        public string replayAuxMao2 { get; set; }
        public Nullable<System.DateTime> dataOficial { get; set; }
        public Nullable<System.DateTime> dataOficialMao2 { get; set; }
        public Nullable<System.DateTime> dataReal { get; set; }
        public Nullable<System.DateTime> dataRealMao2 { get; set; }
        public Nullable<int> idEquipaCasa { get; set; }
        public Nullable<int> idEquipaFora { get; set; }
    
        public virtual Competicao Competicao { get; set; }
        public virtual ICollection<Estatistica> Estatistica { get; set; }
        public virtual Equipa Equipa2 { get; set; }
        public virtual Equipa Equipa11 { get; set; }
    }
}
