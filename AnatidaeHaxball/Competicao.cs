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
    
    public partial class Competicao
    {
        public Competicao()
        {
            this.Jogo = new HashSet<Jogo>();
        }
    
        public int competicaoID { get; set; }
        public string nome { get; set; }
        public short edicao { get; set; }
        public string link { get; set; }
        public string imagem { get; set; }
    
        public virtual ICollection<Jogo> Jogo { get; set; }
    }
}